using UnityEngine;

namespace PGSauce.Core.Utilities
{
    public abstract class DataProvider<T> : ScriptableObject
    {
        public abstract T GetValue();
    }
}