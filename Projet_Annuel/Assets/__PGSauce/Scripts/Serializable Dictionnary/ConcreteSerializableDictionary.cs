using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;

namespace PGSauce.Core.SerializableDictionaries
{
    [Serializable]
    public class ConcreteSerializableDictionary <TKey, TValue>: SerializableDictionary<TKey, TValue, TValue>
    {
        [HideLabel] public ConcreteSerializableDictionary() {}
        public ConcreteSerializableDictionary(IDictionary<TKey, TValue> dict) : base(dict) {}
        protected ConcreteSerializableDictionary(SerializationInfo info, StreamingContext context) : base(info, context) {}
        
        protected override void SetValue(TValue[] storage, int i, TValue value)
        {
            storage[i] = value;
        }

        protected override TValue GetValue(TValue[] storage, int i)
        {
            return storage[i];
        }
    }
}