using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public class WfcInitialConstraintBuilder
    {
        private Transform transform;

        private IWfcGrid grid;

        internal WfcInitialConstraintBuilder(Transform transform, IWfcGrid grid)
        {
            this.transform = transform;

            this.grid = grid;
        }

        private IEnumerable<T> FindObjectsOfType<T>() where T : Component
        {
            return UnityEngine.Object.FindObjectsOfType<T>()
                .Where(x => x.gameObject.activeSelf);
        }

        /// <summary>
        /// Searches the scene for all applicable game objects and converts them to ITesseraInitialConstraint
        /// </summary>
        public List<IWfcInitialConstraint> SearchInitialConstraints()
        {
            var pins = FindObjectsOfType<WfcPinned>().Where(x => x != null);
            var pinGos = new HashSet<GameObject>(pins.Select(x => x.gameObject));
            var cellType = grid.CellType;

            var pinConstraints = pins
                .Select(x => GetInitialConstraint(x, x.transform.localToWorldMatrix));
            var tileConstraints = FindObjectsOfType<WfcTileBase>()
                .Where(x => !pinGos.Contains(x.gameObject))
                .Where(x => x.Cell == cellType)
                .Select(x => GetInitialConstraint(x, x.transform.localToWorldMatrix))
                .Cast<IWfcInitialConstraint>();
            var volumeConstraints = FindObjectsOfType<WfcVolume>()
                .Where(x => x != null)
                .Select(x => GetInitialConstraint(x))
                .Cast<IWfcInitialConstraint>();
            return pinConstraints.Concat(tileConstraints).Concat(volumeConstraints)
                .Where(x => x != null)
                .ToList();
        }

        /// <summary>
        /// Gets the initial constraint for a given game object. 
        /// It checks for a TesseraPinned, TesseraTile or TesseraVolume component.
        /// </summary>
        public IWfcInitialConstraint GetInitialConstraint(GameObject gameObject)
        {
            if (gameObject.GetComponent<WfcPinned>() is WfcPinned pin)
            {
                return GetInitialConstraint(pin);
            }
            else if (gameObject.GetComponent<WfcTileBase>() is WfcTileBase tesseraTile)
            {
                return GetInitialConstraint(tesseraTile, tesseraTile.transform.localToWorldMatrix);
            }
            else if (gameObject.GetComponent<WfcVolume>() is WfcVolume tesseraVolume)
            {
                return GetInitialConstraint(tesseraVolume);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Gets the initial constraint from a given tile.
        /// The tile should be aligned with the grid defined by this generator.
        /// </summary>
        /// <param name="tile">The tile to inspect</param>
        public WfcVolumeFilter GetInitialConstraint(WfcVolume volume)
        {
            var colliders = volume.gameObject.GetComponents<Collider>();
            var cells = volume.invertArea
                ? new HashSet<Vector3Int>(grid.GetCells())
                : new HashSet<Vector3Int>();

            foreach (var collider in colliders)
            {
                if (!collider.enabled)
                    continue;

                var worldBounds = collider.bounds;
                var localBounds = GeometryUtils.Multiply(transform.worldToLocalMatrix, worldBounds);
                foreach (var cell in grid.GetCellsIntersectsApprox(localBounds, true))
                {
                    var center = grid.GetCellCenter(cell);
                    var worldCenter = transform.TransformPoint(center);
                    if (collider.ClosestPoint(worldCenter) == worldCenter)
                    {
                        if (volume.invertArea)
                        {
                            cells.Remove(cell);
                        }
                        else
                        {
                            cells.Add(cell);
                        }
                    }
                }
            }

            return new WfcVolumeFilter()
            {
                name = volume.name,
                cells = cells.ToList(),
                tiles = volume.tiles,
                volumeType = volume.volumeType,
            };
        }

        /// <summary>
        /// Gets the initial constraint from a given tile.
        /// The tile should be aligned with the grid defined by this generator.
        /// </summary>
        /// <param name="tile">The tile to inspect</param>
        public WfcInitialConstraint GetInitialConstraint(WfcTileBase tile)
        {
            return GetInitialConstraint(tile, tile.transform.localToWorldMatrix);
        }

        /// <summary>
        /// Gets the initial constraint from a given tile at a given position.
        /// The tile should be aligned with the grid defined by this generator.
        /// </summary>
        /// <param name="tile">The tile to inspect</param>
        /// <param name="localToWorldMatrix">The matrix indicating the position and rotation of the tile</param>
        public WfcInitialConstraint GetInitialConstraint(WfcTileBase tile, Matrix4x4 localToWorldMatrix)
        {
            if (!grid.FindCell(tile.center, transform.worldToLocalMatrix * localToWorldMatrix, out var cell,
                out var rotation))
            {
                return null;
            }

            // TODO: Needs support for big tiles
            return new WfcInitialConstraint()
            {
                name = tile.name,
                faceDetails = tile.faceDetails,
                offsets = tile.offsets,
                cell = cell,
                rotation = rotation,
            };
        }

        /// <summary>
        /// Gets the initial constraint from a given pin at a given position.
        /// It should be aligned with the grid defined by this generator.
        /// </summary>
        /// <param name="pin">The pin to inspect</param>
        public IWfcInitialConstraint GetInitialConstraint(WfcPinned pin)
        {
            return GetInitialConstraint(pin, pin.transform.localToWorldMatrix);
        }


        /// <summary>
        /// Gets the initial constraint from a given pin at a given position.
        /// It should be aligned with the grid defined by this generator.
        /// </summary>
        /// <param name="pin">The pin to inspect</param>
        /// <param name="localToWorldMatrix">The matrix indicating the position and rotation of the tile</param>
        public IWfcInitialConstraint GetInitialConstraint(WfcPinned pin, Matrix4x4 localToWorldMatrix)
        {
            var tile = pin.tile ??
                       pin.GetComponent<WfcTile>() ?? throw new Exception($"Tile not defined for {pin}");

            if (!grid.FindCell(tile.center, transform.worldToLocalMatrix * localToWorldMatrix, out var cell,
                out var rotation))
            {
                return null;
            }

            if (pin.pinType == PinType.Pin)
            {
                return new WfcPinConstraint()
                {
                    name = pin.name,
                    tile = tile,
                    cell = cell,
                    rotation = rotation,
                };
            }
            else
            {
                return new WfcInitialConstraint()
                {
                    name = tile.name,
                    faceDetails = tile.faceDetails,
                    offsets = pin.pinType == PinType.FacesOnly ? new List<Vector3Int>() : tile.offsets,
                    cell = cell,
                    rotation = rotation,
                };
            }
        }

        /// <summary>
        /// Converts a TesseraTileInstance to a ITesseraInitialConstraint.
        /// This allows you to easily use the output of one generation for later generations
        /// </summary>
        public IWfcInitialConstraint GetInitialConstraint(TileRequest tileInstance,
            PinType pinType = PinType.Pin)
        {
            if (pinType == PinType.Pin)
            {
                return new WfcPinConstraint()
                {
                    name = tileInstance.Cell.ToString(),
                    tile = tileInstance.Tile,
                    cell = tileInstance.Cell,
                    rotation = tileInstance.CellRotation,
                };
            }
            else
            {
                return new WfcInitialConstraint()
                {
                    name = tileInstance.Cell.ToString(),
                    faceDetails = tileInstance.Tile.faceDetails,
                    offsets = pinType == PinType.FacesOnly ? new List<Vector3Int>() : tileInstance.Tile.offsets,
                    cell = tileInstance.Cell,
                    rotation = tileInstance.CellRotation,
                };
            }
        }
    }
    
    internal static class GeometryUtils
    {
        internal static Quaternion ToQuaternion(Rotation r)
        {
            return Quaternion.Euler(0, -r.RotateCw, 0);
        }

        internal static Matrix4x4 ToMatrix(Rotation r)
        {
            var q = Quaternion.Euler(0, -r.RotateCw, 0);
            return Matrix4x4.TRS(Vector3.zero, q, new Vector3(r.ReflectX ? -1 : 1, 1, 1));
        }

        internal static Bounds Multiply(Matrix4x4 m, Bounds b)
        {
            var bx = Abs(m.MultiplyVector(Vector3.right * b.size.x));
            var by = Abs(m.MultiplyVector(Vector3.up * b.size.y));
            var bz = Abs(m.MultiplyVector(Vector3.forward * b.size.z));
            var c = m.MultiplyPoint3x4(b.center);
            return new Bounds(c, bx + by + bz);
        }

        public static Vector3 Abs(Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }

        public static Vector3 ElementwiseDivide(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }
    }
}