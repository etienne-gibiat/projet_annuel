namespace PGSauce.Core.FSM.Base
{
    /// <summary>
    /// A Transition between 2 states
    /// </summary>
    /// <typeparam name="T">The type of object that has a FSM (Player, Bomb etc.)</typeparam>
    public interface ITransitionBase<T> : ITransition where T : MonoStateMachineBase<T>
    {
        /// <summary>
        /// Should we go from the current state to target state ?
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public abstract bool Decide(T controller);
        /// <summary>
        /// Target State
        /// </summary>
        public abstract StateBase<T> To { get; set; }
        /// <summary>
        /// If current state is target state, should we allow loop ?
        /// </summary>
        public abstract bool AllowLoop { get; set; }
    }
}