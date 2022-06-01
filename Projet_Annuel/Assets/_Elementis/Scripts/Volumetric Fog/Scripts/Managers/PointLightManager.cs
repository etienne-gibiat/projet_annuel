using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolumetricFogAndMist2 {

    [ExecuteInEditMode]
    [DefaultExecutionOrder(100)]
    public class PointLightManager : MonoBehaviour, IVolumetricFogManager {

        public static bool usingPointLights;

        public string managerName {
            get {
                return "Point Light Manager";
            }
        }

        public const int MAX_POINT_LIGHTS = 16;

        [Header("Point Light Search Settings")]
        [Tooltip("Point lights are sorted by distance to tracking center object")]
        public Transform trackingCenter;
        [Tooltip("Point lights are sorted by camera distance every certain time interval to ensure the nearest 16 point lights are used.")]
        public float distanceSortTimeInterval = 3f;
        [Tooltip("Ignore point lights behind camera")]
        public bool excludeLightsBehind = true;

        [Header("Common Settings")]
        [Tooltip("Global inscattering multiplier for point lights")]
        public float inscattering = 1f;
        [Tooltip("Global intensity multiplier for point lights")]
        public float intensity = 1f;
        [Tooltip("Reduces light intensity near point lights")]
        public float insideAtten;

        readonly List<FogPointLight> pointLights = new List<FogPointLight>();
        Vector4[] pointLightColorBuffer;
        Vector4[] pointLightPositionBuffer;
        float distanceSortLastTime;
        bool requireRefresh;

        private void OnEnable() {
            if (trackingCenter == null) {
                Camera cam = null;
                Tools.CheckCamera(ref cam);
                if (cam != null) {
                    trackingCenter = cam.transform;
                }
            }
            if (pointLightColorBuffer == null || pointLightColorBuffer.Length != MAX_POINT_LIGHTS) {
                pointLightColorBuffer = new Vector4[MAX_POINT_LIGHTS];
            }
            if (pointLightPositionBuffer == null || pointLightPositionBuffer.Length != MAX_POINT_LIGHTS) {
                pointLightPositionBuffer = new Vector4[MAX_POINT_LIGHTS];
            }
        }

        private void LateUpdate() {
            if (!usingPointLights) return;
            usingPointLights = false;

            if (requireRefresh) {
                requireRefresh = false;
                TrackPointLights(true);
            } else {
                if (pointLights.Count == 0) return;
                TrackPointLights();
            }
            SubmitPointLightData();

        }

        void SubmitPointLightData() {

            int k = 0;
            bool appIsRunning = Application.isPlaying;

            Vector3 trackingCenterPosition;
            Vector3 trackingCenterForward;
            bool isExcludingLightsBehind = excludeLightsBehind && appIsRunning;

            if (isExcludingLightsBehind && trackingCenter != null) {
                trackingCenterPosition = trackingCenter.position;
                trackingCenterForward = trackingCenter.forward;
            } else {
                trackingCenterPosition = trackingCenterForward = Vector3.zero;
                isExcludingLightsBehind = false;
            }

            int pointLightsCount = pointLights.Count;
            for (int i = 0; k < MAX_POINT_LIGHTS && i < pointLightsCount; i++) {
                FogPointLight fogPointLight = pointLights[i];
                if (fogPointLight == null || !fogPointLight.isActiveAndEnabled) continue;

                Light light = pointLights[i].pointLight;
                if (light == null || !light.isActiveAndEnabled || light.type != LightType.Point) continue;
                Vector3 pos = light.transform.position;
                float range = light.range;
                range *= fogPointLight.inscattering * inscattering / 25f; // note: 25 comes from Unity point light attenuation equation

                // if point light is behind camera and beyond the range, ignore it
                if (isExcludingLightsBehind) {
                    Vector3 toLight = pos - trackingCenterPosition;
                    float dot = Vector3.Dot(trackingCenterForward, pos - trackingCenterPosition);
                    if (dot < 0 && toLight.sqrMagnitude > range * range) {
                        continue;
                    }
                }

                // add light to the buffer if intensity is enough
                float multiplier = light.intensity * fogPointLight.intensity * intensity;

                if (range > 0 && multiplier > 0) {
                    pointLightPositionBuffer[k].x = pos.x;
                    pointLightPositionBuffer[k].y = pos.y;
                    pointLightPositionBuffer[k].z = pos.z;
                    pointLightPositionBuffer[k].w = 0;
                    Color color = light.color;
                    pointLightColorBuffer[k].x = color.r * multiplier;
                    pointLightColorBuffer[k].y = color.g * multiplier;
                    pointLightColorBuffer[k].z = color.b * multiplier;
                    pointLightColorBuffer[k].w = range;
                    k++;
                }
            }

            Shader.SetGlobalVectorArray(ShaderParams.PointLightColors, pointLightColorBuffer);
            Shader.SetGlobalVectorArray(ShaderParams.PointLightPositions, pointLightPositionBuffer);
            Shader.SetGlobalFloat(ShaderParams.PointLightInsideAtten, insideAtten);
            Shader.SetGlobalInt(ShaderParams.PointLightCount, k);
        }


        public void RegisterPointLight(FogPointLight fogPointLight) {
            if (fogPointLight != null) {
                pointLights.Add(fogPointLight);
                requireRefresh = true;
            }
        }

        public void UnregisterPointLight(FogPointLight fogPointLight) {
            if (fogPointLight != null && pointLights.Contains(fogPointLight)) {
                pointLights.Remove(fogPointLight);
                requireRefresh = true;
            }
        }



        /// <summary>
        /// Look for nearest point lights
        /// </summary>
        public void TrackPointLights(bool forceImmediateUpdate = false) {

            // Look for new lights?
            if (pointLights != null && pointLights.Count > 0 && (forceImmediateUpdate || !Application.isPlaying || (distanceSortTimeInterval > 0 && Time.time - distanceSortLastTime > distanceSortTimeInterval))) {
                if (trackingCenter != null) {
                    distanceSortLastTime = Time.time;
                    pointLights.Sort(pointLightsDistanceComparer);
                }
            }
        }


        int pointLightsDistanceComparer(FogPointLight l1, FogPointLight l2) {
            float dist1 = (l1.transform.position - trackingCenter.position).sqrMagnitude;
            float dist2 = (l2.transform.position - trackingCenter.position).sqrMagnitude;
            if (dist1 < dist2) return -1;
            if (dist1 > dist2) return 1;
            return 0;
        }


        public void Refresh() {
            requireRefresh = true;
        }


    }

}