using System;
using DG.Tweening;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Animation
{
    /// <summary>
    /// Used to shake a transform
    /// </summary>
    [Serializable]
    public class DotweenShakeProfile
    {
        public float shakeDuration;
        /// <summary>
        /// IF true shake in all axis, else you choose the direction
        /// </summary>
        [CalledByOdin]
        public bool uniformShake;
        /// <summary>
        /// The uniform shake strength, used when uniformShake is true
        /// </summary>
        [ShowIf("@uniformShake")]
        public float shakeUniformStrength;
        /// <summary>
        /// The NON uniform shake strength, used when uniformShake is true
        /// </summary>
        [ShowIf("@!uniformShake")]
        public Vector3 shakeStrength;
        /// <summary>
        /// How much will the shake vibrate
        /// </summary>
        [SerializeField, Min(0)] private int vibrato = 10;
        /// <summary>
        /// Indicates how much the shake will be random (0 to 180 - values higher than 90 kind of suck, so beware). Setting it to 0 will shake along a single direction.
        /// </summary>
        [SerializeField, Range(0, 90)] private float randomness = 90f;
        
        public Tween ShakeRotation(Transform tfm)
        {
            if (uniformShake)
            {
                return tfm.DOShakeRotation(shakeDuration, shakeUniformStrength, vibrato, randomness);
            }

            return tfm.DOShakeRotation(shakeDuration, shakeStrength, vibrato, randomness);
        }

        public Tween ShakePosition(Transform tfm)
        {
            if (uniformShake)
            {
                return tfm.DOShakePosition(shakeDuration, shakeUniformStrength, vibrato, randomness);
            }

            return tfm.DOShakePosition(shakeDuration, shakeStrength, vibrato, randomness);
        }

        public Tween ShakeScale(Transform tfm)
        {
            if (uniformShake)
            {
                return tfm.DOShakeScale(shakeDuration, shakeUniformStrength, vibrato, randomness);
            }

            return tfm.DOShakeScale(shakeDuration, shakeStrength, vibrato, randomness);
        }
    }
}