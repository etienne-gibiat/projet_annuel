using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Unity;
using UnityEngine.Events;

namespace PGSauce.NoDevSuite
{
    public abstract class NoDevEventReacter : PGMonoBehaviour
    {
        [SerializeField] private PGEventGameObjectData noDevSuiteListenedEvent;

        protected void Awake()
        {
            var listener = gameObject.AddComponent<PGEventGameObjectDataListener>();
            listener.GameEvent = noDevSuiteListenedEvent;
            listener.Register();
            listener.Action = new UnityEvent<GameObjectData>();
            listener.Action.AddListener(React);
            CustomAwake();
        }

        protected virtual void CustomAwake()
        {
        }

        protected abstract void React(GameObjectData data);
    }
}
