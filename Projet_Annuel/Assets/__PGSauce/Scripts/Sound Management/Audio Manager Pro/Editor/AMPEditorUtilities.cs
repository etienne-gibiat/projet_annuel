using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>Collection of different utilities used by AMP editors.</summary>
namespace PGSauce.AudioManagement.External.Editor
{
    /// <summary>Editor field helper functions for AMP editors.</summary>
    public static class AMPEditorFields
    {
        /// <summary>Creates a MinMaxSlider using SerializedPropertys.</summary>
        /// <param name="Label">The label to display on the MinMaxSlider/</param>
        /// <param name="MinVal">The SerializedProperty for the minimum value or the left handle.</param>
        /// <param name="MaxVal">The SerializedProperty for the maximum value or the right handle.</param>
        /// <param name="MinLimit">The lower limit for the MinMaxSlider.</param>
        /// <param name="MinLimit">The upper limit for the MinMaxSlider.</param>
        /// <param name="Options">Any extra GUIOptions.</param>
        public static void MinMaxSlider(GUIContent Label, SerializedProperty MinVal, SerializedProperty MaxVal, float MinLimit, float MaxLimit, params GUILayoutOption[] Options)
        {
            //Gets float values from properties
            float Min = MinVal.floatValue;
            float Max = MaxVal.floatValue;

            //Creates MinMaxSlider
            EditorGUILayout.MinMaxSlider(Label, ref Min, ref Max, MinLimit, MaxLimit, Options);

            //Injects float values back into properties
            MinVal.floatValue = Min;
            MaxVal.floatValue = Max;
        }
        
        /// <summary>Creates a MinMaxSlider using SerializedPropertys.</summary>
        /// <param name="DrawRect">The rect to draw the MinMaxSlider in.</param>
        /// <param name="Label">The label to display on the MinMaxSlider/</param>
        /// <param name="MinVal">The SerializedProperty for the minimum value or the left handle.</param>
        /// <param name="MaxVal">The SerializedProperty for the maximum value or the right handle.</param>
        /// <param name="MinLimit">The lower limit for the MinMaxSlider.</param>
        /// <param name="MinLimit">The upper limit for the MinMaxSlider.</param>
        public static void MinMaxSlider(Rect DrawRect, GUIContent Label, SerializedProperty MinVal, SerializedProperty MaxVal, float MinLimit, float MaxLimit)
        {
            //Gets float values from properties
            float Min = MinVal.floatValue;
            float Max = MaxVal.floatValue;

            //Creates MinMaxSlider
#if UNITY_5_5_OR_NEWER
            EditorGUI.MinMaxSlider(DrawRect, Label, ref Min, ref Max, MinLimit, MaxLimit);
#else
            EditorGUI.MinMaxSlider(Label, DrawRect, ref Min, ref Max, MinLimit, MaxLimit);
#endif

            //Injects float values back into properties
            MinVal.floatValue = Min;
            MaxVal.floatValue = Max;
        }
    }
}
