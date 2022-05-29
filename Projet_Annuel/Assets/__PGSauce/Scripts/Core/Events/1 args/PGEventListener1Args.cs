using System;
using UnityEngine;
using UnityEngine.Events;

namespace PGSauce.Core.PGEvents
{
    public class PGEventListener1Args<T0, PGEventT, UnityEventT> : IPGEventListener where PGEventT : PGEvent1Args<T0> where UnityEventT : UnityEvent<T0>, new()
    {
        [SerializeField] private PGEventT gameEvent;
        [SerializeField] private UnityEventT action;

        public PGEventT GameEvent
        {
            get => gameEvent;
            set => gameEvent = value;
        }

        public UnityEventT Action
        {
            get => action;
            set => action = value;
        }

        private void OnEnable()
        {
            Register();
        }

        private void OnDisable()
        {
            Unregister();
        }

        private void ActionCalled(T0 value0)
        {
            Action.Invoke(value0);
        }

        public override void Register()
        {
            if(!GameEvent){return;}
            GameEvent.Register(ActionCalled, gameObject);
        }

        public override void Unregister()
        {
            if(!GameEvent){return;}
            GameEvent.Unregister(ActionCalled, gameObject);
        }
    }
}