using PGSauce.Core.Strings;
using UnityEngine;
using Sirenix.OdinInspector;

namespace PGSauce.Mobile
{
     public enum JOYSTICK_MODE
    {
        STATIC,
        DRAG,
        SWIPE
    }
    [CreateAssetMenu(fileName = "Joystick Data", menuName = MenuPaths.MenuBase + "Mobile/Joystick Data")]
    public class JoystickData : ScriptableObject
    {
        [SerializeField] private JOYSTICK_MODE joystickMode = JOYSTICK_MODE.DRAG;
        [SerializeField, Tooltip("Alpha of the Joystick UI"), Range(0, 1)] private float alpha;
        [SerializeField, Tooltip("Normalized radius, depending on canvas scale height"), Range(0, 1)] private float joystickRadius = 1;
        [SerializeField, Tooltip("Visual only, percentage of joystick size"), Range(0, 1)] private float handleRadiusPercentage = 1;
        [SerializeField, Tooltip("Normalized radius for dead zone"), Range(0, 1)] private float deadRadius = 1;
        [SerializeField, Tooltip("Handle Lerping"), Range(0, 1)] private float handlePositionLerpCoeff = 1;
        [Header("Drag options")]
        [SerializeField, Tooltip("Normalized radius, used for moving the joystick with the finger"), Range(0, 1), ShowIf("isDrag")] private float dragRadius = 1;
        [SerializeField, Tooltip("Drag Lerping"), Range(0, 1), ShowIf("isDrag")] private float dragPositionLerpCoeff = 1;
        [Header("Swipe options")]
        [SerializeField, Tooltip("Swipe Lerping"), Range(0, 1), ShowIf("isSwipe")] private float swipePositionLerpCoeff = 1;
        [SerializeField, Tooltip("Scale finger movement"), ShowIf("isSwipe")] private float swipeCoeff = 1;
        [SerializeField, Tooltip("Max Computed delta"), ShowIf("isSwipe")] private float maxComputedDelta = 10;
        public float JoystickRadius { get => joystickRadius; }
        public float DragRadius { get => dragRadius; }
        public float DeadRadius { get => deadRadius; }
        public float HandleRadiusPercentage { get => handleRadiusPercentage; }
        public float Alpha { get => alpha; }
        public float HandlePositionLerpCoeff { get => handlePositionLerpCoeff; }
        public JOYSTICK_MODE JoystickMode { get => joystickMode; }
        public float DragPositionLerpCoeff { get => dragPositionLerpCoeff; }
        public float SwipePositionLerpCoeff { get => swipePositionLerpCoeff; }
        public float SwipeCoeff { get => swipeCoeff; }
        public float MaxComputedDelta { get => maxComputedDelta; }
        private bool isDrag => joystickMode == JOYSTICK_MODE.DRAG;
        private bool isSwipe => joystickMode == JOYSTICK_MODE.SWIPE;
    }
}
