using UnityEditor;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    class UnityEditorEngineInterface : IWfcEngine
    {
        private readonly bool registerUndo;
        private readonly string undoName;

        public UnityEditorEngineInterface(bool registerUndo, string undoName)
        {
            this.registerUndo = registerUndo;
            this.undoName = undoName;
        }

        public void Destroy(UnityEngine.Object o)
        {
            if (registerUndo)
            {
                Undo.DestroyObjectImmediate(o);
            }
            else
            {
                if (Application.isPlaying)
                {
                    GameObject.Destroy(o);
                    if (o is GameObject go)
                        go.SetActive(false);
                }
                else
                {
                    GameObject.DestroyImmediate(o);
                }
            }
        }
        public void RegisterCompleteObjectUndo(UnityEngine.Object objectToUndo)
        {
            if (registerUndo)
            {
                Undo.RegisterCompleteObjectUndo(objectToUndo, undoName);
            }
        }

        public void RegisterCreatedObjectUndo(UnityEngine.Object objectToUndo)
        {
            if (registerUndo)
            {
                Undo.RegisterCreatedObjectUndo(objectToUndo, undoName);
            }
        }
    }
}