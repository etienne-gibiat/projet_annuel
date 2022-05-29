using DG.Tweening;
using PGSauce.Animation;
using PGSauce.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.UI
{
    [InlineEditor()]
    public abstract class AnimationSectionProfile<T> : ScriptableObject
    {
        [SerializeField] private float delay;
        [SerializeField] private DotweenProfile dotweenProfile;

        [SerializeField, InfoBox("The value you want it to be set when coming FROM this animation profile")]
        private T comingFromValue;
        [SerializeField, InfoBox("The value you want it to be set when coming TO this animation profile")]
        private T comingToValue;
        public float Delay => delay;
        public Ease Ease => dotweenProfile.Ease;
        public float Duration => dotweenProfile.Duration;

        public T ComingFromValue => comingFromValue;

        public T ComingToValue => comingToValue;
    }
}