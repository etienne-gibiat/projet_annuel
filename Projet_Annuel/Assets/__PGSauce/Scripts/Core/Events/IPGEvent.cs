using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Core.PGEvents
{
    public abstract class IPGEvent: ScriptableObject
    {
        [SerializeField] [TextArea] private string description;

        [ShowInInspector]
        public List<GameObject> Listeners { get; } = new List<GameObject>();

        protected void RegisterListener(GameObject listener)
        {
            Listeners.Add(listener);
        }

        protected void UnregisterListener(GameObject listener)
        {
            Listeners.Remove(listener);
        }
    }
}
