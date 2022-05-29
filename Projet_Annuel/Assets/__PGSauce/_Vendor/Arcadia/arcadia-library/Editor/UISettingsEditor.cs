using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace GameTroopers.UI
{
    [CustomEditor(typeof(UISettings))]
    public class UISettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            m_tempObject.Update();
            EditorGUILayout.Space();

            if (m_dirty)
            {
                EditorGUILayout.HelpBox("You have unsaved changes. Please, apply or revert them before closing this window", MessageType.Warning);
            }

            EditorGUI.BeginChangeCheck();
            string loadAllTooltip = "If checked, all the groups of menus will be loaded into memory when the game is initialized";
            m_loadAllOnStartProp.boolValue = EditorGUILayout.Toggle(new GUIContent("Load all groups on Start", loadAllTooltip), 
                                                                    m_loadAllOnStartProp.boolValue);
            EditorGUILayout.Space();

            DrawLayerConfiguration();
            EditorGUILayout.Space();

            DrawGroupsConfiguration();
            if (EditorGUI.EndChangeCheck())
            {
                m_dirty = true;
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            GUI.enabled = m_dirty;

            if (GUILayout.Button(new GUIContent("Apply"), GUILayout.MaxWidth(50)))
            {
                ApplyChanges();
            }

            if (GUILayout.Button(new GUIContent("Revert"), GUILayout.MaxWidth(50)))
            {
                RevertChanges();
            }
            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            m_tempObject.ApplyModifiedProperties();
        }

        private void DrawLayerConfiguration()
        {
            EditorGUILayout.LabelField(new GUIContent("Layers"), EditorStyles.boldLabel);

            EditorGUILayout.BeginVertical();

            if (m_layersProp.arraySize == 0)
            {
                EditorGUILayout.HelpBox("You need to create at least 1 layer", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.HelpBox("Layers order goes from back to front. The last layer on the list " +
                                            "will be rendered in front of all the previous ones.", MessageType.Info);
            }

            m_layersList.DoLayoutList();

            EditorGUILayout.EndVertical();
        }

        private void DrawGroupsConfiguration()
        {
            EditorGUILayout.LabelField(new GUIContent("Groups"), EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();

            if (m_groupsNamesProp.arraySize == 0)
            {
                EditorGUILayout.HelpBox("You need to create at least 1 group", MessageType.Warning);
            }

            for (int index = 0; index < m_groupsNamesProp.arraySize; index++)
            {
                var groupName   = m_groupsNamesProp.GetArrayElementAtIndex(index);
                var groupMenus  = m_groupsMenusProp.GetArrayElementAtIndex(index);

                DrawGroup(index, groupName, groupMenus.FindPropertyRelative("menuList"));

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                EditorGUILayout.Space();
            }

            if (GUILayout.Button("+"))
            {
                AddGroup();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawGroup(int groupIndex, SerializedProperty groupName, SerializedProperty groupMenus)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUI.BeginChangeCheck();
            var name = EditorGUILayout.TextField(new GUIContent("Group Name"), groupName.stringValue);
            if (EditorGUI.EndChangeCheck())
            {
                groupName.stringValue = name;
            }

            ReorderableList menuList = null;
            if (groupIndex < m_menusInGroupsLists.Count)
            {
                menuList = m_menusInGroupsLists[groupIndex];
            }
            else
            {
                menuList = CreateMenusReorderableList(groupIndex, groupMenus);
                m_menusInGroupsLists.Add(menuList);
            }

            menuList.DoLayoutList();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove", EditorStyles.miniButtonLeft))
            {
                RemoveGroup(groupIndex);
            }
            if (GUILayout.Button("▲", EditorStyles.miniButtonMid))
            {
                EditorGUI.FocusTextInControl(null);
                MoveGroup(groupIndex, groupIndex - 1);
            }
            if (GUILayout.Button("▼", EditorStyles.miniButtonRight))
            {
                EditorGUI.FocusTextInControl(null);
                MoveGroup(groupIndex, groupIndex + 1);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        private void CreateLayerReorderableList()
        {
            m_layersList = new ReorderableList(m_tempObject,
                                               m_layersProp,
                                               draggable: true,
                                               displayHeader: false,
                                               displayAddButton: true,
                                               displayRemoveButton: true);

            m_layersList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var layerNameElement = m_layersList.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), layerNameElement, GUIContent.none);
            };

            m_layersList.onAddCallback = (ReorderableList layerList) =>
            {
                var index = layerList.serializedProperty.arraySize;
                layerList.serializedProperty.InsertArrayElementAtIndex(index);
                var layerNameElement = layerList.serializedProperty.GetArrayElementAtIndex(index);
                layerNameElement.stringValue = "DefaultLayer";
            };

            m_layersList.onReorderCallback = (ReorderableList list) =>
            {
                m_dirty = true;
            };
        }

        private ReorderableList CreateMenusReorderableList(int groupIndex, SerializedProperty menuListProp)
        {
            ReorderableList menuList = new ReorderableList(m_tempObject,
                                                           menuListProp,
                                                           draggable: true,
                                                           displayHeader: true,
                                                           displayAddButton: true,
                                                           displayRemoveButton: true);

            menuList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Menu List");
            };

            menuList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var menuPrefabElement = menuList.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), menuPrefabElement, GUIContent.none);
            };

            menuList.onReorderCallback = (ReorderableList list) =>
            {
                m_dirty = true;
            };

            return menuList;
        }

        private void OnEnable()
        {
            m_tempObject = new SerializedObject(Instantiate(target));

            m_loadAllOnStartProp    = m_tempObject.FindProperty("m_loadAllOnStart");
            m_layersProp            = m_tempObject.FindProperty("m_layerNames");
            m_groupsNamesProp       = m_tempObject.FindProperty("m_groupsNames");
            m_groupsMenusProp       = m_tempObject.FindProperty("m_groupsMenus");

            CreateLayerReorderableList();
        }

        private void OnDisable()
        {
            if (m_dirty)
            {
                bool shouldApply = EditorUtility.DisplayDialog("Unsaved changes",
                                                                "You have unsaved changes in the UI library settings. Do you want to apply or revert them?",
                                                                "Apply",
                                                                "Revert");
                if (shouldApply)
                {
                    ApplyChanges();
                }
                else
                {
                    RevertChanges();
                }
            }
        }

        private void ApplyChanges()
        {
            GUI.FocusControl(null);

            List<string> errorMessages = new List<string>();
            if (AreSettingsValid(outputErrorMessages: errorMessages))
            {
                CopyProperties(m_tempObject, serializedObject);
                m_dirty = false;
            }
            else
            {
                string errorList = string.Join("\n", errorMessages.ToArray());
                EditorUtility.DisplayDialog("Could not apply changes",
                                            "Changes in UI settings could not be applied.The following errors were found:\n\n" + errorList,
                                            "Close");

                Debug.LogError("Changes in UI settings could not be applied. The following errors were found:\n" + errorList);
            }
            
        }

        private void RevertChanges()
        {
            GUI.FocusControl(null);
            CopyProperties(serializedObject, m_tempObject);
            m_dirty = false;
        }

        private void CopyProperties(SerializedObject origin, SerializedObject destiny)
        {
            destiny.Update();
            destiny.CopyFromSerializedProperty(origin.FindProperty("m_loadAllOnStart"));
            destiny.CopyFromSerializedProperty(origin.FindProperty("m_layerNames"));
            destiny.CopyFromSerializedProperty(origin.FindProperty("m_groupsNames"));
            destiny.CopyFromSerializedProperty(origin.FindProperty("m_groupsMenus"));
            destiny.ApplyModifiedProperties();
        }

        private bool AreSettingsValid(List<string> outputErrorMessages)
        {
            bool areLayersValid = AreLayerNamesValid(outputErrorMessages);
            bool areGroupsValid = AreMenuGroupsValid(outputErrorMessages);

            return areLayersValid && areGroupsValid;
        }

        private bool AreLayerNamesValid(List<string> errorMessages)
        {
            bool valid = true;

            HashSet<string> uniqueNames = new HashSet<string>();
            for (int i = 0; i < m_layersProp.arraySize; i++)
            {
                string layerName = m_layersProp.GetArrayElementAtIndex(i).stringValue;
                if (string.IsNullOrEmpty(layerName))
                {
                    valid = false;
                    errorMessages.Add("[Layers] The name of a layer cannot be an empty string.");
                }

                if (!uniqueNames.Add(layerName))
                {
                    valid = false;
                    errorMessages.Add("[Layers] Layer name \"" + layerName + "\" is duplicated. Layers must be unique.");
                }
            }

            return valid;
        }

        private bool AreMenuGroupsValid(List<string> errorMessages)
        {
            bool valid = true;

            HashSet<string> uniqueNames = new HashSet<string>();
            for (int i = 0; i < m_groupsNamesProp.arraySize; i++)
            {
                string  groupName = m_groupsNamesProp.GetArrayElementAtIndex(i).stringValue;
                var     groupList = m_groupsMenusProp.GetArrayElementAtIndex(i).FindPropertyRelative("menuList");

                if (string.IsNullOrEmpty(groupName))
                {
                    valid = false;
                    errorMessages.Add("[Groups] The name of a group cannot be an empty string.");
                }

                if (!uniqueNames.Add(groupName))
                {
                    valid = false;
                    errorMessages.Add("[Groups] Group name \"" + groupName + "\" is duplicated. Layers must be unique.");
                }

                for (int j = 0; j < groupList.arraySize; j++)
                {
                    if (groupList.GetArrayElementAtIndex(j).objectReferenceValue == null)
                    {
                        valid = false;
                        errorMessages.Add("[Groups] Menu list of \"" + groupName + "\" group has empty values.");
                    }
                }
            }

            return valid;
        }    

        private void AddGroup()
        {
            m_groupsNamesProp.InsertArrayElementAtIndex(m_groupsNamesProp.arraySize);
            m_groupsMenusProp.InsertArrayElementAtIndex(m_groupsMenusProp.arraySize);

            var groupNameProp = m_groupsNamesProp.GetArrayElementAtIndex(m_groupsNamesProp.arraySize - 1);
            var groupMenusProp = m_groupsMenusProp.GetArrayElementAtIndex(m_groupsMenusProp.arraySize - 1);

            groupNameProp.stringValue = "DefaultName";
            groupMenusProp.FindPropertyRelative("menuList").ClearArray();
        }

        private void RemoveGroup(int index)
        {
            m_groupsNamesProp.DeleteArrayElementAtIndex(index);
            m_groupsMenusProp.DeleteArrayElementAtIndex(index);

            m_menusInGroupsLists.RemoveAt(m_menusInGroupsLists.Count - 1);
        }

        private void MoveGroup(int srcIndex, int dstIndex)
        {
            m_groupsNamesProp.MoveArrayElement(srcIndex, dstIndex);
            m_groupsMenusProp.MoveArrayElement(srcIndex, dstIndex);
        }

        bool m_dirty = false;

        ReorderableList m_layersList;
        List<ReorderableList> m_menusInGroupsLists = new List<ReorderableList>();

        SerializedObject m_tempObject;
        SerializedProperty m_layersProp;
        SerializedProperty m_groupsNamesProp;
        SerializedProperty m_groupsMenusProp;
        SerializedProperty m_loadAllOnStartProp;
    }
}
