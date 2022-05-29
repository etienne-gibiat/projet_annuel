using PGSauce.Core.FSM.Base;

namespace PGSauce.Core.FSM.WithSo
{
    /// <summary>
    /// A scriptable object encapsulating a FSM action 
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    public abstract class SoAction<T> : SoActionBase where T : MonoStateMachineBase<T>
    {
        /// <summary>
        /// The action to do
        /// </summary>
        /// <param name="controller">The controller of the FSM</param>
        public abstract void Act(T controller);
    }
}
