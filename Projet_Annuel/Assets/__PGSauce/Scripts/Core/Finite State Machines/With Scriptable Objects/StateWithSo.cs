using System.Collections.Generic;
using PGSauce.Core.FSM.Base;

namespace PGSauce.Core.FSM.WithSo
{
    /// <summary>
    /// State for SO FSMs
    /// Owns a scriptable object representing it
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    public class StateWithSo<T> : StateBase<T> where T : MonoStateMachineBase<T>
    {
        private readonly SoState<T> _soState;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="soState">The SO representing this state</param>
        public StateWithSo(SoState<T> soState)
        {
            _soState = soState;
            Transitions = new List<ITransitionBase<T>>();
        }

        public sealed override void Enter(T controller)
        {
            base.Enter(controller);
            _soState.OnEnter(controller);
        }

        public sealed override void Exit(T controller)
        {
            base.Exit(controller);
            _soState.OnExit(controller);
        }

        public sealed override void LogicUpdate(T controller)
        {
            base.LogicUpdate(controller);
            _soState.LogicUpdate(controller);
        }

        public sealed override void PhysicsUpdate(T controller)
        {
            base.PhysicsUpdate(controller);
            _soState.PhysicsUpdate(controller);
        }

        /// <summary>
        /// The name of the state is given by the SO
        /// </summary>
        public override string Name()
        {
            return _soState.StateName;
        }

        /// <summary>
        /// IsNull is given by the SO
        /// </summary>
        public override bool IsNullState => _soState.IsNullState;
    }
}