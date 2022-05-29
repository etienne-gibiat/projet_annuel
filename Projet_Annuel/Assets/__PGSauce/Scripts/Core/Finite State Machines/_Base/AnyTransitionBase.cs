using System.Collections.Generic;

namespace PGSauce.Core.FSM.Base
{
    /// <summary>
    /// The any transition : when multiple states should have the same transition
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface AnyTransitionBase<T>: ITransitionBase<T>, IAnyTransition where T : MonoStateMachineBase<T>
    {
        public abstract List<StateBase<T>> ExcludedFromStates { get; }
    }
}