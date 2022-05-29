using System;
using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.FSM.Base;

namespace PGSauce.Core.FSM.WithSo
{
    /// <summary>
    /// Serializable Any Transition
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    [Serializable]
    public class AnyTransition<T> : Transition<T>, AnyTransitionBase where T : MonoStateMachineBase<T>
    {
        /// <summary>
        /// Excluded states of the any transition
        /// </summary>
        public List<SoState<T>> excludedStates;
        /// <summary>
        /// Cast the excluded states to SoStateBase (removes the generic)
        /// </summary>
        /// <returns></returns>
        public List<SoStateBase> ExcludedStates()
        {
            var list = excludedStates.Select(state => state as SoStateBase).ToList();
            return list;
        }
    }
}