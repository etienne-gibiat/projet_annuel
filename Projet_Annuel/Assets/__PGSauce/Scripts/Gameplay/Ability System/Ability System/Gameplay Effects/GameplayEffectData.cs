using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    [CreateAssetMenu(menuName = MenuPaths.MenuBase + "Ability System/Gameplay Effect Definition")]
    public class GameplayEffectData : ScriptableObject
    {
        [SerializeField] private GameplayEffectDefinition gameplayEffect;
        [SerializeField] private GameplayEffectTags gameplayEffectTags;
        [SerializeField] private GameplayEffectPeriod period;

        public GameplayEffectTags EffectTags => gameplayEffectTags;

        public GameplayEffectDefinition GameplayEffectDefinition => gameplayEffect;

        public GameplayEffectPeriod Period => period;
    }
}