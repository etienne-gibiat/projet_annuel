using PGSauce.Core.FSM.Base;

namespace PGSauce.Core.FSM.Scripted
{
    /// <summary>
    /// Implementation of the transitions for Scripted FSMs
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    public class TransitionScripted<T> : ITransitionBase<T> where T : MonoStateMachineBase<T>
    {
        public delegate bool Decision();

        private readonly Decision _decision;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to">Goal State</param>
        /// <param name="decision">Should the current state go to this goal state ?</param>
        /// <param name="allowLoop">If from and goal state are the same, should the transition be allowed ?</param>
        public TransitionScripted(StateScripted<T> to, Decision decision, bool allowLoop)
        {
            To = to;
            _decision = decision;
            AllowLoop = allowLoop;
        }

        public bool Decide(T controller)
        {
            return _decision();
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
        /// Goal State
        /// </summary>
        /// <returns>Goal State</returns>
        public AbstractState GetTargetState()
        {
            return To;
        }

        /// <summary>
        /// Used by Editor
        /// </summary>
        /// <returns></returns>
        public IDecision GetDecision()
        {
            return new DecisionInterface();
        }
    }
}
