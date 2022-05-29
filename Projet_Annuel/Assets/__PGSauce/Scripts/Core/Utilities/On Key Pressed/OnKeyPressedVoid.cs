using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PGSauce.Core.Utilities
{
	public class OnKeyPressedVoid : MonoBehaviour
	{
		[SerializeField] private KeyCode key;
		[SerializeField] private UnityEvent actionDown;
        [SerializeField] private UnityEvent actionHold;
        [SerializeField] private UnityEvent actionUp;

        private void Update()
        {
            if (Input.GetKeyDown(key))
            {
                actionDown.Invoke();
            }
            if (Input.GetKey(key))
            {
                actionHold.Invoke();
            }
            if (Input.GetKeyUp(key))
            {
                actionUp.Invoke();
            }
        }
    }
}
