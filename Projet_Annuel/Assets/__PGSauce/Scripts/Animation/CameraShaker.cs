using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Strings;
using PGSauce.Core.Utilities;

namespace PGSauce.Animation
{
    /// <summary>
    /// A Script to shake Cinemachine cameras
    /// The cinemachine camera needs a CinemachineBasicMultiChannelPerlin
    /// </summary>
    /// <see cref="CinemachineBasicMultiChannelPerlin"/>
    [HelpURL(DocsPaths.CameraShaker)]
    public class CameraShaker : MonoSingleton<CameraShaker>
    {
        [SerializeField] private CinemachineVirtualCamera cam;
        /// <summary>
        /// Time the Camera Shake effect will last
        /// </summary>
        public float ShakeDuration = 0.3f;
        /// <summary>
        /// The shake distance
        /// </summary>
        public float ShakeAmplitude = 1.2f;
        /// <summary>
        /// The shake frequence
        /// </summary>
        public float ShakeFrequency = 2.0f;

        private CinemachineBasicMultiChannelPerlin noise;

        void Start()
        {
            noise = cam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
            EndShake();
        }

        private bool shaking;

        /// <summary>
        /// Do a short shake
        /// </summary>
        public void ShakeShortly()
        {
            if (shaking) { return; }
            BeginShake();
            DOVirtual.DelayedCall(ShakeDuration, () => EndShake());
            PGDebug.Message("Do Haptic").LogTodo();
        }

        /// <summary>
        /// End the shake
        /// </summary>
        public void EndShake()
        {
            noise.m_AmplitudeGain = 0;
            noise.m_FrequencyGain = 0;
            shaking = false;
        }

        /// <summary>
        /// Start the shake
        /// </summary>
        public void BeginShake()
        {
            noise.m_AmplitudeGain = ShakeAmplitude;
            noise.m_FrequencyGain = ShakeFrequency;
            shaking = true;
        }
    }
}
