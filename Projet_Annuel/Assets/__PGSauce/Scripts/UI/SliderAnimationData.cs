using PGSauce.Animation;
using PGSauce.Core.Strings;
using PGSauce.Core.Utilities;
using PGSauce.Core.Utilities.Float_Providers;
using UnityEngine;
using UnityEngine.UI;

namespace PGSauce.UI
{
    [CreateAssetMenu(menuName = MenuPaths.UI + "Slider Animation")]
    public class SliderAnimationData : ScriptableObject
    {
        [SerializeField] private Float01Provider sliderValue;
        [SerializeField] private DotweenProfile sliderDotweenProfile;

        public Float01 SliderNormalizedValue => sliderValue.GetValue();

        public float GetValueFromSlider(Slider slider)
        {
            return Mathf.Lerp(slider.minValue, slider.maxValue, SliderNormalizedValue);
        }

        public DotweenProfile SliderDotweenProfile => sliderDotweenProfile;
    }
}