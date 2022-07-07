using System;
using System.Collections.Generic;
using System.Threading;
using PGSauce.Core.Utilities;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public class WfcOptions
    {
        /// <summary>
        /// Called for each newly generated tile.
        /// </summary>
        public Action<TileRequest> onCreate;

        /// <summary>
        /// Called when the generation is complete.
        /// </summary>
        public Action<WfcCompletion> onComplete;

        /// <summary>
        /// Called with a string describing the current phase of the calculations, and the progress from 0 to 1.
        /// Progress can move backwards for retries or backtracing.
        /// Note progress can be called from threads other than the main thread.
        /// </summary>
        public Action<string, Float01> progress;

        /// <summary>
        /// Allows interruption of the calculations
        /// </summary>
        public CancellationToken cancellationToken;

        /// <summary>
        /// Fixes the seed for random number generator.
        /// If the value is zero, the seed is taken from Unity.Random 
        /// </summary>
        public int seed;

        /// <summary>
        /// If set, then generation is offloaded to another thread
        /// stopping Unity from freezing.
        /// Requires you to use StartGenerate in a coroutine.
        /// Multithreaded is ignored in the WebGL player, as it doesn't support threads.
        /// </summary>
        public bool multithreaded = true;

        
        public List<IWfcInitialConstraint> initialConstraints;
    }
}