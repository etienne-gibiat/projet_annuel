using System.Collections.Generic;
using PGSauce.Core.PGDebugging;

namespace PGSauce.Core.FSM.Base
{
    /// <summary>
    /// The FSM per se
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StateMachineBase<T> where T : MonoStateMachineBase<T>
    {
        private readonly T _controller;
        private int _currentStateIndex;
        
        private bool ShowDebug => _controller.ShowDebug;
        /// <summary>
        /// The current state of the FSM
        /// </summary>
        public StateBase<T> CurrentState
        {
            get;
            private set;
        }
        private List<AnyTransitionBase<T>> Any
        {
            get;
            set;
        }

        private T Controller => _controller;

        /// <summary>
        /// Inits the FSM and enters the initial state
        /// </summary>
        /// <param name="stateControllerBase">The FSM's controller (MonoBehaviour)</param>
        /// <param name="initialState"></param>
        /// <param name="any">Any transitions</param>
        public StateMachineBase(T stateControllerBase, StateBase<T> initialState, List<AnyTransitionBase<T>> any)
        {
            _controller = stateControllerBase;
            CurrentState = initialState;
            Any = any;
            Enter();
        }
        
        /// <summary>
        /// Exits current state and enters newState
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(StateBase<T> newState)
        {
            if (newState.IsNullState) {return;}
            Exit();
            CurrentState = newState;
            _currentStateIndex = CurrentState.debugIndex;
            Enter();
        }

        /// <summary>
        /// Check if the FSM should change state
        /// </summary>
        public void CheckTransitions()
        {
            var changed = CurrentState.CheckTransitions(Controller);

            if (!changed && Any != null)
            {
                CurrentState.CheckAnyTransitions(Controller, Any);
            }
        }

        /// <summary>
        /// Called each frame
        /// </summary>
        public void LogicUpdate()
        {
            CurrentState.LogicUpdate(Controller);
        }

        /// <summary>
        /// Called each frame in the Unity physics loop
        /// </summary>
        public void PhysicsUpdate()
        {
            CurrentState.PhysicsUpdate(Controller);
        }

        private void Exit()
        {
            PGDebug.SetCondition(ShowDebug).Message($"{Controller.name} Exited state {CurrentState}").Log();
            CurrentState.Exit(Controller);
        }
        
        private void Enter()
        {
            PGDebug.SetCondition(ShowDebug).Message($"{Controller.name} Entered state {CurrentState}").Log();
            CurrentState.Enter(Controller);
        }
    }
}