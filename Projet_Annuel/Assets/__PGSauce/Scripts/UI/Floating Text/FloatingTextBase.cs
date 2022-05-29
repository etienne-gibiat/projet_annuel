using System;
using DG.Tweening;
using PGSauce.Core.Extensions;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Utilities;
using PGSauce.Unity;
using UnityEngine;

namespace PGSauce.UI
{
    public abstract class FloatingTextBase : PGMonoBehaviour
    {
        [SerializeField] private UIElementWorld uiElement;
        [SerializeField, Tooltip("the part of the prefab that we'll rotate to face the target camera")]
        private Transform billboard;
        [SerializeField] private Color defaultColor = Color.black;

        private Sequence _showTween;
        private Quaternion _targetCameraRotation;
        private Camera _targetCamera;

        public Color DefaultColor => defaultColor;

        private void Update()
        {
            _targetCameraRotation = _targetCamera.transform.rotation;
            var tfm = uiElement.transform;
            billboard.transform.LookAt(tfm.position + _targetCameraRotation * Vector3.forward, _targetCameraRotation * tfm.up);
        }

        private void OnDestroy()
        {
            //TODO Replace with pool
            _showTween?.Kill();
            _showTween = null;
        }

        protected abstract void SetText(string value, Color color);

        public void Initialize(UIElementData uiElementData, string value, Camera targetCamera)
        {
            Initialize(uiElementData, value, DefaultColor, 1, targetCamera);
        }

        public void Initialize(UIElementData uiElementData, string value, Color color, float scale, Camera targetCamera)
        {
            _targetCamera = targetCamera;
            
            _showTween?.Kill();
            _showTween = DOTween.Sequence();
            _showTween
                .AppendCallback(() =>
                {
                    uiElement.SetUIData(uiElementData);
                    uiElement.ChangeVisibility(true, false);
                    SetText(value, color);
                    billboard.LocalScale(scale);
                });
                        
            var showTime = uiElement.GetAllShowingTime(uiElementData);

            _showTween
                .AppendInterval(showTime);
                        
            _showTween
                .AppendCallback(() =>
                {
                    uiElement.ChangeVisibility(false, false);
                });
            var hideTime = uiElement.GetAllHidingTime(uiElementData);
            _showTween.AppendInterval(hideTime);
            _showTween.AppendCallback((() =>
            {
                //TODO Replace with pool
                Destroy(gameObject); 
            }));
        }
    }
}