using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.FSM.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Core.FSM.WithSo
{
    /// <summary>
    /// A scriptable object encapsulating a FSM any transition 
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    [InlineEditor]
    public class SoAny<T> : SoAnyBase where T : MonoStateMachineWithSo<T>
    {
        [SerializeField] private AnyTransition<T>[] transitions;
        /// <summary>
        /// Casts the any transitions to AnyTransitionBase
        /// </summary>
        /// <param name="monoStateMachineWithSo">The FSM using this any so</param>
        /// <returns></returns>
        public List<AnyTransitionBase<T>> GetTransitions(MonoStateMachineWithSo<T> monoStateMachineWithSo)
        {
            return transitions.Select(t =>
                    new AnyTransitionWithSo<T>(monoStateMachineWithSo.GetState(t.to), t.decision,
                        t.reverseValue, t.allowLoop,
                        t.excludedStates.Select(monoStateMachineWithSo.GetState).ToList()))
                .Cast<AnyTransitionBase<T>>().ToList();
        }
    }
}