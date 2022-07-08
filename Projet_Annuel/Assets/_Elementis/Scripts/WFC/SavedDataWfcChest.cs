using System;
using PGSauce.Core.Strings;
using PGSauce.Save;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    [Serializable]
    public struct WfcChestData
    {
        public bool opened;
    }
    [CreateAssetMenu(menuName = MenuPaths.GameSavedData + "WFC Chest")]
    public class SavedDataWfcChest  : SavedData<WfcChestData>
    {
        
    }
}