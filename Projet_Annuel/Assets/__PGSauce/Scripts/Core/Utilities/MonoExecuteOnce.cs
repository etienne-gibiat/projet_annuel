using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace PGSauce.Core.Utilities
{
    public class MonoExecuteOnce : MonoBehaviour
    {
        [SerializeField] private UnityEvent action;
        private bool executed;

        private void Awake()
        {
            executed = false;
        }

        public void Execute()
        {
            if(executed) { return; }
            action.Invoke();
        }
    }
}
