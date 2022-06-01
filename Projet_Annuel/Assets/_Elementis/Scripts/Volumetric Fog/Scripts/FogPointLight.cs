using System;
using UnityEngine;

namespace VolumetricFogAndMist2 {

    [ExecuteInEditMode]
    public class FogPointLight : MonoBehaviour {

        [NonSerialized] public Light pointLight;

        [Tooltip("Inscattering multiplier for this point light")]
        public float inscattering = 1f;
        [Tooltip("Intensity multiplier for this point light")]
        public float intensity = 1f;

        private void OnEnable() {
            pointLight = GetComponent<Light>();
            VolumetricFogManager.pointLightManager.RegisterPointLight(this);
        }

        private void OnDisable() {
            if (VolumetricFogManager.GetManagerIfExists() != null) {
                VolumetricFogManager.pointLightManager.UnregisterPointLight(this);
            }
        }

        private void OnValidate() {
            inscattering = Mathf.Max(0, inscattering);
            intensity = Mathf.Max(0, intensity);
        }

    }
}