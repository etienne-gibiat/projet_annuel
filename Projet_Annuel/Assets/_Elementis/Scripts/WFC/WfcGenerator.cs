using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PGSauce.Core.PGDebugging;
using PGSauce.Unity;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

namespace _Elementis.Scripts.WFC
{
    public enum FailureMode
    {
        /// <summary>
        /// If a failure occurs, don't output anything
        /// </summary>
        Cancel,

        /// <summary>
        /// If a failure occurs, output the progress so far
        /// </summary>
        Last,

        /// <summary>
        /// If a failure occurs, backtrack to the last safe point.
        /// </summary>
        LastGood,

        /// <summary>
        /// Examines the progress so far for the minimal set of tiles that cause an issue
        /// </summary>
        Minimal,
    }

    [Serializable]
    public class TileList
    {
        public List<WfcTileBase> tiles;
    }

    [AddComponentMenu("WFC/Generator")]
    public class WfcGenerator : PGMonoBehaviour
    {
        [SerializeField] private Vector3Int size = new Vector3Int(10, 1, 10);

        [SerializeField] private Vector3 center = Vector3.zero;

        /// <summary>
        /// The list of tiles eligable for generation.
        /// </summary>
        public List<TileEntry> tiles = new List<TileEntry>();

        /// <summary>
        /// The stride between each cell in the generation.
        /// "big" tiles may occupy a multiple of this tile size.
        /// </summary>
        public Vector3 tileSize = Vector3.one;

        /// <summary>
        /// If set, backtracking will be used during generation.
        /// Backtracking can find solutions that would otherwise be failures,
        /// but can take a long time.
        /// </summary>
        public bool backtrack = false;

        public WfcTileBase skybox;

        /// <summary>
        /// If backtracking is off, how many times to retry generation if a solution
        /// cannot be found.
        /// </summary>
        public int retries = 5;

        /// <summary>
        /// How many steps to take before retrying from the start.
        /// </summary>
        public int stepLimit = 0;

        /// <summary>
        /// Controls the algorithm used internally for Wave Function Collapse.
        /// </summary>
        public WfcAlgorithm algorithm;

        /// <summary>
        /// Controls what is output when the generation fails.
        /// </summary>
        public FailureMode failureMode = FailureMode.Cancel;

        /// <summary>
        /// Game object to show in cells that have yet to be fully solved.
        /// </summary>
        public GameObject uncertaintyTile;

        /// <summary>
        /// Game object to show in cells that cannot be solved.
        /// </summary>
        public GameObject contradictionTile;

        /// <summary>
        /// If true, the uncertainty tiles shrink as the solver gets more certain.
        /// </summary>
        public bool scaleUncertainyTile = true;

        /// <summary>
        /// If true, then active tiles in the scene will be taken as initial constraints.
        /// If false, then no initial constraints are used.
        /// </summary>
        public bool searchInitialConstraints = true;

        public WfcPalette Palette => tiles.Select(x => x.tile ? x.tile.palette : null).FirstOrDefault();

        public Vector3Int Size
        {
            get => size;
            set => size = value;
        }

        public Vector3 Center
        {
            get => center;
            set => center = value;
        }

        public Vector3 Origin =>
            Center - CubeGeometryUtils.GetCellCenter(size - Vector3Int.one, Vector3.zero, tileSize) / 2;

        public ICell Cell => CubeCell.Instance;

        public Bounds Bounds
        {
            get => new Bounds(Center, Vector3.Scale(tileSize, Size));
            set
            {
                Center = value.center;
                Size = new Vector3Int(
                    Math.Max(1, (int) Math.Round(value.size.x / tileSize.x)),
                    Math.Max(1, (int) Math.Round(value.size.y / tileSize.y)),
                    Math.Max(1, (int) Math.Round(value.size.z / tileSize.z))
                );
            }
        }

        public void Clear()
        {
            var output = GetComponent<IWfcTileOutput>() ?? new InstantiateOutput(transform);
            output.ClearTiles(UnityEngineInterface.Instance);
        }

        public WfcCompletion Generate(WfcOptions options = null)
        {
            var clearable = GetTileOutput().IsEmpty;
            if (clearable)
            {
                Clear();
            }
            
            
            options ??= new WfcOptions();
            var e = StartGenerate(options);
            while (e.MoveNext())
            {
            }

            return e.Result;
        }

        public EnumeratorWithResult<WfcCompletion> StartGenerate(WfcOptions wfcOptions)
        {
            return new EnumeratorWithResult<WfcCompletion>(StartGenerateInner(wfcOptions));
        }

        private IEnumerator StartGenerateInner(WfcOptions wfcOptions)
        {
            var helper = CreateHelper(wfcOptions);
            for (int r = 0; r < retries; r++)
            {
                var name = gameObject.name;
                TilePropagator propagator;
                TilePropagator Run()
                {
                    try
                    {
                        Profiler.BeginThreadProfiling("WFC Generation", name);
                        helper.FullRun(r >= retries - 1);
                        return helper.Propagator;
                    }
                    finally
                    {
                        Profiler.EndThreadProfiling();
                    }
                }
                
                if (wfcOptions.multithreaded)
                {
                    var runTask = Task.Run(Run, wfcOptions.cancellationToken);

                    while (!runTask.IsCompleted)
                        yield return null;

                    wfcOptions.cancellationToken.ThrowIfCancellationRequested();

                    propagator = runTask.Result;
                }
                else
                {
                    propagator = Run();
                }

                var status = propagator.Status;

                var contradictionTile = new ModelTile {};

                var result = propagator.ToValueArray<ModelTile?>(contradiction: contradictionTile, undecided:null);


                if (status == Resolution.Contradiction)
                {
                    if (r < retries - 1)
                    {
                        continue;
                    }
                }

                var completion = new WfcCompletion();
                completion.retries = r;
                completion.backtrackCount = propagator.BacktrackCount;
                completion.success = status == Resolution.Decided;
                completion.tileInstances = GetTileRequests(result, helper.Grid).ToList();
                completion.contradictionLocation = completion.success ? null : WfcUtils.GetContradictionLocation(result, helper.Grid);
                completion.isIncremental = false;
                completion.grid = helper.Grid;


                if (wfcOptions.onComplete != null)
                {
                    wfcOptions.onComplete(completion);
                }
                else
                {
                    HandleComplete(wfcOptions, completion);
                }

                if(completion.success == false && failureMode != FailureMode.Cancel && (uncertaintyTile != null || this.contradictionTile != null))
                {
                    InstantiateUncertaintyObjects(helper.Grid, propagator);
                }

                yield return completion;

                // Exit retries
                break;
            }
            
            
        }
        
        private void InstantiateUncertaintyObjects(IWfcGrid grid, TilePropagator propagator)
        {
            var tileCount = propagator.TileModel.Tiles.Count();
            const float MinScale = 0.0f;
            const float MaxScale = 1.0f;

            var modelTileSets = propagator.ToValueSets<ModelTile>();

            foreach (var cell in grid.GetCells())
            {
                var i = grid.GetIndex(cell);
                var modelTiles = modelTileSets.Get(i);
                if (modelTiles == null || modelTiles.Count == 1)
                {
                    continue;
                }
                var isContradiction = modelTiles.Count == 0;
                // TODO: A lot of this seems shared with GetTesseraTileInstance. Refactor?
                var tiles = modelTiles.Select(x => x.Tile).Distinct().ToList();
                var name = (isContradiction ? "Contradiction" : "Uncertain") + $" ({ cell.x }, { cell.y}, { cell.z})";
                //var go = new GameObject(name);
                var go = Instantiate(isContradiction && contradictionTile != null ? contradictionTile : uncertaintyTile);
                go.name = name;
                go.transform.parent = transform;
                go.transform.localPosition = grid.GetCellCenter(cell);
                if(scaleUncertainyTile && !isContradiction)
                {
                    var scale = (MaxScale - MinScale) * modelTiles.Count / tileCount + MinScale;
                    go.transform.localScale = go.transform.localScale * scale;
                }
                if (!isContradiction)
                {
                    var volume = go.AddComponent<WfcVolume>();
                    volume.generator = this;
                    volume.tiles = tiles;
                }
            }
        }

        
        private void HandleComplete(WfcOptions options, WfcCompletion completion)
        {
            completion.LogErrror();

            if (!completion.success && failureMode == FailureMode.Cancel)
            {
                return;
            }

            IWfcTileOutput to = null;
            if(options.onCreate != null)
            {
                to = new ForEachOutput(options.onCreate);
            }
            else
            {
                to = GetTileOutput();
            }

            to.UpdateTiles(completion, UnityEngineInterface.Instance);
        }
        
        public IWfcTileOutput GetTileOutput(bool forceIncremental = false)
        {
            var component = GetComponent<IWfcTileOutput>();
            if(component != null)
            {
                return component;
            }
            if(forceIncremental)
            {
                return new UpdatableInstantiateOutput(transform);
            }
            return new InstantiateOutput(transform);
        }

        
        internal IEnumerable<TileRequest> GetTileRequests(ITopoArray<ModelTile?> result, IWfcGrid grid)
        {
            var topology = result.Topology;
            var mask = topology.Mask ?? Enumerable.Range(0, topology.IndexCount).Select(x => true).ToArray();

            var empty = mask.ToArray();
            for (var x = 0; x < topology.Width; x++)
            {
                for (var y = 0; y < topology.Height; y++)
                {
                    for (var z = 0; z < topology.Depth; z++)
                    {
                        var p = new Vector3Int(x, y, z);
                        // Skip if already filled
                        if (!empty[grid.GetIndex(p)])
                            continue;
                        var modelTile = result.Get(x, y, z);
                        if (modelTile == null)
                            continue;
                        var rot = modelTile.Value.Rotation;
                        var tile = modelTile.Value.Tile;
                        if (tile == null)
                            continue;

                        var ti = GetTileRequest(x, y, z, modelTile.Value, grid);

                        // Fill locations
                        foreach (var p2 in ti.Cells)
                        {
                            if (grid.InBounds(p2))
                                empty[grid.GetIndex(p2)] = false;
                        }

                        if (ti != null)
                        {
                            yield return ti;
                        }
                    }
                }
            }
        }
        
        internal TileRequest GetTileRequest(int x, int y, int z, ModelTile modelTile, IWfcGrid grid)
        {
            var rot = modelTile.Rotation;
            var tile = modelTile.Tile;
            var cellType = grid.CellType;
            
            var p = new Vector3Int(x, y, z);

            var tileTrs = new TRS(cellType.GetMatrix(rot));

            var localTrs = grid.GetTRS(p) * tileTrs * new TRS(-cellType.GetCellCenter(modelTile.Offset, tile.center, tileSize));
            var worldTrs = TRS.World(transform) * localTrs;

            var cells = new Vector3Int[tile.offsets.Count];
            var rotations = new CellRotation[tile.offsets.Count];
            for (var i = 0; i < tile.offsets.Count; i++)
            {
                var offset = tile.offsets[i];
                if (!grid.TryMoveByOffset(p, modelTile.Offset, offset, rot, out var cell, out var rotation))
                {
                    throw new Exception($"BigTile {modelTile.Tile} is not fully contained in topology. This indicates an internal error.");
                }
                cells[i] = cell;
                rotations[i] = rotation;
            }
            var instance = new TileRequest()
            {
                Tile = tile,
                Position = worldTrs.Position,
                Rotation = worldTrs.Rotation,
                LossyScale = worldTrs.Scale,
                LocalPosition = localTrs.Position,
                LocalRotation = localTrs.Rotation,
                LocalScale = localTrs.Scale,
                Cell = cells[0],
                Cells = cells,
                CellRotation = rot,
                CellRotations = rotations,
            };
            return instance;
        }


        internal WfcGeneratorHelper CreateHelper(WfcOptions wfcOptions)
        {
            var t1 = DateTime.Now;
            var progress = wfcOptions.progress;
            var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            var rng = new XoRoRNG(seed);
            ValidateTiles();
            var actualInitialConstraints = new List<IWfcInitialConstraint>();
            var cellType = Cell;
            var tileModelInfo = TileModelInfo.Create(tiles, cellType);
            var grid = GetGrid();
            
            
            
            var constraintsBuilder = new WfcInitialConstraintBuilder(transform, grid);
            var initialConstraints = wfcOptions.initialConstraints ?? (searchInitialConstraints
                    ? (IEnumerable<IWfcInitialConstraint>) constraintsBuilder.SearchInitialConstraints()
                    : null) ??
                Array.Empty<IWfcInitialConstraint>();
            actualInitialConstraints.AddRange(initialConstraints);
            var constraints = GetTileConstraints(tileModelInfo, grid);

            var sBox = skybox == null
                ? null
                : new WfcInitialConstraint()
                {
                    faceDetails = skybox.faceDetails,
                    offsets = skybox.offsets,
                };
            
            var stats = new WfcStats() { createHelperTime = (DateTime.Now - t1).TotalSeconds };
            var options = new WfcHelperOptions()
            {
                grid = grid,
                palette = Palette,
                tileModelInfo = tileModelInfo,
                initialConstraints = actualInitialConstraints,
                constraints = constraints,
                backtrack = backtrack,
                stepLimit = stepLimit,
                algorithm = algorithm,
                progress = progress,
                progressTiles = null,
                xororng = rng,
                cancellationToken = wfcOptions.cancellationToken,
                failureMode = failureMode,
                stats = stats,
                skybox = sBox
            };

            return new WfcGeneratorHelper(options);
        }

        private List<ITileConstraint> GetTileConstraints(TileModelInfo tileModelInfo, IWfcGrid grid)
        {
            var l = new List<ITileConstraint>();
            foreach (var constraintComponent in GetComponents<WfcConstraint>())
            {
                if (constraintComponent.enabled)
                {
                    var constraints = constraintComponent.GetTileConstraint(tileModelInfo, grid);
                    l.AddRange(constraints);
                }
            }
            return l;
        }

        private IWfcGrid GetGrid()
        {
            return new CubeGrid(Origin, Size, tileSize);
        }

        private void ValidateTiles()
        {
            var allTiles = tiles.Select(x => x.tile).Where(x => x != null);

            var cellTypes = GetCellTypes();
            if (cellTypes.Count > 1)
            {
                PGDebug.Message(
                        $"You cannot mix tiles of multiple cell types, such as {string.Join(", ", cellTypes.Select(x => x.GetType().Name))} .\n")
                    .LogError();
            }

            var misSizedTiles = GetMisSizedTiles().ToList();
            if (misSizedTiles.Count > 0)
            {
                PGDebug.Message(
                        $"Some tiles do not have the same tileSize as the generator, {tileSize}, this can cause unexpected behaviour.\n" +
                        "NB: Big tiles should still share the same value of tileSize\n" +
                        "Affected tiles:\n" +
                        string.Join("\n", misSizedTiles))
                    .LogError();
            }

            var palette = tiles.Select(x => x.tile != null ? x.tile.palette : null).FirstOrDefault();
            var wrongPaletteTiles = allTiles.Where(x => x.palette != palette).ToList();
            if (wrongPaletteTiles.Count > 0)
            {
                PGDebug.Message(
                        $"Some tiles do not all have the same palette.\b" +
                        "Affected tiles:\n" +
                        string.Join("\n", wrongPaletteTiles))
                    .LogError();
            }
        }
        
        public IEnumerable<WfcTileBase> GetMisSizedTiles()
        {
            bool IsMisSized(WfcTileBase tile)
            {
                if (size.x != 1 && Math.Abs(tile.tileSize.x - tileSize.x) > 0.01f) return true;
                if (size.y != 1 && Math.Abs(tile.tileSize.y - tileSize.y) > 0.01f) return true;
                if (size.z != 1 && Math.Abs(tile.tileSize.z - tileSize.z) > 0.01f) return true;

                return false;
            }

            return tiles.Select(x => x.tile)
                .Where(x => x != null)
                .Where(IsMisSized);
        }

        public IList<ICell> GetCellTypes()
        {
            return tiles.Select(x => x.tile)
                .Where(x => x != null)
                .Select(x => x.Cell)
                .Distinct()
                .ToList();
        }

        public static GameObject[] Instantiate(TileRequest instance, Transform parent)
        {
            return Instantiate(instance, parent, instance.Tile.gameObject, instance.Tile.instantiateChildrenOnly);
        }

        public static GameObject[] Instantiate(TileRequest instance, Transform parent, GameObject gameObject,
            bool instantiateChildrenOnly)
        {
            var transformsAndGameObjects =
                InstantiateUntransformed(instance, parent, gameObject, instantiateChildrenOnly);
            var gameObjects = transformsAndGameObjects.Select(x => x.Item2).ToArray();
            // Flip box transformations to stop Unity whining: "BoxColliders does not support negative scale or size."
            foreach (var go in gameObjects)
            {
                var localScale = go.transform.localScale;
                var flip = new Vector3(Math.Sign(localScale.x), Math.Sign(localScale.y), Math.Sign(localScale.z));
                if (flip == Vector3.one)
                    continue;
                foreach (var bc in go.GetComponentsInChildren<BoxCollider>())
                {
                    bc.size = Vector3.Scale(flip, bc.size);
                }
            }

            return gameObjects;
        }

        private static (Matrix4x4, GameObject)[] InstantiateUntransformed(TileRequest instance, Transform parent,
            GameObject gameObject, bool instantiateChildrenOnly)
        {
            var cell = instance.Cell;
            if (instantiateChildrenOnly)
            {
                // These two methods are mostly equivalent, but we need to investigate which is actually faster in Unity
                if (true)
                {
                    var worldTransform = Matrix4x4.TRS(instance.Position, instance.Rotation, instance.LossyScale);
                    var localTransform =
                        Matrix4x4.TRS(instance.LocalPosition, instance.LocalRotation, instance.LossyScale);
                    return gameObject.transform.Cast<Transform>().Select(child =>
                    {
                        var childToInstance =
                            gameObject.transform.worldToLocalMatrix * child.transform.localToWorldMatrix;
                        var local = new TRS(localTransform * childToInstance);
                        var world = new TRS(worldTransform * childToInstance);
                        var go = GameObject.Instantiate(child.gameObject, world.Position, world.Rotation, parent);
                        go.transform.localScale = local.Scale;
                        go.name = child.gameObject.name + $" ({cell.x}, {cell.y}, {cell.z})";
                        return (childToInstance, go);
                    }).ToArray();
                }
            }
            else
            {
                var go = GameObject.Instantiate(gameObject, instance.Position, instance.Rotation, parent);
                go.transform.localScale = instance.LocalScale;
                go.name = gameObject.name + $" ({cell.x}, {cell.y}, {cell.z})";
                return new[] {(Matrix4x4.identity, go)};
            }
        }

    }
    
    
}