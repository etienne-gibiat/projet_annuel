using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    public abstract class AbilityData : ScriptableObject
    {
        [SerializeField] private AbilityTags tags;
        [SerializeField] private GameplayEffectData cost;
        [SerializeField] private GameplayEffectData coolDown;
        [SerializeField] private Sprite icon;

        public GameplayEffectData CoolDown => coolDown;

        public GameplayEffectData Cost => cost;

        public AbilityTags Tags => tags;

        public Sprite Icon => icon;

        public abstract AbilitySpec CreateAbility(AbilitySystem owner);
    }
}