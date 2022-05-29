using UnityEngine;
using DG.Tweening;

namespace PGSauce.Core.Pool
{
    public class DelayedDespawnPoolable : Poolable
    {
        [SerializeField] private float despawnTime = 1;

        protected override void OnSpawnCustom()
        {
            base.OnSpawnCustom();
            DOVirtual.DelayedCall(despawnTime, Despawn);
        }
    }
}
