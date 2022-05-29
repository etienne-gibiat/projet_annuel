using System;
using GameTroopers.UI;
using PGSauce.Core.Extensions;
using PGSauce.Core.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PGSauce.UI
{
    public abstract class PGMenuManager<T> : MonoSingleton<T> where T : MonoSingletonBase
    {
        [SerializeField] private MenuManager menuManager;
        
        protected void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        protected void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        protected abstract bool ShouldLoadMenu(Scene scene);
        protected abstract string GroupName { get; }

        private MenuManager ArcadiaMenuManager => menuManager;

        protected abstract void ShowOnSceneLoadMenu();
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (!ArcadiaMenuManager.IsInitialized)
            {
                ArcadiaMenuManager.Initialize();
            }
            
            TryLoadMenuGroup(false);
            var shouldLoad = ShouldLoadMenu(scene);
            if (!shouldLoad)
            {
                return;
            }
            
            TryLoadMenuGroup(true);
            
            ShowOnSceneLoadMenu();
        }

        protected void TryLoadMenuGroup(bool load)
        {
            if (load)
            {
                if (!ArcadiaMenuManager.IsGroupLoaded(GroupName))
                {
                    ArcadiaMenuManager.LoadGroup(GroupName);
                }
            }
            else
            {
                ArcadiaMenuManager.HideAll(false);
            }
        }

        protected void GoBack()
        {
            ArcadiaMenuManager.GoBack();
        }

        public TMenu ShowMenu<TMenu>(Action<TMenu> setup = null) where TMenu : Menu
        {
            if (setup == null)
            {
                setup = menu => { };
            }
            var menuName = typeof(TMenu).ToString().RemoveNamespace();
            return ArcadiaMenuManager.ShowMenu<TMenu>(GroupName, setup);
        }

        public TMenu GetMenu<TMenu>() where TMenu : Menu
        {
            return ArcadiaMenuManager.GetMenu<TMenu>(GroupName);
        }

        public void HideMenu<TMenu>() where TMenu : Menu
        {
            ArcadiaMenuManager.HideMenu<TMenu>(GroupName);
        }

        /// <summary>
        /// Hide all the menus which layers are above layerName.
        /// </summary>
        /// <param name="layerName">The reference layer. Any layer above it will be hidden.</param>
        /// <param name="included">Should Menus with this layer also be hidden ?</param>
        public void HideLayersAboveThisOne(string layerName, bool included = false)
        {
            ArcadiaMenuManager.HideLayersAboveThisOne(layerName, included);
        }
        
        /// <summary>
        /// Hide all the menus which layers are above the layer of TMenu
        /// </summary>
        /// <param name="included">Should Menus with this layer also be hidden ?</param>
        /// <typeparam name="TMenu">The reference Menu type</typeparam>
        public void HideLayersAboveThisOne<TMenu>(bool included = false) where TMenu : Menu
        {
            var menu = GetMenu<TMenu>();
            ArcadiaMenuManager.HideLayersAboveThisOne(menu.Layer, included);
        }
    }
}