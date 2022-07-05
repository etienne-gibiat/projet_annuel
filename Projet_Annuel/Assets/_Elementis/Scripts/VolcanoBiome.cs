using System;
using _Elementis.Scripts.Character_Controller;
using _Elementis.Scripts.Skybox_Blending;
using DG.Tweening;
using PGSauce.Animation;
using UnityEngine;

namespace _Elementis.Scripts
{
    public class VolcanoBiome : MonoBehaviour
    {
        public float fogIntensityInSnow = 0.05f;
        public Color fogColor = Color.white;
        public Material defaultSkybox;
        public Material volcanoSkybox;

        private void Awake()
        {
            _defaultColor = RenderSettings.fogColor;
            defaultSkybox = RenderSettings.skybox;
            _normalFogIntensity = RenderSettings.fogDensity;
            _currentFog = _normalFogIntensity;
        }

        private float _normalFogIntensity;
        private bool _inBiome;
        private Color _defaultColor;

        private float _targetFog;
        private float _currentFog;
        [SerializeField] private float fogLerp = 0.05f;

        private void Update()
        {
            
            _targetFog = _inBiome ? fogIntensityInSnow : _normalFogIntensity;
            
            _currentFog = Mathf.Lerp(_currentFog, _targetFog, fogLerp);

            if (_inBiome)
            {
                RenderSettings.fogDensity = _currentFog;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleCollision(other, true);
        }

        private void HandleCollision(Collider other, bool entered)
        {
            if (other.gameObject.layer == Layers.PLAYER)
            {
                _inBiome = entered;
                if (_inBiome)
                {
                    RenderSettings.skybox = volcanoSkybox;
                    RenderSettings.fogColor = fogColor;
                }
                else
                {
                    RenderSettings.skybox = defaultSkybox;
                    RenderSettings.fogColor = _defaultColor;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            HandleCollision(other, false);
        }
    }
}