using UnityEngine;
using UnityEditor;


using AmazingAssets.DynamicRadialMasks;

namespace AmazingAssets.DynamicRadialMasksEditor
{
    [CustomEditor(typeof(DRMController))]
    public class DRMControllerEditor : Editor
    {
        SerializedProperty materials;
        SerializedProperty drawInEditor;

        void OnEnable()
        {
            materials = serializedObject.FindProperty("materials");
            drawInEditor = serializedObject.FindProperty("drawInEditor");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            if (serializedObject.FindProperty("scope").enumValueIndex == (int)DRMController.MASK_SCOPE.Local)
            {
                materials.isExpanded = EditorGUILayout.Foldout(materials.isExpanded, "Materials", true);
                if (materials.isExpanded)
                {
                    using (new EditorGUIHelper.EditorGUIIndentLevel(1))
                    {
                        materials.arraySize = EditorGUILayout.IntField("Size", materials.arraySize);

                        for (int i = 0; i < materials.arraySize; ++i)
                        {
                            SerializedProperty transformProp = materials.GetArrayElementAtIndex(i);
                            EditorGUILayout.PropertyField(transformProp, new GUIContent("Element " + i));
                        }
                    }
                }
            }

            EditorGUILayout.PropertyField(drawInEditor);

            serializedObject.ApplyModifiedProperties();
        }
    }
}