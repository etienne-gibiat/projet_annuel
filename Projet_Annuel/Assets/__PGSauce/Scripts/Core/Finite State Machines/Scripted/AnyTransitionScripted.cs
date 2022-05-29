using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.FSM.Base;

namespace PGSauce.Core.FSM.Scripted
{
    /// <summary>
    /// Any transition for Scripted FSMs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AnyTransitionScripted<T> : TransitionScripted<T>, AnyTransitionBase<T> where T : MonoStateMachineBase<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to">The goal state that any other states can reach</param>
        /// <param name="decision">Should the current state go to this goal state ?</param>
        /// <param name="allowLoop">If the current state is the goal state, should it transition to itself ?</param>
        /// <param name="excludedFromStates">If the current state is excluded, it should not be considered in this any transition</param>
        public AnyTransitionScripted(StateScripted<T> to, Decision decision, bool allowLoop, List<StateBase<T>> excludedFromStates) : base(to,decision,allowLoop)
        {
            ExcludedFromStates = excludedFromStates;
        }

        /// <summary>
        /// Excluded States
        /// If the current state is excluded, it should not be considered in this any transition
        /// </summary>
        public List<StateBase<T>> ExcludedFromStates { get; }
        /// <summary>
        /// Cast the excluded states to AbstractState
        /// </summary>
        /// <returns></returns>
        public List<AbstractState> ExcludedStates()
        {
            return ExcludedFromStates.Cast<AbstractState>().ToList();
        }

        /// <summary>
        /// Should the decision be reversed ?
        /// </summary>
        /// <returns></returns>
        public bool GetReverseValue()
        {
            return false;
        }
    }
}