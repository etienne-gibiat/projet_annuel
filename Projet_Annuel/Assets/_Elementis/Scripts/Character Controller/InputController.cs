using System;
using PGSauce.Core.PGDebugging;
using PGSauce.Unity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Elementis.Scripts.Character_Controller
{
    public class InputController : PGMonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool toggleCameraLock;
        public bool isAiming;
        public bool isShooting;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;
        
        public bool CanUseInputs { get; set; }
        
        public void TakeControlFromPlayer()
        {
	        move = Vector2.zero;
	        sprint = false;
	        isAiming = false;
	        isShooting = false;
        }

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
	        if (!CanUseInputs)
	        {
		        return;
	        }
            move = value.Get<Vector2>();
        }

        public void OnLook(InputValue value)
        {
	        if (!CanUseInputs)
	        {
		        return;
	        }
        	if(cursorInputForLook)
        	{
	            look = value.Get<Vector2>();
        	}
        }

        public void OnJump(InputValue value)
        {
	        if (!CanUseInputs)
	        {
		        return;
	        }
	        jump = value.isPressed;
        }

        public void OnSprint(InputValue value)
        {
	        if (!CanUseInputs)
	        {
		        return;
	        }
	        sprint = value.isPressed;
        }

        public void OnCameraLock(InputValue value)
        {
	        if (!CanUseInputs)
	        {
		        return;
	        }
	        if (value.isPressed)
	        {
		        toggleCameraLock = !toggleCameraLock;
	        }
        }
        
        public void OnAim(InputValue value)
        {
	        if (!CanUseInputs)
	        {
		        return;
	        }
	        isAiming = value.isPressed;
        }
        
        public void OnShoot(InputValue value)
        {
	        if (!CanUseInputs)
	        {
		        return;
	        }
	        if (value.isPressed && isAiming)
	        {
		        isShooting = true;
	        }
	        else
	        {
		        isShooting = false;
	        }
        }
#endif

	    private void Awake()
	    {
		    CanUseInputs = true;
	    }

	    private void OnApplicationFocus(bool hasFocus)
        {
        	SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
        	Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        
    }
}