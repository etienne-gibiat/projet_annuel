using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PGSauce.Core.PGEditor
{
    public class FSMDecisionGenerationAction : OdinWindowBaseCodeGenerationAction<FSMCodeGeneratorOdinWindow>
    {
        public FSMDecisionGenerationAction(FSMCodeGeneratorOdinWindow window) : base(window)
        {
        }

        protected override void GenerateAction()
        {
            Window.GenerateNewFsmDecision();
        }

        protected override (bool isOK, string errorMessage) VerifyCriticalData()
        {
            var ok = Window.IsNotNullOrEmpty(Window.ToThisState) && Window.IsNotNullOrEmpty(Window.SelectedState);
            return (ok, "From and To States must be filled");
        }
    }
}
