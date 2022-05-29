using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using GameTroopers.UI;
using PGSauce.AudioManagement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.UI
{
    public abstract class PGMenu : Menu, IOnMenuLoaded, IOnMenuShowStarted, IOnMenuHideStarted, IOnMenuShowEnded, IOnMenuHideEnded, IOnMenuFocused, IOnMenuUnfocused
    {
        private List<UIElement> _animatedElements;
        private float _hidingTime;
        private float _showingTime;
        private bool _visible;

        protected override Vector2 GetMenuOffScreenPosition()
        {
            return GetMenuOnScreenPosition();
        }

        private bool UseCustomShowAnim => _showingTime > 0;

        private bool UseCustomHideAnim => _hidingTime > 0;

        private IEnumerator CustomShowAction(Menu menu, Vector2 startposition, Vector2 endposition, float duration)
        {
            ChangeVisibility(true, false);
            yield return new WaitForSecondsRealtime(duration);
        }

        private IEnumerator CustomHideAction(Menu menu, Vector2 startposition, Vector2 endposition, float duration)
        {
            ChangeVisibility(false, false);
            yield return new WaitForSecondsRealtime(duration);
        }
        
        

        public float GetAllHidingTime()
        {
            if (_hidingTime != 0)
            {
                return _hidingTime;
            }

            foreach (var uiA in _animatedElements)
            {
                var uiAHidingTime = uiA.GetAllHidingTime();

                if (uiAHidingTime > _hidingTime)
                {
                    _hidingTime = uiAHidingTime;
                }
            }
            return _hidingTime;
        }
        
        public float GetAllShowingTime()
        {
            if (_showingTime != 0)
            {
                return _showingTime;
            }

            foreach (var uiA in _animatedElements)
            {
                var uiAShowingTime = uiA.GetAllShowingTime();

                if (uiAShowingTime > _showingTime)
                {
                    _showingTime = uiAShowingTime;
                }
            }
            return _showingTime;
        }

        [Button, DisableInEditorMode]
        public void ChangeVisibility(bool visible, bool noSounds = false)
        {
            foreach (var element in _animatedElements)
            {
                element.ChangeVisibility(visible, noSounds);
            }

            _visible = visible;
        }

        public void OnMenuLoaded()
        {
            LoadAnimatedElements();
            _hidingTime = GetAllHidingTime();
            _showingTime = GetAllShowingTime();
            HideAnimType = UseCustomHideAnim ? AnimType.Custom : AnimType.Instant;
            ShowAnimType = UseCustomShowAnim ? AnimType.Custom : AnimType.Instant;

            if (UseCustomHideAnim)
            {
                HideAction = CustomHideAction;
            }

            if (UseCustomShowAnim)
            {
                ShowAction = CustomShowAction;
            }
            
            _visible = false;
            CustomOnMenuLoaded();
        }

        protected internal sealed override void HandleGoBack()
        {
            base.HandleGoBack();
            OnBackEvent();
        }

        protected virtual void OnBackEvent()
        {
            
        }

        protected virtual void CustomOnMenuLoaded()
        {
            
        }

        public void OnMenuShowStarted()
        {
            AnimationDuration = _showingTime;
            CustomOnShowStarted();
        }

        protected virtual void CustomOnShowStarted()
        {
        }

        public void OnMenuHideStarted()
        {
            AnimationDuration = _hidingTime;
            CustomOnHideStarted();
        }
        
        protected virtual void CustomOnHideStarted()
        {
        }
        
        
        private void LoadAnimatedElements()
        {
            _animatedElements = GetComponentsInChildren<UIElement>().ToList();
            foreach (var element in _animatedElements)
            {
                element.Initialize();
            }
        }

        public void OnMenuShowEnded()
        {
            CustomOnShowEnded();
        }
        
        protected virtual void CustomOnShowEnded()
        {
        }

        public void OnMenuHideEnded()
        {
            CustomOnHideEnded();
        }
        
        protected virtual void CustomOnHideEnded()
        {
        }

        public void OnMenuFocused()
        {
            CustomOnFocused();
        }
        
        protected virtual void CustomOnFocused()
        {
        }

        public void OnMenuUnfocused()
        {
            
            CustomOnUnFocused();
        }
        
        protected virtual void CustomOnUnFocused()
        {
        }
    }
}