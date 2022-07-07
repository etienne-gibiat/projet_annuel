using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public interface IWfcEngine
    {
        void Destroy(UnityEngine.Object o);
        void RegisterCompleteObjectUndo(UnityEngine.Object objectToUndo);
        void RegisterCreatedObjectUndo(UnityEngine.Object objectToUndo);
    }
    
    public class UnityEngineInterface : IWfcEngine
    {
        private static UnityEngineInterface instance;

        public static UnityEngineInterface Instance => instance;

        static UnityEngineInterface()
        {
            instance = new UnityEngineInterface();
        }

        public void Destroy(Object o)
        {
            if (Application.isPlaying)
            {
                Object.Destroy(o);
                if (o is GameObject go)
                {
                    go.SetActive(false);
                }
            }
            else
            {
                Object.DestroyImmediate(o);
            }
        }

        public void RegisterCompleteObjectUndo(Object objectToUndo)
        {
        }

        public void RegisterCreatedObjectUndo(Object objectToUndo)
        {
        }
    }
}