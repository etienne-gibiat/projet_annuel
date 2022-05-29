using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.Strings;
using PGSauce.Gameplay.AbilitySystem.Ability_System;

namespace PGSauce.Gameplay.AbilitySystem
{
    [CreateAssetMenu(menuName = MenuPaths.MenuBase + "Ability System/Abilities/Examples/Throw Projectile")]
    public class ThrowProjectileAbilityData : AbilityData
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private GameplayEffectData onHitEffect;
        public override AbilitySpec CreateAbility(Ability_System.AbilitySystem owner)
        {
            var spec = new ThrowProjectileAbilitySpec(this, owner, projectilePrefab, onHitEffect);
            return spec;
        }

        public class ThrowProjectileAbilitySpec : AbilitySpec
        {
            public ThrowProjectileAbilitySpec(AbilityData ability, Ability_System.AbilitySystem owner,
                Projectile projectile, GameplayEffectData gameplayEffectData) : base(ability, owner)
            {
                CastOrigin = owner.GetComponent<CastOrigin>();
                ProjectilePrefab = projectile;
                OnHitEffect = gameplayEffectData;
            }

            public GameplayEffectData OnHitEffect { get; set; }

            public Projectile ProjectilePrefab { get; set; }
            public CastOrigin CastOrigin { get; set; }

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
                var cd = Owner.CreateGameplayEffectSpec(Ability.CoolDown, Level);
                var cost = Owner.CreateGameplayEffectSpec(Ability.Cost, Level);

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    var target = hit.collider.GetComponentInParent<Ability_System.AbilitySystem>();
                    if (!target)
                    {
                        yield break;
                    }

                    var proj = Instantiate(ProjectilePrefab, CastOrigin.Position, Quaternion.identity);
                    proj.Target = target;
                    Owner.ApplyGameplayEffectToSelf(cd);
                    Owner.ApplyGameplayEffectToSelf(cost);
                    yield return proj.TravelToTarget();
                    var spec = Owner.CreateGameplayEffectSpec(OnHitEffect, Level);
                    target.ApplyGameplayEffectToSelf(spec);
                    Destroy(proj.gameObject);
                }
            }
        }
    }
}
