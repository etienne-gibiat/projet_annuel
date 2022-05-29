using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace GameTroopers.UI
{
    [CustomEditor(typeof(Menu), editorForChildClasses:true)]
    public class MenuEditor : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            if (m_settings == null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("Couldn't locate settings file.\nYou can generate it by pressing \"Open settings\" button in MenuManager component", MessageType.Warning);
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
            else
            { 
                serializedObject.Update();

                if (m_settings.LayerNames.Count == 0)
                {
                    EditorGUILayout.HelpBox("You need to define at least 1 layer in the settings file.", MessageType.Error);
                    GUI.enabled = false;
                }

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                EditorGUILayout.LabelField(new GUIContent("Base Menu Settings"), EditorStyles.boldLabel);
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.LabelField(new GUIContent("Properties"), EditorStyles.boldLabel);
                EditorGUILayout.Space();

                m_isGoBackEnabledProp.boolValue = 
                    EditorGUILayout.Toggle("Go Back Enabled", m_isGoBackEnabledProp.boolValue);

                EditorGUI.BeginDisabledGroup(disabled: Application.isPlaying);

                string eventsEnabledTooltip = "If selected, this menu will send events to " +
                                               "its children during its lifecycle";
                m_eventsEnabledProp.boolValue = 
                    EditorGUILayout.Toggle(new GUIContent("Events Enabled", eventsEnabledTooltip),
                                                                             m_eventsEnabledProp.boolValue);

                EditorGUI.EndDisabledGroup();

                m_swallowFocusProp.boolValue = 
                    EditorGUILayout.Toggle("Swallow Focus", m_swallowFocusProp.boolValue);
                
                EditorGUILayout.PropertyField(m_firstSelectedProp, new GUIContent("First selected"));

                EditorGUI.BeginDisabledGroup(disabled: Application.isPlaying);

                DrawLayerSelection();

                EditorGUILayout.Space();
                EditorGUILayout.LabelField(new GUIContent("Animations"), EditorStyles.boldLabel);
                EditorGUILayout.Space();

                string showAnimTooltip = "Set the type of animation that will be used to show this menu. " +
                                            "Custom animations require to manually add them by code.";
                m_showAnimProp.enumValueIndex = Convert.ToInt32(EditorGUILayout.EnumPopup(
                                                                    new GUIContent("Show Animation", showAnimTooltip),
                                                                    (Menu.AnimType)m_showAnimProp.enumValueIndex));

                string hideAnimTooltip = "Set the type of animation that will be used to hide this menu. " +
                                            "Custom animations require to manually add them by code.";
                m_hideAnimProp.enumValueIndex = Convert.ToInt32(EditorGUILayout.EnumPopup(
                                                                    new GUIContent("Hide Animation", hideAnimTooltip),
                                                                    (Menu.AnimType)m_hideAnimProp.enumValueIndex));

                EditorGUI.EndDisabledGroup();

                string animDurationTooltip = "Set the duration in seconds for both show and hide animations";

                m_animDurationProp.floatValue = EditorGUILayout.FloatField(
                                                                    new GUIContent("Animation Duration", animDurationTooltip),
                                                                    m_animDurationProp.floatValue);

                serializedObject.ApplyModifiedProperties();

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.LabelField(new GUIContent(target.name + " Settings"), 
                                                            EditorStyles.boldLabel);
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                DrawDefaultInspector();

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                GUI.enabled = true;
            }
        }

        private void DrawLayerSelection()
        {
            if (m_settings.LayerNames.Count > 0)
            {
                var layerList = m_settings.LayerNames;
                int currentValueIndex = layerList.IndexOf(m_layerProp.stringValue);
                if (currentValueIndex == -1)
                {
                    currentValueIndex = 0;
                }

                int selectedIndex = EditorGUILayout.Popup("Layer", currentValueIndex, layerList.ToArray());
                m_layerProp.stringValue = layerList[selectedIndex];
            }
            else
            {
                EditorGUILayout.Popup("Layer", 0, new string[] {"No layers defined"});
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            m_isGoBackEnabledProp   = serializedObject.FindProperty("m_isGoBackEnabled");
            m_eventsEnabledProp     = serializedObject.FindProperty("m_eventsEnabled");
            m_swallowFocusProp      = serializedObject.FindProperty("m_shouldSwallowFocus");
            m_firstSelectedProp     = serializedObject.FindProperty("m_firstSelected");
            m_layerProp             = serializedObject.FindProperty("m_layer");
            m_showAnimProp          = serializedObject.FindProperty("m_showAnimType");
            m_hideAnimProp          = serializedObject.FindProperty("m_hideAnimType");
            m_animDurationProp      = serializedObject.FindProperty("m_animationDuration");

            var fullSettingsPath = UISettings.SettingsPath + UISettings.AssetName + ".asset";
            m_settings = AssetDatabase.LoadAssetAtPath<UISettings>(fullSettingsPath);
        }

        UISettings m_settings;

        SerializedProperty m_isGoBackEnabledProp;
        SerializedProperty m_eventsEnabledProp;
        SerializedProperty m_swallowFocusProp;
        SerializedProperty m_firstSelectedProp;
        SerializedProperty m_layerProp;
        SerializedProperty m_showAnimProp;
        SerializedProperty m_hideAnimProp;
        SerializedProperty m_animDurationProp;
    }
}
