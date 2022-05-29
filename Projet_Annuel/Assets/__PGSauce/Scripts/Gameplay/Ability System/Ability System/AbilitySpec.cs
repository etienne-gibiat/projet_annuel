using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PGSauce.Gameplay.AbilitySystem.Gameplay_Tags;
using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    public abstract class AbilitySpec
    {
        public AbilityData Ability { get; }
        public AbilitySystem Owner { get; set; }
        public float Level => Owner.Level;
        public bool IsActive { get; private set; }
        public Coroutine AbilityRoutine { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is AbilitySpec spec)) return false;
            return spec.Ability == Ability;
        }

        public override int GetHashCode()
        {
            return Ability.GetHashCode();
        }

        protected AbilitySpec(AbilityData ability, AbilitySystem owner)
        {
            Ability = ability;
            Owner = owner;
        }
        
        public IEnumerator TryActivateAbility(Action onFinished)
        {
            if (!CanActivateAbility())
            {
                onFinished();
                yield break;
            }
            
            IsActive = true;
            yield return PreActivate();
            yield return ActivateAbility();
            EndAbility();
            onFinished();
        }

        public bool CanActivateAbility()
        {
            return !IsActive
                   && CheckGameplayTags()
                   && CheckCost()
                   && IsCoolDownFinished;
        }

        public bool IsCoolDownFinished => CheckCooldown().TimeRemaining <= 0;

        protected bool HasRequiredTags(AbilitySystem abilitySystem)
        {
            return HasAllTags(abilitySystem, Ability.Tags.OwnerTags.requiredTags) &&
                   HasNoneTags(abilitySystem, Ability.Tags.OwnerTags.ignoredTags);
        }

        protected bool HasAllTags(AbilitySystem abilitySystem, IEnumerable<GameplayTagData> tags)
        {
            if (!abilitySystem)
            {
                return true;
            }

            foreach (var abilityTag in tags)
            {
                var ok = false;
                foreach (var grantedTags in abilitySystem.AppliedEffects.Select(appliedEffect => appliedEffect.GrantedTags))
                {
                    ok = grantedTags.Any(tag => tag == abilityTag);
                    if (ok)
                    {
                        break;
                    }
                }

                if (!ok)
                {
                    return false;
                }
            }

            return true;
        }
        
        protected bool HasNoneTags(AbilitySystem abilitySystem, IEnumerable<GameplayTagData> tags)
        {
            if (!abilitySystem)
            {
                return true;
            }

            foreach (var abilityTag in tags)
            {
                if (abilitySystem.AppliedEffects.Select(appliedEffect => appliedEffect.GrantedTags).Any(grantedTags => grantedTags.Any(tag => tag == abilityTag)))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckCost()
        {
            if (Ability.Cost == null)
            {
                return true;
            }

            var spec = Owner.CreateGameplayEffectSpec(Ability.Cost, Level);
            if (spec.GameplayEffect.GameplayEffectDefinition.durationType != DurationType.Instant)
            {
                return true;
            }

            foreach (var modifier in spec.GameplayEffect.GameplayEffectDefinition.modifiers)
            {
                if (modifier.modifier != EAttributeModifier.Add)
                {
                    continue;
                }

                var costValue = modifier.modifierMagnitude.CalculateMagnitude(spec);

                if (!Owner.AttributesManagerInstance.TryGetAttributeValue(modifier.attribute, out var attributeValue,
                    out _))
                {
                    continue;
                }
                if (attributeValue.CurrentValue + costValue < 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void CancelAbility()
        {
            //TODO Stop coroutine here (replace with dotween)
            CancelAbilityCustom();
        }

        protected abstract void CancelAbilityCustom();
        protected abstract bool CheckGameplayTags();
        protected abstract IEnumerator PreActivate();
        protected abstract IEnumerator ActivateAbility();
        
        public AbilityCoolDown CheckCooldown()
        {
            var maxDuration = 0f;
            if (Ability.CoolDown == null) return new AbilityCoolDown();
            var cooldownTags = Ability.CoolDown.EffectTags.GrantedTags;

            var longestCooldown = 0f;

            // Check if the cooldown tag is granted to the player, and if so, capture the remaining duration for that tag
            foreach (var gameplayEffect in Owner.AppliedEffects)
            {
                var grantedTags = gameplayEffect.GrantedTags;
                foreach (var grantedTag in grantedTags)
                {
                    foreach (var coolDownTag in cooldownTags)
                    {
                        if (grantedTag == coolDownTag)
                        {
                            // If this is an infinite GE, then return null to signify this is on CD
                            if (gameplayEffect.DurationType == DurationType.Infinite) {
                                
                                return new AbilityCoolDown
                                {
                                    TimeRemaining = float.MaxValue,
                                    TotalDuration = 0
                                };
                            }

                            var durationRemaining = gameplayEffect.DurationRemaining;

                            if (durationRemaining > longestCooldown)
                            {
                                longestCooldown = durationRemaining;
                                maxDuration = gameplayEffect.TotalDuration;
                            }
                        }
                    }
                }
            }

            return new AbilityCoolDown
            {
                TimeRemaining = longestCooldown,
                TotalDuration = maxDuration
            };
        }

        private void EndAbility()
        {
            IsActive = false;
        }
    }
}