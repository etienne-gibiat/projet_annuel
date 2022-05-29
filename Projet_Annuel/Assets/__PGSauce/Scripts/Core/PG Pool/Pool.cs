using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Core.Pool
{
    public class Pool : MonoSingleton<Pool>
    {
        [AssetsOnly]
        public List<PoolObject> poolObjects;
        [AssetsOnly]
        public Poolable nullPoolable;
        
        [InfoBox("If the pool reaches capacity, should new spawns force older ones to despawn?")]
        public bool recycle;
        
        public Dictionary<PoolObject, Queue<Poolable>> poolDictionnary;

        public override void Init()
        {
            base.Init();
            poolDictionnary = new Dictionary<PoolObject, Queue<Poolable>>();

            foreach (var poolObject in poolObjects)
            {
                var queue = new Queue<Poolable>();
                for (var i = 0; i < poolObject.size; i++)
                {
                    var go = Instantiate(poolObject.poolable);
                    go.gameObject.SetActive(false);
                    queue.Enqueue(go);
                }
                
                poolDictionnary.Add(poolObject, queue);
            }
        }

        public Poolable Spawn(PoolObject poolObject, Vector3 position = new Vector3(),
            Quaternion rotation = new Quaternion())
        {
            if (!poolDictionnary.ContainsKey(poolObject))
            {
                PGDebug.Message($"Pool Dictionnary does not contain {poolObject.name}").LogWarning();
                return nullPoolable;
            }

            var poolable = poolDictionnary[poolObject].Dequeue();

            if (poolable.IsSpawned)
            {
                if (!recycle)
                {
                    PGDebug.Message($"The pool does not have any available object for {poolObject}").LogWarning();
                    poolDictionnary[poolObject].Enqueue(poolable);
                    return nullPoolable;
                }
                
                poolable.Despawn();
            }

            poolable.transform.position = position;
            poolable.transform.rotation = rotation;
            
            poolable.gameObject.SetActive(true);
            poolable.OnSpawn();
            
            poolDictionnary[poolObject].Enqueue(poolable);

            return poolable;
        }

        public void Despawn(Poolable poolable)
        {
            poolable.OnDespawn();
            poolable.gameObject.SetActive(false);
        }
    }
}