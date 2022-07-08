using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public interface IWfcTileOutput
    {
        /// <summary>
        /// Is this output safe to use with AnimatedGenerator
        /// </summary>
        bool SupportsIncremental { get; }

        /// <summary>
        /// Is the output currently empty.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Clear the output
        /// </summary>
        void ClearTiles(IWfcEngine engine);

        /// <summary>
        /// Update a chunk of tiles.
        /// If inremental updates are supported, then:
        ///  * Tiles can replace other tiles, as indicated by the <see cref="TesseraTileInstance.Cells"/> field.
        ///  * A tile of null indicates that the tile should be erased
        /// </summary>
        void UpdateTiles(WfcCompletion completion, IWfcEngine engine);
    }
    
    internal class ForEachOutput : IWfcTileOutput
    {
        private Action<TileRequest> onCreate;

        public ForEachOutput(Action<TileRequest> onCreate)
        {
            this.onCreate = onCreate;
        }

        public bool IsEmpty => throw new NotImplementedException();

        public bool SupportsIncremental => throw new NotImplementedException();

        public void ClearTiles(IWfcEngine engine)
        {
            throw new NotImplementedException();
        }

        public void UpdateTiles(WfcCompletion completion, IWfcEngine engine)
        {
            foreach (var i in completion.tileInstances)
            {
                onCreate(i);
            }
        }
    }
    
    internal class UpdatableInstantiateOutput : IWfcTileOutput
    {
        private Dictionary<Vector3Int, GameObject[]> instantiated = new Dictionary<Vector3Int, GameObject[]>();
        private readonly Transform transform;

        public UpdatableInstantiateOutput(Transform transform)
        {
            this.transform = transform;
        }

        public bool IsEmpty => transform.childCount == 0;

        public bool SupportsIncremental => true;

        private void Clear(Vector3Int p, IWfcEngine engine)
        {
            if (instantiated.TryGetValue(p, out var gos) && gos != null)
            {
                foreach (var go in gos)
                {
                    engine.Destroy(go);
                }
            }

            instantiated[p] = null;
        }

        public void ClearTiles(IWfcEngine engine)
        {
            foreach (var k in instantiated.Keys.ToList())
            {
                Clear(k, engine);
            }
        }

        public void UpdateTiles(WfcCompletion completion, IWfcEngine engine)
        {
            foreach (var i in completion.tileInstances)
            {
                foreach (var p in i.Cells)
                {
                    Clear(p, engine);
                }
                if (i.Tile != null)
                {
                    instantiated[i.Cells.First()] = WfcGenerator.Instantiate(i, transform);
                }
            }
        }
    }
    
    public class InstantiateOutput : IWfcTileOutput
    {
        private readonly Transform transform;

        public InstantiateOutput(Transform transform)
        {
            this.transform = transform;
        }

        public bool IsEmpty => transform.childCount == 0;

        public bool SupportsIncremental => false;

        public void ClearTiles(IWfcEngine engine)
        {
            var children = transform.Cast<Transform>().ToList();
            foreach (var child in children)
            {
                engine.Destroy(child.gameObject);
            }
        }

        public void UpdateTiles(WfcCompletion completion, IWfcEngine engine)
        {
            foreach (var i in completion.tileInstances)
            {
                foreach (var go in WfcGenerator.Instantiate(i, transform))
                {
                    engine.RegisterCreatedObjectUndo(go);
                }
            }
        }
    }

}