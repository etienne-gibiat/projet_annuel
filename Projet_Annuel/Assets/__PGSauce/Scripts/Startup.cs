using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using PGSauce.Core.PGDebugging;
using PGSauce.Save;
using PGSauce.Unity;
using UnityEngine.SceneManagement;

namespace PGSauce.PGStartup
{
    public class Startup : PGMonoBehaviour
    {
#if UNITY_ASSERTIONS
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void DisableAnimancerWarnings()
        {
            Animancer.OptionalWarning.EndEventInterrupt.Disable();
    
            // You could disable OptionalWarning.All, but that is not recommended for obvious reasons.
        }
#endif

        private void Awake()
        {
            if (PgSave.Instance.IsGameLaunchedForTheFirstTime)
            {
                PGDebug.Message("Game Launched For the first time").Log();
                PgSave.Instance.GameLaunchedForTheFirstTime.SaveData(false);
            }
            else
            {
                PGDebug.Message("Game NOT Launched the first time").Log();
            }
            
            SceneManager.LoadScene(1);
        }
    }
}
