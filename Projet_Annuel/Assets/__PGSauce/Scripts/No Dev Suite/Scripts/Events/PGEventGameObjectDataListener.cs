using System;
using PGSauce.Core.PGEvents;
using UnityEngine;
using UnityEngine.Events;

namespace PGSauce.NoDevSuite
{
    public class PGEventGameObjectDataListener : PGEventListener1Args<GameObjectData, PGEventGameObjectData, UnityEvent<GameObjectData>>
    {
    }
}