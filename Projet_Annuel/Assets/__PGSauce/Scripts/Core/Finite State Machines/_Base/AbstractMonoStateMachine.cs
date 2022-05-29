using System.Collections.Generic;
using PGSauce.Unity;
using UnityEngine;

namespace PGSauce.Core.FSM.Base
{
    /// <summary>
    /// The MonoBehaviour Base (non generic) for all FSMs
    /// </summary>
    public abstract class AbstractMonoStateMachine : PGMonoBehaviour, IStateMachine
    {
        /// <summary>
        /// The current state in which the FSM is
        /// </summary>
        /// <returns></returns>
        public abstract AbstractState GetCurrentState();
        /// <summary>
        /// All the any transitions
        /// A any transition is a state that any state in the fsm can come to
        /// </summary>
        /// <returns></returns>
        public abstract List<IAnyTransition> GetAny();
        /// <summary>
        /// The initial state of the FSM
        /// </summary>
        /// <returns></returns>
        public abstract AbstractState GetInitialState();
    }
}