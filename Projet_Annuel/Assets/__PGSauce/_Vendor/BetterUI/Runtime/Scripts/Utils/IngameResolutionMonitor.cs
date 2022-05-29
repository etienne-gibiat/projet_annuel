using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheraBytes.BetterUi
{
    [AddComponentMenu("Better UI/In-Game Resolution Monitor", 30)]
    public class IngameResolutionMonitor : MonoBehaviour
    {
        public static GameObject Create()
        {
            GameObject go = new GameObject("IngameResolutionMonitor");
            go.AddComponent<IngameResolutionMonitor>();
            GameObject.DontDestroyOnLoad(go);

            return go;
        }

        private void OnEnable()
        {           
            SceneManager.sceneLoaded += SceneLoaded;
        }

        private void OnDisable()
        {           
            SceneManager.sceneLoaded -= SceneLoaded;
        }

        private void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ResolutionMonitor.SetDirty();
            ResolutionMonitor.Update();
        }

#if !(UNITY_EDITOR)
        void Update()
        {
            ResolutionMonitor.Update();
        }
#endif
    }
}
