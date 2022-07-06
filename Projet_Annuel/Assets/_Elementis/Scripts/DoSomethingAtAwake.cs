using System;
using PGSauce.Save;
using PGSauce.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace _Elementis.Scripts
{
    public class DoSomethingAtAwake : PGMonoBehaviour
    {
        [SerializeField] private SavedDataBool doIt;
        [SerializeField] private UnityEvent ifTrue;
        [SerializeField] private UnityEvent ifFalse;

        private void Awake()
        {
            if (doIt.SavedValue)
            {
                ifTrue.Invoke();
            }
            else
            {
                ifFalse.Invoke();
            }
        }
    }
}