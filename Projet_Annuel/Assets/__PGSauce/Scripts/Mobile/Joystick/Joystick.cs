using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using PGSauce.Unity;
using Sirenix.OdinInspector;
using UnityEditor;

namespace PGSauce.Mobile
{
    [ExecuteInEditMode]
    public class Joystick : PGMonoBehaviour
    {
        [Header("To Customize")]
        [SerializeField, InlineEditor()] private JoystickData settings;
        [SerializeField, Required] private FingerRegisteringValidator fingerRegisteringValidator;
        [SerializeField, Required] private GestureBehavior gesture;  
        [SerializeField] private bool initAtStart;
        [Header("Debug")]
        [SerializeField] private bool showDebug;
        [SerializeField, ShowIf("showDebug")] private RectTransform dragDebug, deadDebug;
        [SerializeField, ShowIf("showDebug")] private int debugFontSize = 50;
        [Header("Events")]
        [SerializeField] private OnMoveStartHandler onMoveStart;
        [SerializeField] private OnMoveHandler onMove;
        [SerializeField] private OnMoveEndHandler onMoveEnd;
        [SerializeField] private OnShortTapHandler onShortTap;
        [Header("UI")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private CanvasScaler canvasScaler;
        [SerializeField] private RectTransform joystickRect;
        [SerializeField] private RectTransform handleRect;
        [SerializeField] private RectTransform visualRect;
        [System.Serializable] public class OnMoveStartHandler : UnityEvent<LeanFinger> { }
        [System.Serializable] public class OnMoveHandler : UnityEvent<LeanFinger, Vector2> { }
        [System.Serializable] public class OnMoveEndHandler : UnityEvent<LeanFinger>{ }
        [System.Serializable] public class OnShortTapHandler : UnityEvent<LeanFinger> { }
        private LeanFinger currentFinger;
        private Camera cam;
        private float joystickSize;
        private float dragSize;
        private float deadSize;
        private Vector2 screenSize;
        private bool touchDown;
        private Vector2 fingerScreenPosition;
        private Vector2 startScreenPosition;
        private Vector2 handleScreenPositionLerped;
        private Vector2 dragPositionLerped;
        private Vector2 swipePositionLerped;
        Vector2 screenToCanvasScalerRatio;

        public GestureBehavior Gesture => gesture;

        private void Awake()
        {
            cam = Camera.main;
            HideJoystick();
            UpdateJoystickSize();
            if(initAtStart) { EnableTouch(); }
        }
        public virtual void HideJoystick()
        {
            canvasGroup.alpha = 0f;
        }
        public virtual void ShowJoystick()
        {
            if (Application.isEditor)
            {
                if (canvasGroup && settings) canvasGroup.alpha = settings.Alpha;
            }
            else
            {
                canvasGroup.alpha = 0;
            }
        }
        public virtual void UpdateJoystickSize()
        {
            screenSize = new Vector2Int(Screen.width, Screen.height);
            screenToCanvasScalerRatio = new Vector2(canvasScaler.referenceResolution.x / screenSize.x, canvasScaler.referenceResolution.y / screenSize.y);
            joystickSize = (canvasScaler.referenceResolution.y * settings.JoystickRadius);
            deadSize = (canvasScaler.referenceResolution.y * settings.DeadRadius);
            dragSize = joystickSize + (canvasScaler.referenceResolution.y * settings.DragRadius);
            joystickRect.sizeDelta = new Vector2(joystickSize, joystickSize);
            visualRect.sizeDelta = joystickRect.sizeDelta;
            handleRect.sizeDelta = settings.HandleRadiusPercentage * joystickRect.sizeDelta;
            if (showDebug)
            {
                deadDebug.sizeDelta = new Vector2(deadSize, deadSize);
                dragDebug.sizeDelta = new Vector2(dragSize, dragSize);
            }
            deadDebug.gameObject.SetActive(showDebug);
            dragDebug.gameObject.SetActive(showDebug);
        }
#if UNITY_EDITOR
        public virtual void OnValidate()
        {
            if (Application.isPlaying) return;
            ShowJoystick();
            UpdateJoystickSize();
        }
        private void OnGUI()
        {
            var guiStyle = new GUIStyle();
            guiStyle.fontSize = debugFontSize;
            if (showDebug && touchDown)
            {
                GUI.Label(new Rect(joystickRect.position.x, Screen.height-joystickRect.position.y, 200, 100), output.ToString("F2"), guiStyle);
            }
        }
        private void Update()
        {
            OnValidate();
        }
#endif
        public virtual void OnDisable()
        {
            if (!Application.isPlaying) return;
            DisableTouch();
        }
        public virtual void OnDestroy()
        {
            if (!Application.isPlaying) return;
            DisableTouch();
        }
        public virtual void EnableTouch()
        {
            DisableTouch();
            LeanTouch.OnFingerDown += OnFingerDown;
            LeanTouch.OnFingerUpdate += OnFingerUpdate;
            LeanTouch.OnFingerUp += OnFingerUp;
            LeanTouch.OnFingerTap += OnFingerTap;
        }
        public virtual void DisableTouch()
        {
            LeanTouch.OnFingerDown -= OnFingerDown;
            LeanTouch.OnFingerUpdate -= OnFingerUpdate;
            LeanTouch.OnFingerUp -= OnFingerUp;
            LeanTouch.OnFingerTap -= OnFingerTap;
            HideJoystick();
            if(!JoystickManager.InstanceWithoutFindObjectOfType) {return;}
            JoystickManager.Instance.UnregisterFinger(currentFinger);
        }
        public void ToggleTouch(bool enabledTouch)
        {
            if (enabledTouch)
            {
                EnableTouch();
            }
            else
            {
                DisableTouch();
            }
        }

        private void OnFingerDown(LeanFinger finger)
        {
            if(! IsCurrentFinger(finger)) { return; }
            if(JoystickManager.Instance.IsFingerUsed(finger)) { return; }
            if(! fingerRegisteringValidator.Validate(finger)) { return; }
            currentFinger = finger;
            JoystickManager.Instance.RegisterFinger(currentFinger);
            touchDown = true;
            fingerScreenPosition = finger.ScreenPosition;
            startScreenPosition = fingerScreenPosition;
            dragPositionLerped = startScreenPosition;
            handleScreenPositionLerped = startScreenPosition;
            swipePositionLerped = startScreenPosition;
            joystickRect.position = startScreenPosition;
            handleRect.position = startScreenPosition;
            ShowJoystick();
            onMoveStart.Invoke(finger);
        }
        private Vector2 lerpedDelta;
        private float distance, handleDistance, computedDelta;
        private Vector2 output;
        private void OnFingerUpdate(LeanFinger finger)
        {
            if (!IsCurrentFinger(finger)) { return; }
            fingerScreenPosition = finger.ScreenPosition;
            handleScreenPositionLerped = Vector2.Lerp(handleScreenPositionLerped, fingerScreenPosition,
                settings.HandlePositionLerpCoeff);
            lerpedDelta = (handleScreenPositionLerped - startScreenPosition);
            float screenJoystickSize = joystickSize / screenToCanvasScalerRatio.y;
            float deadScreenSize = deadSize / screenToCanvasScalerRatio.y;
            float dragScreenSize = dragSize / screenToCanvasScalerRatio.y;
            Vector2 joystickDirection = lerpedDelta.normalized;
            distance = lerpedDelta.magnitude;
            handleDistance = Mathf.Clamp(distance, 0, screenJoystickSize / 2) / (screenJoystickSize / 2);
            //speed = (Mathf.Clamp(distance, 0, screenJoystickSize / 2) - deadScreenSize / 2) / (screenJoystickSize / 2 - deadScreenSize / 2);
            computedDelta = distance >= deadScreenSize / 2 ? handleDistance : 0;
            if (computedDelta < 0) computedDelta = 0;
            switch (settings.JoystickMode)
            {
                case JOYSTICK_MODE.STATIC:
                    break;
                case JOYSTICK_MODE.DRAG:
                    if(distance > dragScreenSize / 2)
                    {
                        dragPositionLerped = Vector2.Lerp(dragPositionLerped, startScreenPosition + joystickDirection * (distance - dragScreenSize / 2), settings.DragPositionLerpCoeff);
                        startScreenPosition = dragPositionLerped;
                    }
                    break;
                case JOYSTICK_MODE.SWIPE:
                    computedDelta = ((swipePositionLerped - handleScreenPositionLerped).magnitude) / (screenJoystickSize / 2) * settings.SwipeCoeff;
                    if(computedDelta > settings.MaxComputedDelta) { computedDelta = settings.MaxComputedDelta;  }
                    swipePositionLerped = Vector2.Lerp(swipePositionLerped, handleScreenPositionLerped, settings.SwipePositionLerpCoeff);
                    startScreenPosition = swipePositionLerped;
                    break;
            }
            joystickRect.position = startScreenPosition;
            handleRect.position = startScreenPosition + joystickDirection * handleDistance * screenJoystickSize / 2;
            output = new Vector2(joystickDirection.x * computedDelta, joystickDirection.y * computedDelta);
            //PGDebug.Message("JOYSTICK OUTPUT IS " + output.ToString("F3")).Log();
            onMove.Invoke(finger, output);
        }
        private void OnFingerUp(LeanFinger finger)
        {
            if (! IsCurrentFinger(finger)) { return; }
            JoystickManager.Instance.UnregisterFinger(currentFinger);
            currentFinger = null;
            touchDown = false;
            HideJoystick();
            onMoveEnd.Invoke(finger);
        }
        private void OnFingerTap(LeanFinger finger)
        {
            if(! IsCurrentFinger(finger)) { return; }
            if(! fingerRegisteringValidator.Validate(finger)) { return; }
            onShortTap.Invoke(finger);
        }
        private bool IsCurrentFinger(LeanFinger finger)
        {
            return currentFinger == null || currentFinger.Equals(finger);
        }
    }
}
