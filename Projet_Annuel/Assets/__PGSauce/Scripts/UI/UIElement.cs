using System;
using DG.Tweening;
using PGSauce.AudioManagement;
using PGSauce.Core.Extensions;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Utilities;
using PGSauce.Unity;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PGSauce.UI
{
    public class UIElement : PGMonoBehaviour, UIElementBase
    {
        [SerializeField] private UIElementData elementData;
        [SerializeField, Tooltip("Should this menu deactivate while its not visible on the screen.")]
        public bool deactivateWhileInvisible = true;
        [SerializeField] private bool visible;
        
        private PgSfxBase showClip => elementData.ShowClip;
        private PgSfxBase hideClip => elementData.HideClip;
        

        private Vector3AnimationSection MovementSection => elementData.MovementSection;
        [SerializeField, Tooltip("The position the element will move to when it's hiding."), ShowIf("@HasUIData && MovementSection.UseSection")]
        private ScreenSides hidingPosition;
        [SerializeField, Tooltip("The gap between the element and the edge of the screen while it's hiding (in percentage of element's width or height).")]
        private float edgeGap = 0.25f;
        
        private Vector3AnimationSection RotationSection => elementData.RotationSection;
        private Vector3AnimationSection ScaleSection => elementData.ScaleSection;
        private FloatAnimationSection OpacitySection => elementData.OpacitySection;
        [SerializeField, Tooltip("The component you want to change it's opacity (Graphic or CanvasGroup components).")] private Component target;
        private FloatAnimationSection SliceSection => elementData.SliceSection;
        [SerializeField] private Image sliceImage;


        #region Private

        private float _showingTime;
        private float _hidingTime;
        private RectTransform _rectTransform;
        private CanvasScaler _parentCanvasScaler;
        private RectTransform _parentCanvasScalerRectTransform;
        private RectTransform _directParentRectTransform;
        private float _canvasHalfHeight;
        private float _canvasHalfWidth;
        private float _directParentHalfHeight;
        private float _directParentHalfWidth;
        private float _rectTransformHeight;
        private float _rectTransformWidth;
        private Tween _soundTween;
        private Tween _hideTweenDelay;
        #endregion

        public enum ScreenSides { Top, Bottom, Left, Right, TopLeftCorner, TopRightCorner, BotLeftCorner, BotRightCorner }
        
        [CalledByOdin]
        public bool HasUIData => elementData != null;

        public float GetAllHidingTime()
        {
            return GetAllHidingTime(elementData);
        }

        public float GetAllShowingTime()
        {
            return GetAllShowingTime(elementData);
        }

        public void ChangeVisibility(bool v, bool noSounds = false)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            if (visible != v && !noSounds)
            {
                
                if (v)
                {
                    if (showClip)
                    {
                        PgAudioManager.Sfx.Play(showClip);
                    }
                }
                else
                {
                    if (hideClip)
                    {
                        PgAudioManager.Sfx.Play(hideClip);
                    }
                }
            }

            visible = v;

            //TODO Refactor with the sections below
            if (MovementSection.UseSection)
            {
                MovementSection.Control(gameObject, ve => _rectTransform.position = ve, visible, false);
            }

            if (RotationSection.UseSection)
            {
                RotationSection.Control(gameObject, vector3 => _rectTransform.eulerAngles = vector3,  visible, false);
            }
            
            if (ScaleSection.UseSection)
            {
                ScaleSection.Control(gameObject, vector3 => _rectTransform.localScale = vector3,  visible, false);
            }
            
            if (OpacitySection.UseSection)
            {
                if (!target)
                {
                    FindTargetFader();
                }

                if (target)
                {
                    
                    if (target is Graphic graphic)
                    {
                        OpacitySection.Control(gameObject, val =>
                        {
                            graphic.Alpha(val);
                        }, visible, false);
                        
                    }
                    else if (target is CanvasGroup canvasGroup)
                    {
                        OpacitySection.Control(gameObject,val => canvasGroup.alpha = val,  visible, false);
                    }
                }
            }
            
            if (SliceSection.UseSection)
            {
                if (!sliceImage)
                {
                    FindSliceImage();
                }

                if (sliceImage)
                {
                    SliceSection.Control(gameObject,slice => sliceImage.fillAmount = slice, visible, false);
                }
            }
            
            if (deactivateWhileInvisible)
            {
                if (visible)
                {
                    _hideTweenDelay?.Kill();
                }
                else
                {
                    _hideTweenDelay = DOVirtual.DelayedCall(_hidingTime, () =>
                    {
                        gameObject.SetActive(false);
                        _hideTweenDelay = null;
                    });
                }
            }
        }

        public void Initialize()
        {
            elementData = elementData == null ? ScriptableObject.CreateInstance<UIElementData>() : elementData.CloneScriptableObject();
            InitUIData();

            _rectTransform = GetComponent<RectTransform>();
            _parentCanvasScaler = GetComponentInParent<CanvasScaler>();
            _parentCanvasScalerRectTransform = _parentCanvasScaler.GetComponent<RectTransform>();
            if (transform.parent)
            {
                _directParentRectTransform = transform.parent.GetComponent<RectTransform>();
            }

            var lossyScale = _parentCanvasScalerRectTransform.lossyScale;

            _canvasHalfWidth = lossyScale.x * _parentCanvasScalerRectTransform.rect.width / 2f;
            _canvasHalfHeight = lossyScale.y * _parentCanvasScalerRectTransform.rect.height / 2f;

            if (_directParentRectTransform)
            {
                _directParentHalfWidth = lossyScale.x * _directParentRectTransform.rect.width / 2f;
                _directParentHalfHeight = lossyScale.y * _directParentRectTransform.rect.height / 2f;
            }

            _rectTransformWidth = lossyScale.x * _rectTransform.rect.width;
            _rectTransformHeight = lossyScale.y * _rectTransform.rect.height;

            
            //TODO rethink architecture to avoid setting shown and hidden values
            var position = _rectTransform.position;
            MovementSection.ComingToShownValue = position;
            MovementSection.ComingFromShownValue = position;
            var hiddenPos = GetHidingPosition(hidingPosition, edgeGap);
            MovementSection.ComingFromHiddenValue = hiddenPos;
            MovementSection.ComingToHiddenValue = hiddenPos;

            var eulerAngles = _rectTransform.eulerAngles;
            RotationSection.ComingFromShownValue = eulerAngles;
            RotationSection.ComingToShownValue = eulerAngles;

            var localScale = _rectTransform.localScale;
            ScaleSection.ComingFromShownValue = localScale;
            ScaleSection.ComingToShownValue = localScale;

            FindTargetFader();
            FindSliceImage();
        }

        private void InitUIData()
        {
            elementData.Init();

            _hidingTime = GetAllHidingTime();
            _showingTime = GetAllShowingTime();
        }

        private void FindSliceImage()
        {
            if (!SliceSection.UseSection){return;}
            
            if (sliceImage)
            {
                SliceSection.ComingFromShownValue = sliceImage.fillAmount;
                SliceSection.ComingToShownValue = sliceImage.fillAmount;
            }
            else
            {
                sliceImage = GetComponent<Image>();
                if (sliceImage)
                {
                    SliceSection.ComingFromShownValue = sliceImage.fillAmount;
                    SliceSection.ComingToShownValue = sliceImage.fillAmount;
                }
                else 
                {
                    PGDebug.SetContext(gameObject).Message($"There's no Image component on {gameObject.name}, must have one to use the Slice transition.").LogError();
                }
            }
        }

        private void FindTargetFader()
        {
            if(!OpacitySection.UseSection){return;}
            
            if (target)
            {
                if (target is Graphic tfGraphic)
                {
                    OpacitySection.ComingFromShownValue = tfGraphic.color.a;
                    OpacitySection.ComingToShownValue = tfGraphic.color.a;
                }
                else if (target is CanvasGroup tfCanvasGroup)
                {
                    var alpha = tfCanvasGroup.alpha;
                    OpacitySection.ComingFromShownValue = alpha;
                    OpacitySection.ComingToShownValue = alpha;
                }
            }
            else
            {
                var graphic = GetComponent<Graphic>();
                target = graphic;
                if (target)
                {
                    OpacitySection.ComingFromShownValue = graphic.color.a;
                    OpacitySection.ComingToShownValue = graphic.color.a;
                }
                else
                {
                    var canvasGroup = GetComponent<CanvasGroup>();
                    target = canvasGroup;
                    if (target)
                    {
                        var alpha = canvasGroup. alpha;
                        OpacitySection.ComingFromShownValue = alpha;
                        OpacitySection.ComingToShownValue =alpha;
                    }
                    else
                    {
                        PGDebug.SetContext(gameObject).Message($"Theres no Image, Text, RawImage nor CanvasGroup component on {gameObject.name}, must have one of them to use the Opacity transition.").LogError();
                    }
                }
            }
        }

        private Vector3 GetHidingPosition(ScreenSides hidingPos, float edge)
        {
            var pos = new Vector3();
            var x = 0f;
            var y = 0f;

            var distanceToEdge = new Vector2(_rectTransform.pivot.x, _rectTransform.pivot.y);
            var originalPosition = MovementSection.ComingFromShownValue;
            
            switch (hidingPos)
        {
            case ScreenSides.Top:
                y = _parentCanvasScalerRectTransform.position.y + _canvasHalfHeight + _rectTransformHeight * (distanceToEdge.y + edge);
                pos = new Vector3(originalPosition.x, y, originalPosition.z);
                break;
            case ScreenSides.Bottom:
                y = _parentCanvasScalerRectTransform.position.y - _canvasHalfHeight - _rectTransformHeight * (1 - distanceToEdge.y + edge);
                pos = new Vector3(originalPosition.x, y, originalPosition.z);
                break;
            case ScreenSides.Left:
                x = _parentCanvasScalerRectTransform.position.x - _canvasHalfWidth - _rectTransformWidth * (1 - distanceToEdge.x + edge);
                pos = new Vector3(x, originalPosition.y, originalPosition.z);
                break;
            case ScreenSides.Right:
                x = _parentCanvasScalerRectTransform.position.x + _canvasHalfWidth + _rectTransformWidth * (distanceToEdge.x + edge);
                pos = new Vector3(x, originalPosition.y, originalPosition.z);
                break;
            case ScreenSides.TopLeftCorner:
                y = _parentCanvasScalerRectTransform.position.y + _canvasHalfHeight + _rectTransformHeight * (distanceToEdge.y + edge);
                x = _parentCanvasScalerRectTransform.position.x - _canvasHalfWidth - _rectTransformWidth * (1 - distanceToEdge.x + edge);
                pos = new Vector3(x, y, originalPosition.z);
                break;
            case ScreenSides.TopRightCorner:
                y = _parentCanvasScalerRectTransform.position.y + _canvasHalfHeight + _rectTransformHeight * (distanceToEdge.y + edge);
                x = _parentCanvasScalerRectTransform.position.x + _canvasHalfWidth + _rectTransformWidth * (distanceToEdge.x + edge);
                pos = new Vector3(x, y, originalPosition.z);
                break;
            case ScreenSides.BotLeftCorner:
                y = _parentCanvasScalerRectTransform.position.y - _canvasHalfHeight - _rectTransformHeight * (1 - distanceToEdge.y + edge);
                x = _parentCanvasScalerRectTransform.position.x - _canvasHalfWidth - _rectTransformWidth * (1 - distanceToEdge.x + edge);
                pos = new Vector3(x, y, originalPosition.z);
                break;
            case ScreenSides.BotRightCorner:
                y = _parentCanvasScalerRectTransform.position.y - _canvasHalfHeight - _rectTransformHeight * (1 - distanceToEdge.y + edge);
                x = _parentCanvasScalerRectTransform.position.x + _canvasHalfWidth + _rectTransformWidth * (distanceToEdge.x + edge);
                pos = new Vector3(x, y, originalPosition.z);
                break;
        }
        return pos;
        }

        public void SetUIData(UIElementData numberUIElementData)
        {
            elementData = numberUIElementData.CloneScriptableObject();
            InitUIData();
        }

        public float GetAllHidingTime(UIElementData numberUIElementData)
        {
            var hidingTime = 0.0f;
            hidingTime = numberUIElementData.CheckGreatestHidingTime(hidingTime);
            
            return hidingTime;
        }

        public float GetAllShowingTime(UIElementData numberUIElementData)
        {
            var showingTime = 0.0f;
            showingTime = numberUIElementData.CheckGreatestShowingTime(showingTime);
            
            return showingTime;
        }
    }
}