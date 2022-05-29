using System.Collections.Generic;

namespace PGSauce.Core.FSM.Base
{
    /// <summary>
    /// Abstract ANY transition : Target State, Decision to transition, Excluded from states
    /// </summary>
    public interface IAnyTransition
    {
        public List<AbstractState> ExcludedStates();
        AbstractState GetTargetState();
        IDecision GetDecision();
        bool GetReverseValue();
    }
}