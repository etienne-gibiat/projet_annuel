using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public interface IWfcInitialConstraint
    {
        string Name { get; }
    }
    
    /// <summary>
    /// Initial constraint objects fix parts of the generation process in places.
    /// </summary>
    [Serializable]
    public class WfcInitialConstraint : IWfcInitialConstraint
    {
        internal string name;

        internal List<OrientedFace> faceDetails;

        internal List<Vector3Int> offsets;

        internal CellRotation rotation;

        internal Vector3Int cell;

        public string Name => name;
    }

    public class WfcVolumeFilter : IWfcInitialConstraint
    {
        internal string name;

        internal List<WfcTileBase> tiles;

        internal List<Vector3Int> cells;
        public string Name => name;

        public VolumeType volumeType;
    }

    public class WfcPinConstraint : IWfcInitialConstraint
    {
        internal string name;

        internal WfcTileBase tile;

        internal CellRotation rotation;

        internal Vector3Int cell;

        public string Name => name;
    }
}