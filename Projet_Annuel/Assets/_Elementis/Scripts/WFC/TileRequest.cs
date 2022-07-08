using System.Linq;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public class TileRequest
    {
        public WfcTileBase Tile { get; internal set; }
        // TRS in World space
        public Vector3 Position { get; internal set; }
        public Quaternion Rotation { get; internal set; }
        public Vector3 LossyScale { get; internal set; }
        // TRS in generator space
        public Vector3 LocalPosition { get; internal set; }
        public Quaternion LocalRotation { get; internal set; }
        public Vector3 LocalScale { get; internal set; }

        /// <summary>
        /// The grid cell this instance fills. (for big tiles, this is the cell of the first offset) 
        /// </summary>
        public Vector3Int Cell { get; internal set; }
        
        /// <summary>
        /// The rotation this instance is placed at (for big tiles, this is the cell of the first offset)
        /// </summary>
        public CellRotation CellRotation { get; internal set; }

        /// <summary>
        /// The cells this instance fills, in the same order as the tile offsets. 
        /// </summary>
        public Vector3Int[] Cells { get; internal set; }

        /// <summary>
        /// The rotations this instance instance, in the same order as the tile offsets.
        /// Most grids will have same rotation for all offsets.
        /// </summary>
        public CellRotation[] CellRotations { get; internal set; }

        public TileRequest Clone()
        {
            var i = (TileRequest)MemberwiseClone();
            i.Cells = i.Cells.ToArray();
            i.CellRotations = i.CellRotations.ToArray();
            return i;
        }

        /// <summary>
        /// Sets Position/Rotation/Scale from the local versions and a given transform
        /// </summary>
        public void Align(Transform transform)
        {
            var localTrs = new TRS(LocalPosition, LocalRotation, LocalScale);
            var worldTrs = TRS.World(transform) * localTrs;
            Position = worldTrs.Position;
            Rotation = worldTrs.Rotation;
            LossyScale = worldTrs.Scale;
        }
    }
}