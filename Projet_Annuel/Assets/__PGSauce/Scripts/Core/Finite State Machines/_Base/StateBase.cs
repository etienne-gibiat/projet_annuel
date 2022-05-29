using System.Collections.Generic;
using System.Linq;

namespace PGSauce.Core.FSM.Base
{
    /// <summary>
    /// The Generic State the FSM can be in
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    public abstract class StateBase<T> : AbstractState where T : MonoStateMachineBase<T>
    {
        /// <summary>
        /// All the transitions (From state, To State, Decision)
        /// </summary>
        /// <returns></returns>
        public List<ITransitionBase<T>> Transitions { get; protected set; }
        private List<ITransition> TemporaryTransitions { get; set; } = new List<ITransition>();

        /// <summary>
        /// The name of the state
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name();
        }

        /// <summary>
        /// Used by editor, to debug the FSM
        /// </summary>
        /// <returns></returns>
        public int debugIndex;
        
        /// <summary>
        /// If a transition is valid, change the state
        /// </summary>
        /// <param name="controller"></param>
        /// <returns>true if the state has been changed</returns>
        public bool CheckTransitions(T controller)
        {
            return CheckTransitions(controller, Transitions);
        }

        /// <summary>
        /// If a transition is valid, change the state
        /// </summary>
        /// <param name="controller"></param>
        /// <returns>true if the state has been changed</returns>
        public bool CheckAnyTransitions(T controller, List<AnyTransitionBase<T>> any)
        {
            var from = controller.Fsm.CurrentState;
            var transitions = any.Where(t => !t.ExcludedFromStates.Contains(from)).Select(t => t as ITransitionBase<T>).ToList();
            return CheckTransitions(controller, transitions);
        }

        public override List<ITransition> GetTransitions()
        {
            return Transitions.Cast<ITransition>().ToList();
        }

        public override void CleanTemporaryTransitions()
        {
            TemporaryTransitions?.Clear();
        }

        public override List<ITransition> GetTransitionsIncludingTemporary()
        {
            var list = new List<ITransition>(GetTransitions());
            list.AddRange(TemporaryTransitions);
            return list;
        }

        public override void AddTemporaryTransition(IAnyTransition any)
        {
            TemporaryTransitions ??= new List<ITransition>();
            TemporaryTransitions.Add(new TransitionInterface(any.GetTargetState(), any.GetDecision(), any.GetReverseValue()));
        }

        /// <summary>
        /// Called when we enter the state
        /// </summary>
        /// <param name="controller">FSM controller</param>
        public virtual void Enter(T controller)
        {
        }

        /// <summary>
        /// Called when we exit the state
        /// </summary>
        /// <param name="controller">FSM controller</param>
        public virtual void Exit(T controller)
        {
        }

        /// <summary>
        /// Called each frame in the Unity physics loop
        /// </summary>
        /// <param name="controller">FSM controller</param>
        public virtual void PhysicsUpdate(T controller)
        {
        }

        /// <summary>
        /// Called each frame
        /// </summary>
        /// <param name="controller">FSM controller</param>
        public virtual void LogicUpdate(T controller)
        {
        }
        
        private bool CheckTransitions(T controller, List<ITransitionBase<T>> transitions)
        {
            var from = controller.Fsm.CurrentState;
            foreach (var t in transitions)
            {
                var to = t.To;
                if (from.Equals(to))
                {
                    var allowLoop = t.AllowLoop;
                    if(!allowLoop){continue;}
                }
                
                var decisionSucceeded = t.Decide(controller);

                if (!decisionSucceeded) continue;
                controller.Fsm.ChangeState(to);
                return true;
            }

            return false;
        }
    }
}