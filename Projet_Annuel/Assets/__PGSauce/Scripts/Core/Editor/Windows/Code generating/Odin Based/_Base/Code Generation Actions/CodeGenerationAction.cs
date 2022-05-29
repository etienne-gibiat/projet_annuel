using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace PGSauce.Core.PGEditor
{
    public abstract class CodeGenerationAction
    {
        public static readonly (bool, string) OkData = (true, "");
        
        public void Generate()
        {
            var (isOK, errorMessage) = VerifyCriticalData();
            if (!isOK)
            {
                throw new UnityException(errorMessage);
            }
            if(! ConfirmCodeGeneration()) {return;}

            GenerateAction();
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        protected abstract void GenerateAction();

        protected abstract bool ConfirmCodeGeneration();

        protected abstract (bool isOK, string errorMessage) VerifyCriticalData();
    }
}
