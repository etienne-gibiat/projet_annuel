using System;
using DG.Tweening;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Animation
{
    /// <summary>
    /// A class to store an individual tween's params, such as its duration, delay and ease.
    /// It can be applied on multiple tweens.
    /// </summary>
    [Serializable]
    public abstract class DotweenProfileBase
    {
        [SerializeField, Min(0)] private float duration;
        [SerializeField, Min(0)] private float delay;
        [SerializeField] private EaseType easeType = EaseType.DotweenEase;
        [SerializeField, ShowIf("UseDotweenEase"), LabelText("Ease")]  private Ease ease = Ease.Linear;
        [SerializeField, ShowIf("UseAnimationCurves"), LabelText("Ease")] private AnimationCurve animationCurve;
        [SerializeField, BoxGroup("Loop"), CalledByOdin] private bool useLoop;
        [SerializeField, BoxGroup("Loop"), ShowIf("@useLoop")] private int loopCount = 1;
        [SerializeField, BoxGroup("Loop"), ShowIf("@useLoop")] private LoopType loopType = LoopType.Incremental;
        [SerializeField] private bool relative;

        private TweenParams _params;
        
        /// <summary>
        /// The duration of the tween
        /// </summary>
        public float Duration => duration;
        /// <summary>
        /// This is all the params condensed in one object.
        /// How to use : myTween.SetAs(myDotweenProfile.Params);
        /// </summary>
        public TweenParams Params => _params ?? CreateTweenParams();
        [CalledByOdin]
        private bool UseDotweenEase => easeType == EaseType.DotweenEase;
        [CalledByOdin]
        private bool UseAnimationCurves => easeType == EaseType.AnimationCurve;
        
        [Obsolete("Will be removed when the UI Animation is refactored")]
        public Ease Ease => ease;

        private TweenParams CreateTweenParams()
        {
            _params = new TweenParams();

            if (delay > 0)
            {
                _params.SetDelay(delay);
            }

            if (UseDotweenEase)
            {
                _params.SetEase(ease);
            }
            else if (UseAnimationCurves)
            {
                _params.SetEase(animationCurve);
            }

            if (useLoop)
            {
                _params.SetLoops(loopCount, loopType);
            }

            _params.SetRelative(relative);

            return _params;
        }
        
        private enum EaseType
        {
            DotweenEase,
            AnimationCurve
        }
    }
}