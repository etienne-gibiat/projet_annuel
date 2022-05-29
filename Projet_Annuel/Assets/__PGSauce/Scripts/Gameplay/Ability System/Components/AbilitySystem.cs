using System;
using System.Collections.Generic;
using System.Linq;
using PGSauce.Gameplay.AbilitySystem.Attribute_System;
using PGSauce.Gameplay.AbilitySystem.Gameplay_Tags;
using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    [RequireComponent(typeof(AttributesManager))]
    public class AbilitySystem : MonoBehaviour
    {
        [SerializeField] private AttributesManager attributesManager;
        [SerializeField] private float level;

        public AttributesManager AttributesManagerInstance => attributesManager;

        public List<GameplayEffectContainer> AppliedEffects => _appliedEffects;

        public List<AbilitySpec> GrantedAbilities => _grantedAbilities;

        public float Level => level;

        private List<GameplayEffectContainer> _appliedEffects;
        private List<AbilitySpec> _grantedAbilities;

        private void Awake()
        {
            _appliedEffects = new List<GameplayEffectContainer>();
            _grantedAbilities = new List<AbilitySpec>();
            if (attributesManager == null)
            {
                attributesManager = GetComponent<AttributesManager>();
                if (attributesManager == null)
                {
                    throw new UnityException("Attribute Manager is not set !!");
                }
            }
        }

        private void Update()
        {
            //AttributesManagerInstance.ResetAttributeModifiers();
            UpdateSystem();
            TickGameplayEffects();
            CleanGameplayEffects();
        }

        public AbilitySpec GrantAbility(AbilityData abilityData)
        {
            var abilitySpec = abilityData.CreateAbility(this);
            if (GrantedAbilities.Contains(abilitySpec))
            {
                return abilitySpec;
            }
            GrantedAbilities.Add(abilitySpec);
            return abilitySpec;
        }
        public GameplayEffectSpec CreateGameplayEffectSpec(GameplayEffectData gameplayEffect, float level)
        {
            return GameplayEffectSpec.CreateNew(gameplayEffect, this, level);
        }

        public void RemoveAbilitiesWithTag(GameplayTagData gameplayTag)
        {
            for (var i = GrantedAbilities.Count - 1; i >= 0; i--)
            {
                if (GrantedAbilities[i].Ability.Tags.GameplayTag == gameplayTag)
                {
                    GrantedAbilities.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns>If the effects has been applied</returns>
        public bool ApplyGameplayEffectToSelf(GameplayEffectSpec spec)
        {
            if(spec == null){return false;}
            var tagsRequirement = CheckTagRequirementsMet(spec);
            if (!tagsRequirement)
            {
                return false;
            }

            switch (spec.DurationType)
            {
                case DurationType.HasDuration:
                case DurationType.Infinite:
                    ApplyDurationalGameplayEffectToSelf(spec);
                    break;
                case DurationType.Instant:
                    ApplyInstantGameplayEffectToSelf(spec);
                    break;
            }

            return true;
        }

        private void ApplyDurationalGameplayEffectToSelf(GameplayEffectSpec spec)
        {
            var modifiersToApply = new List<GameplayEffectContainer.ModifierContainer>();
            foreach (var modifier in spec.Modifiers)
            {
                var magnitude = modifier.modifierMagnitude.CalculateMagnitude(spec);
                var attributeModifier = new CharacterStat();
                switch (modifier.modifier)
                {
                    case EAttributeModifier.Add:
                        attributeModifier.AddModifier(new StatModifier(magnitude, StatModType.Flat));
                        break;
                    case EAttributeModifier.Multiply:
                        attributeModifier.AddModifier(new StatModifier(magnitude, StatModType.PercentMult));
                        break;
                    case EAttributeModifier.Override:
                        attributeModifier.BaseValue = magnitude;
                        break;
                }
                modifiersToApply.Add(new GameplayEffectContainer.ModifierContainer
                {
                    AttributeData = modifier.attribute,
                    Modifier = attributeModifier
                });
            }
            
            AppliedEffects.Add(new GameplayEffectContainer
            {
                Spec = spec,
                Modifiers = modifiersToApply.ToArray()
            });
        }

        private void ApplyInstantGameplayEffectToSelf(GameplayEffectSpec spec)
        {
            foreach (var modifier in spec.Modifiers)
            {
                var magnitude = modifier.modifierMagnitude.CalculateMagnitude(spec);
                var attribute = modifier.attribute;
                if(! AttributesManagerInstance.TryGetAttributeValue(attribute, out var value, out _)){return;}

                switch (modifier.modifier)
                {
                    case EAttributeModifier.Add:
                        value.AddModifier(new StatModifier(magnitude, StatModType.Flat));
                        break;
                    case EAttributeModifier.Multiply:
                        value.AddModifier(new StatModifier(magnitude, StatModType.PercentMult));
                        break;
                    case EAttributeModifier.Override:
                        value.Clear();
                        value.BaseValue = magnitude;
                        break;
                }
            }
        }

        private bool CheckTagRequirementsMet(GameplayEffectSpec spec)
        {
            var appliedTags = new List<GameplayTagData>();
            foreach (var appliedEffect in AppliedEffects)
            {
                appliedTags.AddRange(appliedEffect.GrantedTags);
            }

            if (spec.RequiredTags.Any(requiredTag => !appliedTags.Contains(requiredTag)))
            {
                return false;
            }

            if (spec.IgnoredTags.Any(ignoreTag => appliedTags.Contains(ignoreTag)))
            {
                return false;
            }

            return true;
        }

        private void UpdateSystem()
        {
            foreach (var appliedEffect in AppliedEffects)
            {
                var modifiers = appliedEffect.Modifiers;
                foreach (var modifier in modifiers)
                {
                    AttributesManagerInstance.UpdateAttributeModifier(modifier.AttributeData, modifier.Modifier, out _);
                }
            }
        }

        private void TickGameplayEffects()
        {
            foreach (var appliedEffect in AppliedEffects)
            {
                var spec = appliedEffect.Spec;
                if(spec.DurationType != DurationType.HasDuration){continue;}

                spec.UpdateRemainingDuration(Time.deltaTime);
                spec.TickPeriodic(Time.deltaTime, out bool execute);

                if (execute)
                {
                    ApplyInstantGameplayEffectToSelf(spec);
                }
            }
        }

        private void CleanGameplayEffects()
        {
            AppliedEffects.RemoveAll(gameplayEffect => gameplayEffect.HasExpired);
        }
    }
}