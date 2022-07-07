using System.Collections.Generic;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public enum VolumeType
    {
        /// <summary>
        /// Restricts the set of tiles inside the volume
        /// </summary>
        TilesetFilter,
        /// <summary>
        /// Removes the cells inside the volume from generation
        /// </summary>
        MaskOut,
    }

    [AddComponentMenu("Wfc/Volume", 21)]
    public class WfcVolume : MonoBehaviour
    {
        /// <summary>
        /// No effect on behaviour, setting this improves the UI in the Unity inspector.
        /// </summary>
        public WfcGenerator generator;

        /// <summary>
        /// The list of tiles to filter on.
        /// </summary>
        public List<WfcTileBase> tiles;

        /// <summary>
        /// If false, affect all cells inside the volume's colliders.
        /// If true, affect all cells outside.
        /// </summary>
        public bool invertArea;

        /// <summary>
        /// Controls the behaviour of this volume
        /// </summary>
        public VolumeType volumeType;
    }
}