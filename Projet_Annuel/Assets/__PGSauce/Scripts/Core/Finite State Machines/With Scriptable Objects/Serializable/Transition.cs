using PGSauce.Core.FSM.Base;

namespace PGSauce.Core.FSM.WithSo
{
    /// <summary>
    /// Serializable Transition
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    [System.Serializable]
    public class Transition<T> : TransitionBase where T : MonoStateMachineBase<T>
    {
        /// <summary>
        /// The SO Decision to decide if we transition
        /// </summary>
        public SoDecision<T> decision;
        /// <summary>
        /// The SO State to transition to
        /// </summary>
        public SoState<T> to;
        /// <summary>
        /// Should we reverse the decision value ?
        /// </summary>
        public bool reverseValue;
        /// <summary>
        /// If from and goal state are the same, should the transition be allowed ?
        /// </summary>
        public bool allowLoop;

        public SoDecisionBase GetDecision()
        {
            return decision;
        }

        public SoStateBase GetTargetState()
        {
            return to;
        }

        public bool ReverseValue()
        {
            return reverseValue;
        }
    }
}
