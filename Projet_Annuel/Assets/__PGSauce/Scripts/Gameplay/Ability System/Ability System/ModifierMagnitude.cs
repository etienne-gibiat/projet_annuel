using System;
using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    [Serializable]
    public class ModifierMagnitude
    {
        [SerializeField] private ModifierMagnitudeData magnitude;
        [SerializeField] private float multiplier;

        public void Init(GameplayEffectSpec spec)
        {
            magnitude.Init(spec);
        }

        public float CalculateMagnitude(GameplayEffectSpec spec)
        {
            return magnitude.CalculateMagnitude(spec, multiplier);
        }
    }
}