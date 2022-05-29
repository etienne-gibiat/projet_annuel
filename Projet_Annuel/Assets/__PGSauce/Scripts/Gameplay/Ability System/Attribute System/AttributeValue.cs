using System;

namespace PGSauce.Gameplay.AbilitySystem.Attribute_System
{
    [Serializable]
    public class AttributeValue
    {
        public AttributeData attribute;

        private CharacterStat _characterStat;
        private CharacterStat CharacterStatInstance
        {
            get
            {
                return _characterStat;
            }

            set
            {
                _characterStat = value;
            }
        }

        public AttributeValue()
        {
            CharacterStatInstance = new CharacterStat();
        }

        public float BaseValue
        {
            get => CharacterStatInstance.BaseValue;
            set => CharacterStatInstance.BaseValue = value;
        }

        public float CurrentValue => CharacterStatInstance.Value;

        public void Combine(CharacterStat modifier)
        {
            CharacterStatInstance.Combine(modifier);
        }

        public void AddModifier(StatModifier modifier)
        {
            CharacterStatInstance.AddModifier(modifier);
        }

        public void Clear()
        {
            CharacterStatInstance.Clear();
        }
    }
}