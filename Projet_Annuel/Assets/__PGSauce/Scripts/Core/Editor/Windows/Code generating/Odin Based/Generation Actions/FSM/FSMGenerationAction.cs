namespace PGSauce.Core.PGEditor
{
    public class FSMGenerationAction : OdinWindowBaseCodeGenerationAction<FSMCodeGeneratorOdinWindow>
    {
        public FSMGenerationAction(FSMCodeGeneratorOdinWindow window) : base(window)
        {
        }

        protected override void GenerateAction()
        {
            Window.GenerateNewFsm();
        }

        protected override (bool isOK, string errorMessage) VerifyCriticalData()
        {
            var ok = Window.IsNotNullOrEmpty(Window.NAME());
            return (ok, "FSM Name must be not empty");
        }
    }
}
