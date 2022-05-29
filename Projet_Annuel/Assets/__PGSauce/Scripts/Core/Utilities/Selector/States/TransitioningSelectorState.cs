namespace PGSauce.Core.Utilities
{
    public class TransitioningSelectorState<T> : SelectorState<T>
    {
        public override void Enter(Selector<T> controller)
        {
            base.Enter(controller);
            controller.OnEnterTransitioningState();
        }

        public override void LogicUpdate(Selector<T> controller)
        {
            base.LogicUpdate(controller);
            controller.OnUpdateTransitioningState();
        }

        public override void Exit(Selector<T> controller)
        {
            base.Exit(controller);
            controller.BeginTransition = false;
            controller.OnExitTransitioningState();
        }
    }
}