
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public enum PinType
    {
        /// <summary>
        /// Forces generation the pinned tile at the location of the pin.
        /// </summary>
        Pin,
        /// <summary>
        /// The faces of the pinned tile are used to constrain the cells adjacent to the location of the pinned tile. 
        /// </summary>
        FacesOnly,
        /// <summary>
        /// The faces of the pinned tile are used to constrain the cells adjacent to the location of the pinned tile and
        /// the cells covered by the pin tile are masked out so no tiles will be generated in that location.
        /// </summary>
        FacesAndInterior,
    }

    [AddComponentMenu("WFC/Pinned", 21)]
    public class WfcPinned : MonoBehaviour
    {
        /// <summary>
        /// The tile to pin. 
        /// Defaults to a tile component found on the same GameObject
        /// </summary>
        public WfcTile tile;

        /// <summary>
        /// Sets the type of pin to apply.
        /// </summary>
        public PinType pinType;

    }
}
