using System.Collections.Generic;
using PGSauce.Core.FSM.WithSo;

namespace PGSauce.Core.FSM.Base
{
    /// <summary>
    /// Interface representing an FSM
    /// </summary>
    public interface IStateMachine
    {
        /// <summary>
        /// The current state in which the FSM is
        /// </summary>
        /// <returns></returns>
        AbstractState GetCurrentState();
        /// <summary>
        /// All the any transitions
        /// A any transition is a state that any state in the fsm can come to
        /// </summary>
        /// <returns></returns>
        List<IAnyTransition> GetAny();
        /// <summary>
        /// The initial state of the FSM
        /// </summary>
        /// <returns></returns>
        AbstractState GetInitialState();
    }
}