using System;
using PGSauce.Unity;
using UnityEngine.SceneManagement;

namespace _Elementis.Scripts
{
    public class Managers : PGMonoBehaviour
    {
        private void Awake()
        {
            SceneManager.LoadScene(2);
        }
    }
}