using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace PGSauce.Core.PGEvents
{
    public class PGEventListenerNoArg<PGEventT, UnityEventT> : IPGEventListener where PGEventT : PGEventNoArg where UnityEventT : UnityEvent
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

        private void ActionCalled()
        {
            Action.Invoke();
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
