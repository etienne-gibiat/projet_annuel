using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.Strings;
using PGSauce.Gameplay.AbilitySystem.Ability_System;

namespace PGSauce.Gameplay.AbilitySystem
{
    [CreateAssetMenu(menuName = MenuPaths.MenuBase + "Ability System/Gameplay Effect/Modifier Magnitude/Simple Float")]
    public class AnimationCurveModifierMagnitude : ModifierMagnitudeData
    {
        [SerializeField] private AnimationCurve curve;
        public override void Init(GameplayEffectSpec spec)
        {
        }

        protected override float CalculateMagnitude(GameplayEffectSpec spec)
        {
            return curve.Evaluate(spec.Level);
        }
    }
}
