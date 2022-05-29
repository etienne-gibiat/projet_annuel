using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.PGDebugging;

namespace PGSauce.NoDevSuite.Example
{
    public class TestSpawnObjects : MonoBehaviour
    {
        public PGEventGameObjectData onJump;
        
        public void Jump()
        {
            PGDebug.Message("Player Jumped").Log();
            onJump.Raise(gameObject);
        }
    }
}
