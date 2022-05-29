using UnityEngine;
using UnityEngine.Events;
using PGSauce.Core.PGEvents;

namespace PGSauce.Core.PGEvents
{
    public class PGEventListener3Args<T0, T1, T2, PGEventT, UnityEventT> : IPGEventListener where PGEventT : PGEvent3Args<T0, T1, T2> where UnityEventT : UnityEvent<T0, T1, T2>
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

        private void ActionCalled(T0 value0, T1 value1, T2 value2)
        {
            Action.Invoke(value0, value1, value2);
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