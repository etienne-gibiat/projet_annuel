using System;
using System.Collections.Generic;
using PGSauce.Core.Strings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.NoDevSuite
{
    public class SpawnObjectNoDevSuiteAction : NoDevEventReacter
    {
        [Serializable]
        private struct ParticlesData
        {
            public Transform spawn;
            [AssetsOnly] public GameObject prefab;
            public bool localSpace;
        }

        [SerializeField] private List<ParticlesData> particlesToSpawn;

        protected override void React(GameObjectData data)
        {
            foreach (var particlesData in particlesToSpawn)
            {
                var ps = Instantiate(particlesData.prefab, particlesData.spawn, true);
                var transform = ps.transform;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                transform.localScale = Vector3.one;

                if (!particlesData.localSpace)
                {
                    transform.SetParent(null, true);
                }
            }
            
        }
    }
}