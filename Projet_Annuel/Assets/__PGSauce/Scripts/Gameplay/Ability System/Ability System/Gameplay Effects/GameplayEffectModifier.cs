using System;
using PGSauce.Gameplay.AbilitySystem.Attribute_System;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    [Serializable]
    public struct GameplayEffectModifier
    {
        public AttributeData attribute;
        public EAttributeModifier modifier;
        public ModifierMagnitude modifierMagnitude;
    }
    
    public enum EAttributeModifier
    {
        Add, Multiply, Override
    }
}