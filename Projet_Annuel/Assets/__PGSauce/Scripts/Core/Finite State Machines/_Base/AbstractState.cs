using System.Collections;
using System.Collections.Generic;

namespace PGSauce.Core.FSM.Base
{
    /// <summary>
    /// The base non generic state for any FMSs
    /// </summary>
    public abstract class AbstractState
    {
        /// <summary>
        /// Is the state a null node ?
        /// Useful for stopping some recursive methods for example
        /// </summary>
        public abstract bool IsNullState { get; }
        /// <summary>
        /// The name of the state
        /// </summary>
        /// <returns></returns>
        public abstract string Name();
        /// <summary>
        /// All the permanent transitions (From state, To State, Decision)
        /// </summary>
        /// <returns></returns>
        public abstract List<ITransition> GetTransitions();
        /// <summary>
        /// An editor method used to clean transitions cache (useful in editor)
        /// </summary>
        public abstract void CleanTemporaryTransitions();
        /// <summary>
        /// Get all the transitions, including the cached ones (useful in editor)
        /// </summary>
        /// <returns></returns>
        public abstract List<ITransition> GetTransitionsIncludingTemporary();
        /// <summary> 
        /// Add a cached transition (a fake one) (useful in editor)
        /// </summary>
        /// <param name="any"></param>
        public abstract void AddTemporaryTransition(IAnyTransition any);
    }
}