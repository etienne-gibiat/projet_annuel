using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameTroopers.UI
{
    internal class MenuLoader
    {
        internal MenuLoader()
        {
            m_groups            = new List<Group>();
            //m_referencedMenus   = new Dictionary<Type, GameObject>();
            //m_loadedMenus       = new Dictionary<Type, Menu>();
            m_loadedGroups      = new List<string>();
            m_layers            = new List<Layer>();
            _referencedMenus = new Dictionary<string, Dictionary<Type, GameObject>>();
            _loadedMenus = new Dictionary<string, Dictionary<Type, Menu>>();
            _layerIndexes = new Dictionary<string, int>();
        }

        internal bool IsLoaded { get { return m_settings != null; } }

        internal void Load(UISettings settings, Canvas canvas, Action<Menu> onLoadMenu)
        {
            if (!settings)
            {
                throw new ArgumentNullException("Couldn't load settings.");
            }

            m_settings = settings;

            LoadLayers(canvas);
            LoadMenuGroups();

            if (m_settings.LoadAllOnStart)
            {
                LoadAllGroups(onLoadMenu);
            }
        }

        internal T GetLoadedMenu<T>(string groupName) where T : Menu
        {
            Menu menu = null;

            var menus = _loadedMenus[groupName];

            if (menus.TryGetValue(typeof(T), out menu))
            {
                return (T)menu;
            }
            else
            {
                throw new InvalidOperationException($"Menu {typeof(T)} is not loaded.");
            }
        }

        internal IEnumerable<Menu> GetLoadedMenus()
        {
            var menus = new List<Menu>();
            foreach (var loadedMenu in _loadedMenus.Values)
            {
                menus.AddRange(loadedMenu.Values);
            }
            return menus;
        }

        /// <summary>
        /// Returns the menus from the top of all layers, that are active on the scene.
        /// Includes menus that are animating in, but not the ones that are animating out.
        /// </summary>
        /// <returns>menus from the top of all layers</returns>
        internal IEnumerable<Menu> GetTopActiveMenus()
        {
            return m_layers
                .Select(layer => layer.openedMenus.LastOrDefault())
                .Where(menu => menu != null && menu.IsActive);
        }

        internal Menu GetTopActiveMenuInLayer(string layerName)
        {
            Layer layer = GetLayer(layerName);

            return layer.openedMenus.LastOrDefault(menu => menu.IsActive);
        }

        internal Menu GetNextActiveMenuInLayer(string layerName)
        {
            Layer layer = GetLayer(layerName);
            
            if (layer.openedMenus.Count < 2)
            {
                return null;
            }

            return layer.openedMenus[layer.openedMenus.Count - 2];
        }

        internal void SetTopActiveMenuInLayer(string layerName, Menu menu)
        {
            Layer layer = GetLayer(layerName);

            if (layer.openedMenus.Count > 0)
            {
                int menuIndex = layer.openedMenus.LastIndexOf(menu);

                bool foundCycleInHistory = menuIndex != -1;
                if (foundCycleInHistory)
                {
                    layer.openedMenus.RemoveRange(
                        menuIndex, layer.openedMenus.Count - menuIndex);
                }
            }

            layer.openedMenus.Add(menu);
        }

        internal void SetInactiveMenuInLayer(string layerName, Menu menu)
        {
            Layer layer = GetLayer(layerName);
            layer.openedMenus.Remove(menu);
        }

        internal void LoadGroup(string groupName, Action<Menu> onLoadMenu)
        {
            if (IsGroupLoaded(groupName))
            {
                throw new InvalidOperationException($"{groupName} group is already opened.");
            }

            Group group = m_groups.FirstOrDefault(x => x.name.Equals(groupName));

            if (group == null)
            {
                throw new ArgumentException($"{groupName} group doesn't exist.");
            }
            
            if (m_groups.Count(x => x.name.Equals(groupName)) != 1)
            {
                throw new ArgumentException(
                    $"You have more than a group called ({groupName}). Group names must be unique.");
            }

            foreach (Menu menuPrefab in group.menuPrefabs)
            {
                Menu instantiatedMenu = LoadMenu(menuPrefab.GetType(), groupName);

                onLoadMenu(instantiatedMenu);
            }

            m_loadedGroups.Add(groupName);
        }

        public bool IsGroupLoaded(string groupName)
        {
            return m_loadedGroups.Contains(groupName);
        }

        internal void UnloadGroup(string groupName)
        {
            if (!m_loadedGroups.Contains(groupName))
            {
                throw new InvalidOperationException($"{groupName} group is not opened.");
            }

            Group group = m_groups.FirstOrDefault(x => x.name.Equals(groupName));

            if (group == null)
            {
                throw new ArgumentException($"{groupName} group doesn't exist.");
            }

            foreach (var menuPrefab in group.menuPrefabs)
            {
                var menuType = menuPrefab.GetType();
                var layer = menuPrefab.Layer;
                var menuInstanceToRemove =  _loadedMenus[groupName][menuType];

                if (menuInstanceToRemove.IsActive)
                {
                    menuInstanceToRemove.Hide(false);
                }

                SetInactiveMenuInLayer(layer, menuInstanceToRemove);
                UnityEngine.Object.Destroy(menuInstanceToRemove.gameObject);

                _loadedMenus[groupName].Remove(menuType);
                //m_loadedMenus.Remove(menuType);
            }

            m_loadedGroups.Remove(groupName);
        }

        private void LoadAllGroups(Action<Menu> onLoadMenu)
        {
            foreach (Group group in m_groups)
            {
                LoadGroup(group.name, onLoadMenu);
            }
        }

        private Menu LoadMenu(Type type, string groupName)
        {
            if (_loadedMenus[groupName].ContainsKey(type))
            {
                throw new ArgumentException("Trying to instantiate a menu type that already exists.");
            }

            if (!_referencedMenus[groupName].TryGetValue(type, out var menuGameObject))
            {
                throw new ArgumentException($"\"{type.ToString()}\" menu doesn't exist.");
            }

            if (menuGameObject.activeSelf)
            {
                menuGameObject.SetActive(false);
                Debug.LogWarningFormat("Trying to instantiate an inactive prefab ({0}). Prefab should be disabled by default.", menuGameObject.name);
            }

            menuGameObject = UnityEngine.Object.Instantiate(menuGameObject);
            Menu menu = menuGameObject.GetComponent<Menu>();
            Layer layer = GetLayer(menu.Layer);
            menuGameObject.name = menu.GetType().ToString();
            menu.transform.SetParent(layer.transform, false);

           // m_loadedMenus.Add(menu.GetType(), menu);
            _loadedMenus[groupName].Add(menu.GetType(), menu);

            return menu;
        }

        private void LoadLayers(Canvas canvas)
        {
            for (int i = m_settings.LayerNames.Count - 1; i >= 0; i--)
            {
                string layerName = m_settings.LayerNames[i];

                if (string.IsNullOrEmpty(layerName))
                {
                    throw new ArgumentException("Layer name cannot be an empty string");
                }

                if (m_settings.LayerNames.Count(x => x.Equals(layerName)) != 1)
                {
                    throw new ArgumentException(
                        $"You have more than a layer called ({layerName}). Layer names must be unique.");
                }

                GameObject gameObject = new GameObject();
                gameObject.name = layerName;

                RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
                rectTransform.SetParent(canvas.transform);
                rectTransform.transform.SetAsFirstSibling();
                rectTransform.localPosition = Vector3.zero;
                rectTransform.localScale = Vector3.one;
                rectTransform.anchoredPosition = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;
                rectTransform.offsetMin = Vector2.zero;

                Layer layer = new Layer();
                layer.name = layerName;
                layer.transform = rectTransform;
                m_layers.Add(layer);
                _layerIndexes.Add(layerName, i);
            }
        }

        private void LoadMenuGroups()
        {
            //m_referencedMenus.Clear();
            foreach (var referencedMenu in _referencedMenus)
            {
                referencedMenu.Value.Clear();
            }
            
            _referencedMenus.Clear();
            _loadedMenus.Clear();

            for (var i = 0; i < m_settings.GroupsNames.Count; i++)
            {
                var groupName = m_settings.GroupsNames[i];
                var menuList = m_settings.GroupsMenus[i].menuList;

                if (string.IsNullOrEmpty(groupName))
                {
                    throw new ArgumentException("Group name cannot be an empty string");
                }

                Group group = new Group();
                group.name = groupName;
                _referencedMenus[groupName] = new Dictionary<Type, GameObject>();
                _loadedMenus[groupName] = new Dictionary<Type, Menu>();
                

                for (int j = 0; j < menuList.Count; j++)
                {
                    Type menuType = menuList[j].GetType();
                    if (_referencedMenus[groupName].ContainsKey(menuType))
                    {
                        throw new InvalidOperationException(
                            $"You have two menus of type {menuType}. Only one type of menu is allowed.");
                    }

                    group.menuPrefabs.Add(menuList[j]);
                    _referencedMenus[groupName].Add(menuType, menuList[j].gameObject);
                }

                m_groups.Add(group);
            }
        }
        
        public int GetLayerIndex(string layerName)
        {
            return _layerIndexes[layerName];
        }

        private Layer GetLayer(string layerName)
        {
            var targetLayer = m_layers.FirstOrDefault(layer => layer.name == layerName);
            if (targetLayer == null)
            {
                throw new ArgumentOutOfRangeException($"Layer \"{layerName}\" does not exist.");
            }

            return targetLayer;
        }

        UISettings m_settings;

        List<Group>                     m_groups;
        //Dictionary<Type, GameObject>    m_referencedMenus;
        //Dictionary<Type, Menu>          m_loadedMenus;
        List<string>                    m_loadedGroups;
        List<Layer>                     m_layers;
        private Dictionary<string, Dictionary<Type, Menu>> _loadedMenus;
        private Dictionary<string, Dictionary<Type, GameObject>> _referencedMenus;
        private Dictionary<string, int> _layerIndexes;

        
    }
}