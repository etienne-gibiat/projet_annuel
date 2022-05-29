using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    public abstract class ModifierMagnitudeData : ScriptableObject
    {
        public abstract void Init(GameplayEffectSpec spec);
        protected abstract float CalculateMagnitude(GameplayEffectSpec spec);

        public float CalculateMagnitude(GameplayEffectSpec spec, float multiplier)
        {
            return CalculateMagnitude(spec) * multiplier;
        }
    }
}