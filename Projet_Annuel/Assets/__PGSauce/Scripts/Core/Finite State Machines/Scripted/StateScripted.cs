using System.Collections.Generic;
using PGSauce.Core.Extensions;
using PGSauce.Core.FSM.Base;

namespace PGSauce.Core.FSM.Scripted
{
    /// <summary>
    /// Implementation of the state for Scripted FSMs
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    public abstract class StateScripted<T> : StateBase<T> where T : MonoStateMachineBase<T>
    {
        protected StateScripted()
        {
            Transitions = new List<ITransitionBase<T>>();
        }
        
        /// <summary>
        /// The name of the state is the class name
        /// </summary>
        public override string Name()
        {
            var name = GetType().ToString();
            return name.RemoveNamespace();
        }

        /// <summary>
        /// A scripted state can't be null, returns false
        /// </summary>
        public override bool IsNullState => false;
    }
}
