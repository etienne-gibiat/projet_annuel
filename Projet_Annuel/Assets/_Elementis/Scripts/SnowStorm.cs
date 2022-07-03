using System;
using _Elementis.Scripts.Character_Controller;
using DG.Tweening;
using PGSauce.Animation;
using UnityEngine;

namespace _Elementis.Scripts
{
    public class SnowStorm : MonoBehaviour
    {
        public float fogIntensityInSnow = 0.05f;

        private void Awake()
        {
            _normalFogIntensity = RenderSettings.fogDensity;
            _currentFog = _normalFogIntensity;
        }

        private float _normalFogIntensity;
        private bool _inSnow;

        private float _targetFog;
        private float _currentFog;
        [SerializeField] private float fogLerp = 0.05f;

        private void Update()
        {
            _targetFog = _inSnow ? fogIntensityInSnow : _normalFogIntensity;
            
            _currentFog = Mathf.Lerp(_currentFog, _targetFog, fogLerp);

            RenderSettings.fogDensity = _currentFog;
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleCollision(other, true);
        }

        private void HandleCollision(Collider other, bool entered)
        {
            if (other.gameObject.layer == Layers.PLAYER)
            {
                _inSnow = entered;
                var player = other.gameObject.GetComponentInParent<ElementisCharacterController>();
                if (player)
                {
                    player.InSnow = entered;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            HandleCollision(other, false);
        }
    }
}