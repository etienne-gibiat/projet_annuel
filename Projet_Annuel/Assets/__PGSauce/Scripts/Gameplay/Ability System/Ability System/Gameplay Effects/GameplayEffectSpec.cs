using System;
using System.Collections;
using PGSauce.Gameplay.AbilitySystem.Gameplay_Tags;
using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    [Serializable]
    public class GameplayEffectSpec
    {
        private GameplayEffectSpec(GameplayEffectData gameplayEffect, AbilitySystem source, float level)
        {
            GameplayEffect = gameplayEffect;
            Source = source;
            for (int i = 0; i < GameplayEffect.GameplayEffectDefinition.modifiers.Length; i++)
            {
                GameplayEffect.GameplayEffectDefinition.modifiers[i].modifierMagnitude.Init(this);
            }

            Level = level;
            DurationRemaining = GameplayEffect.GameplayEffectDefinition.CalculateDuration(this);
            TotalDuration = DurationRemaining;

            TimeUntilPeriodTick = GameplayEffect.Period.period;

            if (gameplayEffect.Period.executeImmediately)
            {
                TimeUntilPeriodTick = 0f;
            }
        }

        public float Level { get; set; }

        public AbilitySystem Source { get; set; }

        public GameplayEffectData GameplayEffect { get; private set; }
        public float DurationRemaining { get; private set; }
        public float TotalDuration { get; private set; }
        public float TimeUntilPeriodTick { get; private set; }
        
        public GameplayTagData[] RequiredTags => GameplayEffect.EffectTags.RequiredTags.requiredTags;
        public GameplayTagData[] IgnoredTags => GameplayEffect.EffectTags.RequiredTags.ignoredTags;
        public DurationType DurationType => GameplayEffect.GameplayEffectDefinition.durationType;
        public GameplayEffectModifier[] Modifiers => GameplayEffect.GameplayEffectDefinition.modifiers;

        public static GameplayEffectSpec CreateNew(GameplayEffectData gameplayEffect, AbilitySystem source, float level)
        {
            return new GameplayEffectSpec(gameplayEffect, source, level);
        }

        public void UpdateRemainingDuration(float deltaTime)
        {
            DurationRemaining = Mathf.Clamp(DurationRemaining - deltaTime, 0f, TotalDuration);
        }

        public void TickPeriodic(float deltaTime, out bool execute)
        {
            TimeUntilPeriodTick -= deltaTime;
            execute = false;
            if (!(TimeUntilPeriodTick <= 0)) {return;}
            TimeUntilPeriodTick = GameplayEffect.Period.period;
            if (GameplayEffect.Period.StrictlyPositivePeriod)
            {
                execute = true;
            }
        }
    }
}