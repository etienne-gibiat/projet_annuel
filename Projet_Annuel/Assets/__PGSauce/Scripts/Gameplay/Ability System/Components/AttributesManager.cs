using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem.Attribute_System
{
    public class AttributesManager : MonoBehaviour
    {
        [SerializeField] private AttributeEventHandler[] attributeEventHandlers;
        [SerializeField] private List<AttributeData> attributes;
        
        private List<AttributeValue> _attributeValues;
        private bool _attributeDictStale;
        private readonly Dictionary<AttributeData, int> _attributeIndexes = new Dictionary<AttributeData, int>();

        public Dictionary<AttributeData, int> AttributeIndexes
        {
            get
            {
                if (!_attributeDictStale) return _attributeIndexes;
                _attributeIndexes.Clear();
                for (var i = 0; i < AttributeValues.Count; i++)
                {
                    _attributeIndexes.Add(AttributeValues[i].attribute, i);
                }

                _attributeDictStale = false;

                return _attributeIndexes;
            }
        }

        private List<AttributeValue> AttributeValues => _attributeValues;
        public ReadOnlyCollection<AttributeValue> AttributeValuesReadOnly => AttributeValues.AsReadOnly();

        private void Awake()
        {
            InitializeAttributesValues();
            SetAttributesDirty();
            
        }
        
        private void Update()
        {
            UpdateAttributes();
        }

        public bool UpdateAttributeModifier(AttributeData attribute, CharacterStat modifier, out AttributeValue value)
        {
            return SetAttributeModifiers(attribute, modifier, out value);
        }

        private bool SetAttributeModifiers(AttributeData attributeData, CharacterStat modifier, out AttributeValue value)
        {
            if (!TryGetAttributeValue(attributeData, out value, out var index)) {return false;}
            value.Combine(modifier);
            return true;

        }

        public void AddAttributes(params AttributeData[] attributeDatas)
        {
            foreach (var attData in attributeDatas)
            {
                if (AttributeIndexes.ContainsKey(attData))
                {
                    continue;
                }

                attributes.Add(attData);
                AttributeIndexes.Add(attData, attributes.Count - 1);
            }

            SetAttributesDirty();
        }
        
        public void RemoveAttributes(params AttributeData[] attributeDatas)
        {
            foreach (var attributeData in attributeDatas)
            {
                attributes.Remove(attributeData);
            }
            
            SetAttributesDirty();
        }

        public bool TryGetAttributeValue(AttributeData attributeData, out AttributeValue value, out int index)
        {
            if (AttributeIndexes.TryGetValue(attributeData, out index))
            {
                value = AttributeValues[index];
                return true;
            }

            value = new AttributeValue();
            return false;
        }
        
        public void UpdateAttributes()
        {
            foreach (var eventHandler in attributeEventHandlers)
            {
                eventHandler.PreAttributeChange(this, AttributeValues);
            }
        }

        private void InitializeAttributesValues()
        {
            _attributeValues = new List<AttributeValue>();
            foreach (var att in attributes)
            {
                var newAttribute = new AttributeValue()
                {
                    attribute = att
                };
                AttributeValues.Add(newAttribute);
            }
        }

        private void SetAttributesDirty()
        {
            _attributeDictStale = true;
        }

    }
}