using PGSauce.Core.FSM.Base;

namespace PGSauce.Core.FSM.WithSo
{
    /// <summary>
    /// A scriptable object encapsulating a FSM decision
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    public abstract class SoDecision<T> : SoDecisionBase where T : MonoStateMachineBase<T>
    {
        /// <summary>
        /// This method; given the controller, decides if it should transition to the next state
        /// </summary>
        /// <param name="controller">The controller of the FSM</param>
        /// <returns></returns>
        public abstract bool Decide(T controller);
    }
}
