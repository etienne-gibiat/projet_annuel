using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.Strings;
using PGSauce.Gameplay.AbilitySystem.Ability_System;

namespace PGSauce.Gameplay.AbilitySystem
{
    [CreateAssetMenu(menuName = MenuPaths.MenuBase + "Ability System/Abilities/Stat Initialisation")]
    public class InitializeStatsAbilityData : AbilityData
    {
        [SerializeField] private GameplayEffectData[] initialisationGameplayEffects;

        public GameplayEffectData[] InitialisationGameplayEffects => initialisationGameplayEffects;

        public override AbilitySpec CreateAbility(Ability_System.AbilitySystem owner)
        {
            var spec = new InitializeStats(this, owner);
            return spec;
        }
        
        public class InitializeStats : AbilitySpec
        {
            public InitializeStats(AbilityData initializeStatsAbilityData, Ability_System.AbilitySystem owner) : base(initializeStatsAbilityData, owner)
            {
            }

            protected override void CancelAbilityCustom()
            {
            }

            protected override bool CheckGameplayTags()
            {
                return HasRequiredTags(Owner);
            }

            protected override IEnumerator PreActivate()
            {
                yield return null;
            }

            protected override IEnumerator ActivateAbility()
            {
                //TODO refactor
                if (Ability.CoolDown)
                {
                    var cd = Owner.CreateGameplayEffectSpec(Ability.CoolDown, Level);
                    Owner.ApplyGameplayEffectToSelf(cd);
                }

                if (Ability.Cost)
                {
                    var cost = Owner.CreateGameplayEffectSpec(Ability.CoolDown, Level);
                    Owner.ApplyGameplayEffectToSelf(cost);
                }

                //TODO use genericity
                var so = Ability as InitializeStatsAbilityData;
                Owner.AttributesManagerInstance.UpdateAttributes();

                foreach (var gameplayEffect in so.InitialisationGameplayEffects)
                {
                    var spec = Owner.CreateGameplayEffectSpec(gameplayEffect, Level);
                    Owner.ApplyGameplayEffectToSelf(spec);
                    Owner.AttributesManagerInstance.UpdateAttributes();
                }
                
                yield break;
            }
        }
    }
}
