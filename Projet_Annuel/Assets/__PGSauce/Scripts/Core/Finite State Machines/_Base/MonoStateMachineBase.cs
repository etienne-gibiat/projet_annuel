using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace PGSauce.Core.FSM.Base
{
    /// <summary>
    /// The MonoBehaviour Implementing the FSM
    /// </summary>
    /// <typeparam name="T">The type of the FSM</typeparam>
    public abstract class MonoStateMachineBase<T> : AbstractMonoStateMachine where T : MonoStateMachineBase<T>
    {
        [SerializeField] private bool showDebug;
        
        /// <summary>
        /// The initial state of the FSM
        /// </summary>
        protected abstract StateBase<T> InitialState { get; }
        /// <summary>
        /// The any transitions are stored in this list. An any transition is a target state that any other state can reach
        /// </summary>
        protected abstract List<AnyTransitionBase<T>> AnyTransitions { get; }
        /// <summary>
        /// Used in editor to print some data about the fsm
        /// </summary>
        public bool ShowDebug => showDebug;
        /// <summary>
        /// Current State Name
        /// </summary>
        [ShowInInspector, LabelText("Current State")]
        public string CurrentStateName => Fsm != null ? Fsm.CurrentState.Name() : "";
        /// <summary>
        /// The FSM object
        /// </summary>
        public StateMachineBase<T> Fsm { get; private set; }

        /// <summary>
        /// Initializes the FSM
        /// </summary>
        protected void Awake()
        {
            InitFsm();
            Fsm = new StateMachineBase<T>(this as T, InitialState, AnyTransitions);
            CustomInit();
        }
        
        /// <summary>
        /// Apply the LogicUpdate Method of the FSM and sees if we should change the current state
        /// This is where BeforeFsmUpdate and AfterFsmUpdate are called
        /// </summary>
        protected void Update()
        {
            BeforeFsmUpdate();
            Fsm.LogicUpdate();
            Fsm.CheckTransitions();
            AfterFsmUpdate();
        }
        
        /// <summary>
        /// Apply the PhysicsUpdate Method of the FSM
        /// </summary>
        protected void FixedUpdate()
        {
            Fsm.PhysicsUpdate();
        }
        
        public override AbstractState GetCurrentState()
        {
            return Fsm?.CurrentState;
        }

        public override List<IAnyTransition> GetAny()
        {
            return AnyTransitions == null ? new List<IAnyTransition>() : AnyTransitions.Cast<IAnyTransition>().ToList();
        }

        public override AbstractState  GetInitialState()
        {
            return InitialState;
        }

        /// <summary>
        /// Custom awake for the fsm
        /// </summary>
        protected virtual void CustomInit()
        {
        }

        /// <summary>
        /// How to init the FSM, should be used overridden by direct children only
        /// </summary>
        protected virtual void InitFsm()
        {
        }
        
        /// <summary>
        /// Called right before the fsm is updated
        /// </summary>
        protected virtual void BeforeFsmUpdate()
        {
        }

        /// <summary>
        /// Called right after the fsm is updated
        /// </summary>
        protected virtual void AfterFsmUpdate()
        {
        }

        /// <summary>
        /// Draws the state name next to the object in the editor
        /// </summary>
        protected void OnDrawGizmos()
        {
            if(!showDebug) {return;}
            
#if UNITY_EDITOR
            Handles.Label(transform.position + Vector3.up * 3, CurrentStateName);  
#endif
            
        }
    }
}