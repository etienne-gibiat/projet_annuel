using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Core.GlobalVariables
{
    [CreateAssetMenu(fileName = "new Global Layer Mask", menuName = MenuPaths.MenuBase + "Global Variables/Global Layer Mask")]
    public class GlobalLayerMask : IGlobalValue<LayerMask>
    {
    }
}