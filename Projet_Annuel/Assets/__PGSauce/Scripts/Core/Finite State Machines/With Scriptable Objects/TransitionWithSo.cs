using PGSauce.Core.FSM.Base;

namespace PGSauce.Core.FSM.WithSo
{
    /// <summary>
    /// Implements Transition for SO FSMs
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    public class TransitionWithSo<T> : ITransitionBase<T> where T : MonoStateMachineBase<T>
    {
        private readonly SoDecision<T> _decision;
        private readonly bool _reverseValue;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to">Goal State</param>
        /// <param name="decision">Should the current state go to this goal state ?</param>
        /// <param name="reverseValue">Should we reverse the decision value ?</param>
        /// <param name="allowLoop">If from and goal state are the same, should the transition be allowed ?</param>
        public TransitionWithSo(StateBase<T> to, SoDecision<T> decision, bool reverseValue, bool allowLoop)
        {
            To = to;
            _decision = decision;
            _reverseValue = reverseValue;
            AllowLoop = allowLoop;
        }

        /// <summary>
        /// Should the current state go to this goal state ?
        /// </summary>
        /// <param name="controller">FSM controller</param>
        /// <returns></returns>
        public bool Decide(T controller)
        {
            var ok = _decision.Decide(controller);
            if (ReverseValue)
            {
                ok = !ok;
            }
            return ok;
        }
        /// <summary>
        /// Goal State
        /// </summary>
        public StateBase<T> To { get; set; }
        /// <summary>
        /// If from and goal state are the same, should the transition be allowed ?
        /// </summary>
        public bool AllowLoop { get; set; }
        /// <summary>
        /// Should we reverse the decision value ?
        /// </summary>
        protected bool ReverseValue => _reverseValue;
        /// <summary>
        /// Casts goal state to AbstractState
        /// </summary>
        public AbstractState GetTargetState()
        {
            return To;
        }
        /// <summary>
        /// Casts decision to IDecision
        /// </summary>
        public IDecision GetDecision()
        {
            return new DecisionInterface();
        }
    }
}