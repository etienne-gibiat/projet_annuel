using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.FSM.Base;

namespace PGSauce.Core.FSM.WithSo
{
    /// <summary>
    /// Implements ANY Transition for SO FSMs
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    public sealed class AnyTransitionWithSo<T> : TransitionWithSo<T>, AnyTransitionBase<T> where T : MonoStateMachineBase<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to">The goal state that any other states can reach</param>
        /// <param name="decision">Should the current state go to this goal state ?</param>
        /// <param name="reverseValue">Should we reverse the decision value ?</param>
        /// <param name="allowLoop">If the current state is the goal state, should it transition to itself ?</param>
        /// <param name="excludedFromStates">If the current state is excluded, it should not be considered in this any transition</param>
        public AnyTransitionWithSo(StateBase<T> to, SoDecision<T> decision, bool reverseValue, bool allowLoop, List<StateBase<T>> excludedFromStates) : base(to, decision, reverseValue, allowLoop)
        {
            ExcludedFromStates = excludedFromStates;
        }
        /// <summary>
        /// If the current state is excluded, it should not be considered in this any transition
        /// </summary>
        public List<StateBase<T>> ExcludedFromStates { get; }
        /// <summary>
        /// Cast excluded states to AbstractState
        /// </summary>
        /// <returns></returns>
        public List<AbstractState> ExcludedStates()
        {
            return ExcludedFromStates.Cast<AbstractState>().ToList();
        }
        /// <summary>
        /// Should we reverse the decision value ?
        /// </summary>
        /// <returns></returns>
        public bool GetReverseValue()
        {
            return ReverseValue;
        }
    }
}