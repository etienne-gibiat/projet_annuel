using UnityEngine;
using UnityEngine.Events;

namespace PGSauce.Core.Pool
{
    public abstract class Poolable : MonoBehaviour
    {
        [SerializeField] private UnityEvent onSpawn;
        [SerializeField] private UnityEvent onDespawn;
        
        public bool IsSpawned { get; set; }

        public void OnDespawn()
        {
            onDespawn.Invoke();
            OnDespawnCustom();
            IsSpawned = false;
        }

        protected virtual void OnDespawnCustom()
        {
        }
        
        protected virtual void OnSpawnCustom()
        {
        }

        public void OnSpawn()
        {
            onSpawn.Invoke();
            OnSpawnCustom();
            IsSpawned = true;
        }

        public void Despawn()
        {
            Pool.Instance.Despawn(this);
        }
    }
}