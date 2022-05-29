using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameTroopers.UI
{
    [CustomEditor(typeof(MenuManager))]
    public class MenuManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Open Settings", GUILayout.Width(200), GUILayout.Height(30)))
            {
                LoadSettings();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        private void LoadSettings()
        {
            string fullAssetPath = UISettings.SettingsPath + UISettings.AssetName + ".asset";
            UISettings settings = AssetDatabase.LoadAssetAtPath<UISettings>(fullAssetPath);
            if (settings == null)
            {
                settings = CreateInstance<UISettings>();
                if (!Directory.Exists(UISettings.SettingsPath))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }
                AssetDatabase.CreateAsset(settings, fullAssetPath);
            }

            Selection.activeObject = settings;
        }
        
        SerializedProperty m_canvasProp;
        SerializedProperty m_eventSystemProp;
    }
}