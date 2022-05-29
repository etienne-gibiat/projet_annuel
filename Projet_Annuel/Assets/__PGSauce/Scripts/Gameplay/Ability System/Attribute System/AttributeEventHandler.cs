using System.Collections.Generic;
using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem.Attribute_System
{
    public abstract class AttributeEventHandler : ScriptableObject
    {
        public abstract void PreAttributeChange(AttributesManager attributesManager, List<AttributeValue> currentAttributeValues);
    }
}