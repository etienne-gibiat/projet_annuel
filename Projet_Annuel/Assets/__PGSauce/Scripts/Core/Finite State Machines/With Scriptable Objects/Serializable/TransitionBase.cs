namespace PGSauce.Core.FSM.WithSo
{
    public interface TransitionBase
    {
        /// <summary>
        /// Get the non generic so decision
        /// </summary>
        /// <returns></returns>
        public SoDecisionBase GetDecision();
        /// <summary>
        /// Get the non generic so goal state
        /// </summary>
        /// <returns></returns>
        public SoStateBase GetTargetState();
        /// <summary>
        /// Should we reverse the decision value ?
        /// </summary>
        /// <returns></returns>
        public bool ReverseValue();
    }
}
