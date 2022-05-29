using PGSauce.Core.Strings;
using PGSauce.Save;
using UnityEngine;

namespace PGSauce.Core.Utilities
{
    [CreateAssetMenu(menuName = MenuPaths.DataProvidersBool + "Saved Data")]
    public class SavedDataBoolProvider : BoolProvider
    {
        [SerializeField] private SavedDataBool savedDataBool;
        public override bool GetValue()
        {
            return savedDataBool.Load();
        }
    }
}