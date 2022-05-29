using System;
using UnityEngine;
using UnityEngine.Events;
using PGSauce.Core.PGEvents;

namespace PGSauce.PGEvents.Example
{
    [Serializable]
    public class UnityEventIntPlayer : UnityEvent<int,Player> { }
}
