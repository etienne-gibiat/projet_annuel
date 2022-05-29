using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    [Serializable]
    public class GameplayEffectDefinition
    {
        public DurationType durationType;
        [SerializeField, ShowIf("HasDuration")] private ModifierMagnitude durationModifierMagnitude;
        public GameplayEffectModifier[] modifiers;

        private bool HasDuration => durationType == DurationType.HasDuration;

        public float CalculateDuration(GameplayEffectSpec gameplayEffectSpec)
        {
            if (HasDuration)
            {
                return durationModifierMagnitude.CalculateMagnitude(gameplayEffectSpec);
            }

            return 0f;
        }
    }
}