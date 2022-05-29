namespace PGSauce.Core.Utilities
{
    public class FocusedSelectorState<T> : SelectorState<T>
    {
        public override void Enter(Selector<T> controller)
        {
            base.Enter(controller);
            controller.OnEnterFocusedState();
        }

        public override void LogicUpdate(Selector<T> controller)
        {
            base.LogicUpdate(controller);
            controller.OnUpdateFocusedState();
        }

        public override void Exit(Selector<T> controller)
        {
            base.Exit(controller);
            controller.OnExitFocusedState();
        }
    }
}