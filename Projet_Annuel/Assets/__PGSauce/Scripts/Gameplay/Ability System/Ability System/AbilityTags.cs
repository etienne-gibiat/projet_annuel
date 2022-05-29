using System;
using PGSauce.Gameplay.AbilitySystem.Gameplay_Tags;
using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    [Serializable]
    public struct AbilityTags
    {
        [SerializeField] private GameplayTagData gameplayTag;
        [SerializeField] private RequiredTags ownerTags;
        [SerializeField] private RequiredTags sourceTags;
        [SerializeField] private RequiredTags targetTags;

        public GameplayTagData GameplayTag => gameplayTag;

        public RequiredTags OwnerTags => ownerTags;

        public RequiredTags SourceTags => sourceTags;

        public RequiredTags TargetTags => targetTags;
    }
}