using PGSauce.Gameplay.AbilitySystem.Attribute_System;
using PGSauce.Gameplay.AbilitySystem.Gameplay_Tags;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    public class GameplayEffectContainer
    {
        public GameplayEffectSpec Spec { get; set; }
        public ModifierContainer[] Modifiers { get; set; }

        public GameplayTagData[] GrantedTags => Spec.GameplayEffect.EffectTags.GrantedTags;
        public DurationType DurationType => Spec.GameplayEffect.GameplayEffectDefinition.durationType;
        public float DurationRemaining => Spec.DurationRemaining;
        public float TotalDuration => Spec.TotalDuration;
        public bool HasExpired => Spec.DurationType == DurationType.HasDuration && Spec.DurationRemaining <= 0;

        public class ModifierContainer
        {
            public AttributeData AttributeData { get; set; }
            public CharacterStat Modifier { get; set; }
        }
    }
}