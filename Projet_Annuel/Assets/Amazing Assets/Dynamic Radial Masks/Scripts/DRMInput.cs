using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif


namespace AmazingAssets.DynamicRadialMasks
{
    static public class DRMInput
    {
#if ENABLE_INPUT_SYSTEM
        static public bool GetKeyDown(Key key)
        {
            return Keyboard.current[key].wasPressedThisFrame;
        }

        static public bool GetKey(Key key)
        {
            return Keyboard.current[key].isPressed;
        }

        static public bool GetKeyUp(Key key)
        {
            return Keyboard.current[key].wasReleasedThisFrame;
        }

        static public bool GetLeftMouseButtonDown()
        {
            return Mouse.current.leftButton.wasPressedThisFrame;
        }

        static public bool GetLeftMouseButton()
        {
            return Mouse.current.leftButton.isPressed;
        }

        static public Vector3 GetMousePosition()
        {
            return Mouse.current.position.ReadValue();
        }
#else
        static public bool GetKeyDown(KeyCode key)
        {
            return Input.GetKeyDown(key);
        }

        static public bool GetKey(KeyCode key)
        {
            return Input.GetKey(key);
        }

        static public bool GetKeyUp(KeyCode key)
        {
            return Input.GetKeyUp(key);
        }

        static public bool GetLeftMouseButtonDown()
        {
            return Input.GetMouseButtonDown(0);
        }

        static public bool GetLeftMouseButton()
        {
            return Input.GetMouseButton(0);
        }

        static public Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }
#endif
    }
}