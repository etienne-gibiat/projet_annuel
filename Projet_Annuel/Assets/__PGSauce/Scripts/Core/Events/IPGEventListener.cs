using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PGSauce.Core.PGEvents
{
    public abstract class IPGEventListener : MonoBehaviour
    {
        public abstract void Register();
        public abstract void Unregister();
    }
}