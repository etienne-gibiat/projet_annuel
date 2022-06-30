using System;
using PGSauce.Core.SceneManagement;
using PGSauce.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Elementis.Scripts
{
    public class FogSceneLoader : PGMonoBehaviour
    {
        [SerializeField] private SceneReference scene;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == Layers.PLAYER)
            {
                SceneManager.LoadScene(scene.ScenePath);
            }
        }
    }
}