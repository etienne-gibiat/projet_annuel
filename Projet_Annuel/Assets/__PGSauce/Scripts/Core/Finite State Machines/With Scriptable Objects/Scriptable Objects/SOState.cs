using UnityEngine;
using PGSauce.Core.FSM.Base;
using Sirenix.OdinInspector;

namespace PGSauce.Core.FSM.WithSo
{
    /// <summary>
    /// A scriptable object encapsulating a FSM state
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    public abstract class SoState<T> : SoStateBase where T : MonoStateMachineBase<T>
    {
        [SerializeField] [HideIf("IsNullState")] private SoAction<T> enterAction;
        [SerializeField] [HideIf("IsNullState")] private SoAction<T> updateAction;
        [SerializeField] [HideIf("IsNullState")] private SoAction<T> fixedUpdateAction;
        [SerializeField] [HideIf("IsNullState")] private SoAction<T> exitAction;

        [SerializeField] [HideIf("IsNullState")] private Transition<T>[] transitions;

        /// <summary>
        /// Transitions coming from this state
        /// </summary>
        public Transition<T>[] Transitions => transitions;

        /// <summary>
        /// Called when we enter the state
        /// </summary>
        /// <param name="controller">Controller of the FSM</param>
        public void OnEnter(T controller) {
            if(enterAction == null) { return; }
            enterAction.Act(controller);
        }
        /// <summary>
        /// Called each frame
        /// </summary>
        /// <param name="controller">Controller of the FSM</param>
        public void LogicUpdate(T controller) {
            if (updateAction == null) { return; }
            updateAction.Act(controller);
        }
        /// <summary>
        /// Called each frame in the unity physics loop
        /// </summary>
        /// <param name="controller">Controller of the FSM</param>
        public void PhysicsUpdate(T controller) {
            if (fixedUpdateAction == null) { return; }
            fixedUpdateAction.Act(controller);
        }
        /// <summary>
        /// Called when we exit the state
        /// </summary>
        /// <param name="controller">Controller of the FSM</param>
        public void OnExit(T controller) {
            if (exitAction == null) { return; }
            exitAction.Act(controller);
        }
    }
}
