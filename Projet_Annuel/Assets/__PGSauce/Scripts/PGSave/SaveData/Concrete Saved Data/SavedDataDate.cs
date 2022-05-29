using System;
using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Save
{
    [CreateAssetMenu(menuName = MenuPaths.SavedData + "Date")]
    public class SavedDataDate : SavedData<DateTime>
    {
        [SerializeField] private int daysOffset;
        public override DateTime DefaultValue => DateTime.Now.AddDays(daysOffset);
    }
}
