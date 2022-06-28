using System;
using System.Collections.Generic;
using System.Linq;
using AmazingAssets.DynamicRadialMasks;
using PGSauce.Core.Utilities;
using PGSauce.Unity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Elementis.Scripts.World_Restoration
{
    public class WorldRestorationZone : PGMonoBehaviour
    {
        [SerializeField] public float maxRadius;
        [SerializeField] private DRMGameObject drm;

        private Dictionary<IRestorable, float> _objectToRadius;
        private Float01 _currentRadiusLerp;

        private List<IRestorable> _restorables;

#if UNITY_EDITOR
        [ShowInInspector]
        private List<string> Names =>
            _restorables?.Select(r => r.Name()).ToList();
#endif

        private void Awake()
        {
            _objectToRadius = new Dictionary<IRestorable, float>();
            _restorables = GetComponentsInChildren<IRestorable>().ToList();
            foreach (var restorable in _restorables)
            {
                //restorable.OnUnRestored();
                var dist = Vector3.Distance(transform.position, restorable.Position());
                _objectToRadius.Add(restorable, GetLerpFromRadius(dist));
            }
            _currentRadiusLerp = 1f;
            UpdateRadius(0f);
        }

        [Button]
        public void UpdateRadius(Float01 value)
        {
            foreach (var restorable in _restorables)
            {
                var restorableLerp = _objectToRadius[restorable];
                if (_currentRadiusLerp < value)
                {
                    if (_currentRadiusLerp <= restorableLerp && restorableLerp <= value)
                    {
                        restorable.OnRestored();
                    }
                }
                else if (_currentRadiusLerp > value)
                {
                    //SHRINKING
                    if (value <= restorableLerp && restorableLerp <= _currentRadiusLerp)
                    {
                        restorable.OnUnRestored();
                    }
                }
                else
                {
                    return;
                }
            }
            
            _currentRadiusLerp = value;
            drm.radius = CurrentRadius;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, maxRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, CurrentRadius);

            if (_restorables != null)
            {
                foreach (var restorable in _restorables)
                {
                    var restorableLerp = _objectToRadius[restorable];
                    if (restorableLerp > _currentRadiusLerp)
                    {
                        // NOT RESTORED
                        Gizmos.color = Color.red;
                    }
                    else
                    {
                        // RESTORED
                        Gizmos.color = Color.green;
                    }
                    
                    Gizmos.DrawSphere(restorable.Position(), 1f);
                }
            }
        }

        public float CurrentRadius => GetRadiusFromLerp(_currentRadiusLerp);

        private float GetRadiusFromLerp(Float01 lerp)
        {
            return Mathf.Lerp(0, maxRadius, lerp);
        }

        private float GetLerpFromRadius(float radius)
        {
            return Mathf.InverseLerp(0, maxRadius, radius);
        }
    }
}