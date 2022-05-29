using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PGSauce.Core.PGEditor
{
    public class FSMActionGenerationAction : OdinWindowBaseCodeGenerationAction<FSMCodeGeneratorOdinWindow>
    {
        public FSMActionGenerationAction(FSMCodeGeneratorOdinWindow window) : base(window)
        {
        }

        protected override void GenerateAction()
        {
            Window.GenerateNewFsmAction();
        }

        protected override (bool isOK, string errorMessage) VerifyCriticalData()
        {
            var ok = Window.IsNotNullOrEmpty(Window.ActionTypeName) && Window.IsNotNullOrEmpty(Window.ActionName) && Window.IsNotNullOrEmpty(Window.SelectedState);
            return (ok, "You must select the state and the action type");
        }
    }
}
