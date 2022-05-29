using UnityEngine;

namespace PGSauce.Core.Pool
{
    public abstract class PoolObjectBase : ScriptableObject
    {
        public abstract int Size { get; }
        public abstract Poolable PoolableInstance { get; }
    }
}