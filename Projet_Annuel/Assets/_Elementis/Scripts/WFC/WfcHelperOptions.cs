using System;
using System.Collections.Generic;
using System.Threading;
using PGSauce.Core.Utilities;

namespace _Elementis.Scripts.WFC
{
    internal class WfcHelperOptions
    {
        public IWfcGrid grid;
        public WfcPalette palette;
        public TileModelInfo tileModelInfo;
        // Constraints
        public List<IWfcInitialConstraint> initialConstraints;
        public List<ITileConstraint> constraints;
        // Run control
        public bool backtrack;
        public int stepLimit;
        public WfcAlgorithm algorithm;
        public Action<string, Float01> progress;
        public Action<ITopoArray<ISet<ModelTile>>> progressTiles;
        public XoRoRNG xororng;
        public CancellationToken cancellationToken;
        public FailureMode failureMode;
        public WfcStats stats;
        public WfcInitialConstraint skybox;
    }
}