using PGSauce.Core.Strings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Core.Pool
{
    [CreateAssetMenu(menuName = MenuPaths.MenuBase + "Pools/Pool Object")]
    public class PoolObject : ScriptableObject
    {
        [AssetsOnly]
        public Poolable poolable;
        [Min(1)]
        public int size = 1;

        public Poolable Spawn(Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
        {
            return Pool.Instance.Spawn(this, position, rotation);
        }
    }
}