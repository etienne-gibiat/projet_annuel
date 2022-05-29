using System.Collections.Generic;
using PGSauce.Core.FSM.Base;
using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Core.FSM.Scripted
{
    /// <summary>
    /// The MonoBehaviour base class to all scripted FSMs
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    [HelpURL(DocsPaths.MonoStateMachineScripted)]
    public abstract class MonoStateMachineScripted<T> : MonoStateMachineBase<T>, IMonoStateMachineScripted where T : MonoStateMachineScripted<T>
    {
        private List<AnyTransitionBase<T>> _any;
        
        protected sealed override List<AnyTransitionBase<T>> AnyTransitions => _any;
        
        protected sealed override void InitFsm()
        {
            base.InitFsm();
            InitializeStates();
            _any = new List<AnyTransitionBase<T>>();
            CreateTransitions();
        }

        /// <summary>
        /// Adds a new transition to the FSM
        /// </summary>
        /// <param name="from">From state</param>
        /// <param name="to">Goal state</param>
        /// <param name="decision">If current state is from state, should it go to goal state ?</param>
        /// <param name="allowLoop">If from and goal state are the same, should the transition be allowed ?</param>
        protected void AddNewTransition(StateScripted<T> from, StateScripted<T> to, TransitionScripted<T>.Decision decision, bool allowLoop = false)
        {
            var stateTransition = new TransitionScripted<T>(to, decision, allowLoop);
            from.Transitions.Add(stateTransition);
        }

        /// <summary>
        /// Adds a new any transition
        /// </summary>
        /// <param name="to">Goal State</param>
        /// <param name="decision">If current state is from state, should it go to goal state ?</param>
        /// <param name="allowLoop">If from and goal state are the same, should the transition be allowed ?</param>
        /// <param name="excluded">If the current state is in this list, it should not be considered in this any transition</param>
        protected void AddAnyTransition(StateScripted<T> to, TransitionScripted<T>.Decision decision, bool allowLoop, List<StateBase<T>> excluded)
        {
            _any.Add(new AnyTransitionScripted<T>(to, decision, allowLoop, excluded));
        }

        /// <summary>
        /// Override this and create your states in this method
        /// </summary>
        protected abstract void InitializeStates();
        /// <summary>
        /// Override this and call AddNewTransition && AddAnyTransition in this method to create the FSM transitions
        /// </summary>
        protected abstract void CreateTransitions();
    }
}
