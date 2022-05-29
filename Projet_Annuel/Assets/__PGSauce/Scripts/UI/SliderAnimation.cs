using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PGSauce.Animation;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Utilities;
using PGSauce.Unity;
using UnityEngine.UI;

namespace PGSauce.UI
{
    public class SliderAnimation : PGMonoBehaviour
    {
        #region Public And Serialized Fields

        [SerializeField] private SliderAnimationData sliderAnimationData;
        [SerializeField] private Slider slider;
        #endregion
        #region Private Fields

        private Tween _sliderAnimation;
        #endregion
        #region Properties
        #endregion
        #region Unity Functions
        public void Awake()
        {
        }
        
        public void Start()
        {
        }
        
        public void Update()
        {
        }
        
        public void OnEnable()
        {
        }
        
        public void OnDisable()
        {
        }
        
        public void OnDestroy()
        {
        }
        
        #endregion
        #region Public Methods

        public void DoSliderAnimation()
        {
            _sliderAnimation?.Kill();
            var targetValue = sliderAnimationData.GetValueFromSlider(slider);
            slider.value = slider.minValue;
            _sliderAnimation = slider.DOValue(targetValue, sliderAnimationData.SliderDotweenProfile.Duration)
                .SetAs(sliderAnimationData.SliderDotweenProfile.Params)
                .OnComplete(() =>
                {
                    _sliderAnimation = null;
                });
        }
        
        public void ResetSlider()
        {
            _sliderAnimation?.Kill();
            _sliderAnimation = null;
            slider.value = slider.minValue;
        }

        #endregion
        #region Private Methods
        #endregion
    }
}
