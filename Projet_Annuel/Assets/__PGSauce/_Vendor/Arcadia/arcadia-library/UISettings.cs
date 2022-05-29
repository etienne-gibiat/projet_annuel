using System;
using System.Collections.Generic;
using PGSauce.Core.Utilities;
using UnityEngine;

namespace GameTroopers.UI
{
    public class UISettings : SOSingleton<UISettings>
    {
        public static string SettingsPath = "Assets/__PGSauce/_Vendor/Arcadia/Resources/";
        public static string AssetName = "UISettings";

        public bool LoadAllOnStart
        {
            get { return m_loadAllOnStart; }
            set { m_loadAllOnStart = value; }
        }

        public List<string> LayerNames
        {
            get { return m_layerNames; }
        }

        public List<string> GroupsNames
        {
            get { return m_groupsNames; }
        }

        public List<MenuListWrapper> GroupsMenus
        {
            get { return m_groupsMenus; }
        }

        [SerializeField] private bool                     m_loadAllOnStart;
        [SerializeField] private List<string>             m_layerNames  = new List<string>();
        [SerializeField] private List<string>             m_groupsNames = new List<string>();
        [SerializeField] private List<MenuListWrapper>    m_groupsMenus = new List<MenuListWrapper>();

        [Serializable]
        public class MenuListWrapper
        {
            public List<Menu> menuList = new List<Menu>();
        }
    }
}
