using UnityEngine;
using UnityEditor;


using AmazingAssets.DynamicRadialMasks;

namespace AmazingAssets.DynamicRadialMasksEditor
{
    [CustomPropertyDrawer(typeof(DRMReadOnlyAttribute))]
    public class DRMReadOnlyAttributePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = Application.isPlaying ? false : true;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}