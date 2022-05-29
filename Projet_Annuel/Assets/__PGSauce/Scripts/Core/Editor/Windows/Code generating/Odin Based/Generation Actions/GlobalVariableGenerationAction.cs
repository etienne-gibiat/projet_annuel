using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.PGEditor;

namespace PGSauce.Core.PGEditor
{
    public class GlobalVariableGenerationAction : OdinWindowBaseCodeGenerationAction<GlobalVariableGeneratorOdinWindow>
    {
        //? IS it possible not to have to that ?
        public GlobalVariableGenerationAction(GlobalVariableGeneratorOdinWindow window) : base(window)
        {
        }

        protected override void GenerateAction()
        {
            Window.GenerateGVCode();
        }

        protected override (bool isOK, string errorMessage) VerifyCriticalData()
        {
            return Window.VerifyCriticalData();
        }
    }
}
