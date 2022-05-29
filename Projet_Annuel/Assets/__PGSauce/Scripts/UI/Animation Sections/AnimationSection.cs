using System;
using DG.Tweening;
using PGSauce.Core.PGDebugging;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.UI
{
    [Serializable]
    public abstract class AnimationSection<TInput>
    {
        [SerializeField, Tooltip("Should this section animation be enable?")]
        private bool useSection;

        [SerializeField, ShowIf("@useSection")] private AnimationSectionProfile<TInput> hideAnimation;
        [SerializeField, ShowIf("@useSection")] private AnimationSectionProfile<TInput> showAnimation;

        public TInput ComingFromShownValue { get; set; }
        public TInput ComingToShownValue { get; set; }
        public TInput ComingFromHiddenValue { get; set; }
        public TInput ComingToHiddenValue { get; set; }
        public Tween MotionTween { get; set; }
        public bool UseSection => useSection;
        public float HidingDuration => HideAnimation == null ? 0 : HideAnimation.Duration;
        public float ShowAfter => ShowAnimation == null ? 0 : ShowAnimation.Delay;
        public float HideAfter => HideAnimation == null ? 0 : HideAnimation.Delay;
        public float ShowingDuration => ShowAnimation == null ? 0 : ShowAnimation.Duration;
        public Ease HideEase => HideAnimation == null ? Ease.Unset : HideAnimation.Ease;
        public Ease ShowEase => ShowAnimation == null ? Ease.Unset : ShowAnimation.Ease;

        public AnimationSectionProfile<TInput> HideAnimation => hideAnimation;

        public AnimationSectionProfile<TInput> ShowAnimation => showAnimation;

        protected abstract TInput Lerp(TInput startVal, TInput endVal, float t);

        public override string ToString()
        {
            return
                $"Section : CFH {ComingFromHiddenValue}, CTH {ComingToHiddenValue}, CFS {ComingFromShownValue}, CTS {ComingToShownValue}";
        }

        public void Init()
        {
            ComingFromHiddenValue = HideAnimation == null ? ComingFromHiddenValue : HideAnimation.ComingFromValue;
            ComingToHiddenValue = HideAnimation == null ? ComingToHiddenValue : HideAnimation.ComingToValue;
            ComingFromShownValue = ShowAnimation == null ? ComingFromShownValue : ShowAnimation.ComingFromValue;
            ComingToShownValue = ShowAnimation == null ? ComingToShownValue : ShowAnimation.ComingToValue;
        }
        
        public void KillTween()
        {
            MotionTween?.Kill();
            MotionTween = null;
        }
        
        public float CheckGreatestHidingTime(float hideTime)
        {
            if (!useSection)
            {
                return hideTime;
            }
            var duration = HidingDuration > 0 ? HidingDuration : 0;
            duration += HideAfter;
            return duration > hideTime ? duration : hideTime;
        }

        public float CheckGreatestShowingTime(float showTime)
        {
            if (!useSection)
            {
                return showTime;
            }
            var duration = ShowingDuration > 0 ? ShowingDuration : 0;
            duration += ShowAfter;
            return duration > showTime ? duration : showTime;
        }
        
        public Ease GetMotionType(bool visible)
        {
            return visible ? ShowEase : HideEase;
        }
        
        public float GetSectionDuration(bool visible)
        {
            var hDuration = HidingDuration < 0
                ? 0
                : HidingDuration;
            var sDuration = ShowingDuration < 0
                ? 0
                : ShowingDuration;

            return visible ? sDuration : hDuration;
        }

        public void Control(GameObject caller, Action<TInput> output, bool visible, bool instantStart)
        {
            var startVal = GetStartValue(visible);
            var endVal = GetEndValue(visible);
            
            var duration = GetSectionDuration(visible);
            
            MotionTween?.Kill();
            output(startVal);
            
            if (!caller.activeInHierarchy || duration == 0f)
            {
                output(endVal);
                return;
            }

            MotionTween = Motion(output, startVal, endVal, visible, instantStart);
        }

        public TInput GetEndValue(bool visible)
        {
            return visible ? ComingToShownValue : ComingToHiddenValue;
        }

        public TInput GetStartValue(bool visible)
        {
            return visible ? ComingFromHiddenValue : ComingFromShownValue;
        }

        private Tween Motion(Action<TInput> output, TInput startVal, TInput endVal, bool visible, bool instantStart)
        {
            return FloatMotion(value =>
            {
                var lerped = Lerp(startVal, endVal, value);
                output(lerped);
            }, 0, 1, visible, instantStart);
        }

        private Tween FloatMotion(Action<float> output, float startVal, float endVal, bool visible, bool instantStart)
        {
            var duration = GetSectionDuration(visible);
            var ease = GetMotionType(visible);
            var sequence = DOTween.Sequence();

            if (!instantStart && Time.timeScale != 0)
            {
                var delay = visible ? (ShowAfter < 0 ? 0 : ShowAfter)
                    : HideAfter < 0 ? 0 : HideAfter;
                sequence.AppendInterval(delay);
            }

            sequence.Append(

                DOVirtual.Float(0, 1, duration, value =>
                    {
                        var computed = Mathf.LerpUnclamped(startVal, endVal, value);
                        output(computed);
                    })
                    .SetEase(ease)
            );

            sequence.AppendCallback(() => sequence = null);

            sequence.SetUpdate(true);

            return sequence;
        }
    }
}