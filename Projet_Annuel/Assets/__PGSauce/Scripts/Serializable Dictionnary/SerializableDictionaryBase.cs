using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PGSauce.Core.SerializableDictionaries
{
    public abstract class SerializableDictionaryBase
    {
        public abstract class Storage { }

        protected class Dictionary<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>
        {
            public Dictionary() {}
            public Dictionary(IDictionary<TKey, TValue> dict) : base(dict) {}
            public Dictionary(SerializationInfo info, StreamingContext context) : base(info, context) {}
        }
    }
}