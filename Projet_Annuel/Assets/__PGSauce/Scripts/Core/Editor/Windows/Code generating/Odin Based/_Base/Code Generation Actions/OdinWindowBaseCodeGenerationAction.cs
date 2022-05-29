using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.PGEditor;

namespace PGSauce.Core.PGEditor
{
    public abstract class OdinWindowBaseCodeGenerationAction<T> : CodeGenerationAction where T : CodeGeneratingOdinWindowBase
    {
        protected OdinWindowBaseCodeGenerationAction(T window)
        {
            Window = window;
        }

        protected T Window { get; private set; }
        protected override bool ConfirmCodeGeneration()
        {
            return Window.ConfirmCodeGeneration();
        }
    }
}
