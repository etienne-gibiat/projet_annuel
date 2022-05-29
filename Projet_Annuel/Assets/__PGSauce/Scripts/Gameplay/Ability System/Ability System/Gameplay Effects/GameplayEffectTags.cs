using System;
using PGSauce.Gameplay.AbilitySystem.Gameplay_Tags;
using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    [Serializable]
    public struct GameplayEffectTags
    {
        [SerializeField] private GameplayTagData[] grantedTags;
        [SerializeField] private RequiredTags requiredTags;

        public GameplayTagData[] GrantedTags => grantedTags;

        public RequiredTags RequiredTags => requiredTags;
    }
}