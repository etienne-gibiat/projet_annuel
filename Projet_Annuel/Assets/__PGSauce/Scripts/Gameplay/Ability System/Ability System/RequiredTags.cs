using System;
using PGSauce.Gameplay.AbilitySystem.Gameplay_Tags;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    [Serializable]
    public class RequiredTags
    {
        /// <summary>
        /// All of these tags must be present
        /// </summary>
        public GameplayTagData[] requiredTags;

        /// <summary>
        /// None of these tags can be present
        /// </summary>
        public GameplayTagData[] ignoredTags;
    }
}