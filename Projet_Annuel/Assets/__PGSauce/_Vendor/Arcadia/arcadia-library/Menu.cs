using System;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Unity;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GameTroopers.UI
{
    /// <summary>
    /// Base class of all the UI panels that need to be managed by the <see cref="MenuManager"/>. 
    /// This class is responsible of managing the lifecycle the menu and providing access to its properties.
    /// </summary>
    public class Menu : PGMonoBehaviour
    {
        public enum AnimType
        {
            Instant = 0,
            Linear,
            EaseIn,
            EaseOut,
            EaseInBack,
            EaseOutBack,
            Custom
        }

        public Menu()
        {
            m_eventListeners = new MultitypeSet();

            ShowAnimType = AnimType.Instant;
            m_hideAnimType = AnimType.Instant;

            m_firstSelected = null;
        }

        /// <summary>
        /// Gets the name of the layer where this menu is contained
        /// </summary
        public string Layer
        {
            get { return m_layer; }
            set { m_layer = value; }
        }

        /// <summary>
        /// Returns true if this menu is visible on the screen.
        /// </summary>
        public bool IsActive
        {
            get { return gameObject.activeSelf; }
        }

        public void AddListener<T>(T listener)
        {
            m_eventListeners.Add(listener);
        }

        public void RemoveListener<T>(T listener)
        {
            m_eventListeners.Remove(listener);
        }

        public bool IsPlayingAnimation
        {
            get; private set;
        }

        internal bool HasFocus
        {
            get; private set;
        }

        internal bool IsGoBackEnabled => m_isGoBackEnabled;

        internal bool ShouldSwallowFocus => m_shouldSwallowFocus;

        /// <summary>
        /// Gets and sets the action that will play the show animation for this menu.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when trying to set an 
        /// action without setting the show animation type mode to "Custom"</exception>
        protected MenuAnimationDelegate ShowAction
        {
            get => m_showAction;

            set
            {
                if (ShowAnimType == AnimType.Custom)
                {
                    m_showAction = value;
                }
                else
                {
                    throw new InvalidOperationException("Can't set a custom animation with selected animation type. Change it to \"Custom\" mode first.");
                }
            }
        }

        /// <summary>
        /// Gets and sets the action that will play the hide animation for this menu.
        /// <exception cref="InvalidOperationException">Thrown when trying to set an 
        /// action without setting the hide animation type to "Custom"</exception>
        /// </summary>
        protected MenuAnimationDelegate HideAction
        {
            get => m_hideAction;

            set
            {
                if (HideAnimType == AnimType.Custom)
                {
                    m_hideAction = value;
                }
                else
                {
                    throw new InvalidOperationException("Can't set a custom animaton with selected animation type. Change it to \"Custom\" mode first.");
                }
            }
        }

        /// <summary>
        /// <see cref="GameObject"/>
        /// </summary>
        private new GameObject gameObject
        {
            get
            {
                if (m_gameObject == null)
                    m_gameObject = base.gameObject;
                return m_gameObject;
            }
        }

        /// <summary>
        /// <see cref="RectTransform"/>
        /// </summary>
        private RectTransform rectTransform
        {
            get
            {
                if (m_rectTransform == null)
                    m_rectTransform = GetComponent<RectTransform>();
                return m_rectTransform;
            }
        }
        
        

        public float AnimationDuration
        {
            get => m_animationDuration;
            set => m_animationDuration = value;
        }

        public AnimType HideAnimType
        {
            get => m_hideAnimType;
            set => m_hideAnimType = value;
        }

        public AnimType ShowAnimType
        {
            get => m_showAnimType;
            set => m_showAnimType = value;
        }

        /// <summary>
        /// Gets the position of the menu while it is hidden. This value is also used to 
        /// set the destination position of <see cref="HideAction"/>.
        /// </summary>
        /// <returns>Position of the menu when it is hidden</returns>
        protected virtual Vector2 GetMenuOffScreenPosition()
        {
            var canvasSize = m_menuManager.Canvas.GetComponent<RectTransform>().sizeDelta;
            return new Vector2(0, -canvasSize.y);
        }

        /// <summary>
        /// Gets the position of the menu while it is visible. This value is also used to 
        /// set the destination position of <see cref="HideAction"/>.
        /// </summary>
        /// <returns>Position of the menu when it is visible</returns>
        protected virtual Vector2 GetMenuOnScreenPosition()
        {
            return new Vector2(0, 0);
        }

        internal void Show(bool shouldPlayAnimation = true)
        {
            if (shouldPlayAnimation && ShowAction == null)
            {
                throw new ArgumentException("Cannot play show animation because it's not set");
            }

            gameObject.SetActive(true);
            gameObject.transform.SetAsLastSibling();

            m_lastSelected = null;

            StartCoroutine(ShowAnimationCoroutine(shouldPlayAnimation));
        }

        internal void Hide(bool shouldPlayAnimation = true)
        {
            if (shouldPlayAnimation && HideAction == null)
            {
                throw new ArgumentException("Cannot play hide animation because it's not set");
            }

            m_lastSelected = null;

            StartCoroutine(HideAnimationCoroutine(shouldPlayAnimation));
        }

        internal void OnLoadMenu(MenuManager menuManager)
        {
            m_menuManager = menuManager;

            m_selectables = GetComponentsInChildren<Selectable>(true);

            mSelectableColors = new Dictionary<Selectable, ColorBlock>();
            _selectablesInteractableAtStart = new Dictionary<Selectable, bool>();

            foreach (var selectable in m_selectables)     
            {
                _selectablesInteractableAtStart.Add(selectable, selectable.interactable);
                mSelectableColors.Add(selectable, selectable.colors);
            }

            rectTransform.anchoredPosition = GetMenuOffScreenPosition();

            if (ShowAnimType != AnimType.Custom)
            {
                m_showAction = AnimationHelper.GetDefaultAnimation(ShowAnimType);
            }

            if (HideAnimType != AnimType.Custom)
            {
                m_hideAction = AnimationHelper.GetDefaultAnimation(HideAnimType);
            }

            SetInteractableButtons(false);
            
            if (m_eventsEnabled)
            {
                m_eventListeners.AddRange(GetMenuListeners<IOnMenuLoaded>());
                m_eventListeners.AddRange(GetMenuListeners<IOnMenuShowStarted>());
                m_eventListeners.AddRange(GetMenuListeners<IOnMenuShowEnded>());
                m_eventListeners.AddRange(GetMenuListeners<IOnMenuHideStarted>());
                m_eventListeners.AddRange(GetMenuListeners<IOnMenuHideEnded>());
                m_eventListeners.AddRange(GetMenuListeners<IOnMenuFocused>());
                m_eventListeners.AddRange(GetMenuListeners<IOnMenuUnfocused>());

                DispatchOnMenuLoaded();
            }
        }

        internal void SetSelectedButton()
        {
            if (m_lastSelected != null)
            {
                m_menuManager.EventSystem.SetSelectedGameObject(m_lastSelected);
            }
            else if (m_firstSelected != null)
            {
                m_menuManager.EventSystem.SetSelectedGameObject(m_firstSelected);
            }
            else
            {
                m_menuManager.EventSystem.SetSelectedGameObject(null);
            }
        }

        private void SetInteractableButtons(bool enable)
        {
            if (m_inputEnabled != enable)
            {
                foreach (var selectable in m_selectables)
                {
                    selectable.interactable = enable && _selectablesInteractableAtStart[selectable];
                    SetSelectableColor(enable, selectable);
                }

                m_inputEnabled = enable;
            }
        }

        private void SetSelectableColor(bool enable, Selectable selectable)
        {
            var newColorBlock = selectable.colors;
            newColorBlock.disabledColor = enable ? mSelectableColors[selectable].disabledColor : Color.white;
            selectable.colors = newColorBlock;
        }


        protected internal virtual void HandleGoBack()
        {
            m_menuManager.HideMenu(this);
        }

        private void OnDestroy()
        {
            ClearMenuListeners();
        }

        private void StoreLastSelectable()
        {
            foreach (Selectable selectable in m_selectables)
            {
                if (m_menuManager.EventSystem.currentSelectedGameObject != null &&
                    m_menuManager.EventSystem.currentSelectedGameObject.transform.IsChildOf(this.transform))
                {
                    m_lastSelected = m_menuManager.EventSystem.currentSelectedGameObject;
                    break;
                }
            }
        }

        private void ClearMenuListeners()
        {
            m_eventListeners.Clear();
        }

        private void OnMenuShowStarted()
        {
            if (m_eventsEnabled)
                DispatchOnMenuShowStarted();
        }

        private void OnMenuShowEnded()
        {
            if (m_eventsEnabled)
                DispatchOnMenuShowEnded();
        }

        private void OnMenuHideStarted()
        {
            if (m_eventsEnabled)
                DispatchOnMenuHideStarted();
        }

        private void OnMenuHideEnded()
        {
            if (m_eventsEnabled)
                DispatchOnMenuHideEnded();
        }

        internal void OnFocusedMenu()
        {
            HasFocus = true;

            SetInteractableButtons(true);
            SetSelectedButton();

            if (m_eventsEnabled)
                DispatchOnMenuFocused();
        }

        internal void OnUnfocusedMenu()
        {
            HasFocus = false;

            StoreLastSelectable();
            SetInteractableButtons(false);

            if (m_eventsEnabled)
                DispatchOnMenuUnfocused();
        }

        private void DispatchOnMenuLoaded()
        {
            m_eventListeners.ForEach<IOnMenuLoaded>(x => x.OnMenuLoaded());
        }

        private void DispatchOnMenuShowStarted()
        {
            m_eventListeners.ForEach<IOnMenuShowStarted>(x => x.OnMenuShowStarted());
        }

        private void DispatchOnMenuShowEnded()
        {
            m_eventListeners.ForEach<IOnMenuShowEnded>(x => x.OnMenuShowEnded());
        }

        private void DispatchOnMenuHideStarted()
        {
            m_eventListeners.ForEach<IOnMenuHideStarted>(x => x.OnMenuHideStarted());
        }

        private void DispatchOnMenuHideEnded()
        {
            m_eventListeners.ForEach<IOnMenuHideEnded>(x => x.OnMenuHideEnded());
        }

        private void DispatchOnMenuFocused()
        {
            m_eventListeners.ForEach<IOnMenuFocused>(x => x.OnMenuFocused());
        }

        private void DispatchOnMenuUnfocused()
        {
            m_eventListeners.ForEach<IOnMenuUnfocused>(x => x.OnMenuUnfocused());
        }

        private T[] GetMenuListeners<T>()
        {
            return GetComponentsInChildren<T>();
        }

        private IEnumerator ShowAnimationCoroutine(bool shouldPlayAnimation)
        {
            IsPlayingAnimation = true;

            m_menuManager.UpdateFocus();

            OnMenuShowStarted();

            if (shouldPlayAnimation)
            {
                yield return ShowAction(this, GetMenuOffScreenPosition(), GetMenuOnScreenPosition(), AnimationDuration);
            }
            else
            {
                rectTransform.anchoredPosition = GetMenuOnScreenPosition();
            }

            IsPlayingAnimation = false;

            OnMenuShowEnded();

            m_menuManager.UpdateFocus();
        }

        private IEnumerator HideAnimationCoroutine(bool shouldPlayAnimation)
        {
            IsPlayingAnimation = true;

            m_menuManager.UpdateFocus();

            OnMenuHideStarted();

            if (shouldPlayAnimation)
            {
                yield return HideAction(this, GetMenuOnScreenPosition(), GetMenuOffScreenPosition(), AnimationDuration);
            }
            else
            {
                rectTransform.anchoredPosition = GetMenuOffScreenPosition();
            }

            gameObject.SetActive(false);

            IsPlayingAnimation = false;

            OnMenuHideEnded();

            m_menuManager.UpdateFocus();
        }

        [HideInInspector] [SerializeField] bool m_isGoBackEnabled = true;
        [HideInInspector] [SerializeField] bool m_eventsEnabled = true;
        [HideInInspector] [SerializeField] bool m_shouldSwallowFocus = false;
        [HideInInspector] [SerializeField] GameObject m_firstSelected;
        [HideInInspector] [SerializeField] string m_layer;
        [HideInInspector] [SerializeField] AnimType m_showAnimType;
        [HideInInspector] [SerializeField] AnimType m_hideAnimType;
        [HideInInspector] [SerializeField] float m_animationDuration = 0.5f;

        MultitypeSet m_eventListeners = null;
        RectTransform m_rectTransform = null;
        GameObject m_gameObject = null;
        GameObject m_lastSelected = null;
        MenuAnimationDelegate m_showAction;
        MenuAnimationDelegate m_hideAction;
        bool m_inputEnabled = true;

        protected MenuManager m_menuManager;
        protected Selectable[] m_selectables;
        private Dictionary<Selectable, ColorBlock> mSelectableColors;
        private Dictionary<Selectable, bool> _selectablesInteractableAtStart;
    }
}