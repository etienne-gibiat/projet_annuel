using UnityEngine;
using UnityEditor;
#if AMPLIFY_SHADER_EDITOR
using AmplifyShaderEditor;
#endif


namespace Onager.Vertext
{
    public enum DataNames
    {
        Color = 0,
        Normal = 1,
        Tangent = 2,
        UV0 = 3,
        UV1 = 4,
        UV2 = 5,
        UV3 = 6,
        UV4 = 7,
        UV5 = 8,
        UV6 = 9,
        UV7 = 10,
        ID = 11,
        WorldPos = 12,
        LocalPos = 13,
        Custom = 14,
    }

    public enum ChannelNames
    {
        XYZ = 0,
        X = 1,
        Y = 2,
        Z = 3,
        W = 4,
    }

    public class VertextShaderEditor : ShaderGUI
    {
        public static readonly Color HeaderColor = new Color32(246, 192, 111, 255);

        private MaterialProperty displayedData;
        private MaterialProperty channel;
        private MaterialProperty bias;
        private MaterialProperty absValue;
        private MaterialProperty digitAtlas;
        private MaterialProperty showDigits;
        private MaterialProperty digitColor;
        private MaterialProperty separation;
        private MaterialProperty scale;
        private MaterialProperty precision;
        private MaterialProperty do_round;
        private MaterialProperty wireframe;
        private MaterialProperty wireframeColor;
        private MaterialProperty wireframeSmooth;
        private MaterialProperty wireframeThickness;
        private const int PropertyCount = 15;

        private void FindProperties(MaterialProperty[] props)
        {
            displayedData = FindProperty("DisplayedData", props);
            channel = FindProperty("Channel", props);
            bias = FindProperty("Bias", props);
            absValue = FindProperty("ABSVAL", props);
            digitAtlas = FindProperty("Tex", props);
            showDigits = FindProperty("DIGITS", props);
            digitColor = FindProperty("DigitColor", props);
            separation = FindProperty("Separation", props);
            scale = FindProperty("Scale", props);
            precision = FindProperty("Precision", props);
            do_round = FindProperty("DO_ROUND", props);
            wireframe = FindProperty("WIREFRAME", props);
            wireframeColor = FindProperty("WireframeColor", props);
            wireframeSmooth = FindProperty("WireframeSmoothing", props);
            wireframeThickness = FindProperty("WireframeThickness", props);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            if (!materialEditor.isVisible) return;

            FindProperties(properties);

            { // Data
                EditorGUILayout.Space(16);
                EditorGUILayout.LabelField("Data", EditorStyles.boldLabel);
                var rect = EditorGUILayout.GetControlRect(false, GUILayout.Height(1));
                EditorGUI.DrawRect(rect, HeaderColor);

                materialEditor.ShaderProperty(displayedData, displayedData.displayName);
                materialEditor.ShaderProperty(channel, channel.displayName);
                materialEditor.ShaderProperty(bias, bias.displayName);
                materialEditor.ShaderProperty(absValue, absValue.displayName);
                materialEditor.ShaderProperty(do_round, do_round.displayName);
            }

            if (DrawHeader(materialEditor, showDigits, "Digits"))
            {
                materialEditor.ShaderProperty(digitColor, digitColor.displayName);
                materialEditor.ShaderProperty(separation, separation.displayName);
                materialEditor.ShaderProperty(scale, scale.displayName);
                materialEditor.ShaderProperty(precision, precision.displayName);
            }

            if (DrawHeader(materialEditor, wireframe, "Wireframe"))
            {
                materialEditor.ShaderProperty(wireframeColor, "Color");
                materialEditor.ShaderProperty(wireframeSmooth, "Smoothness");
                materialEditor.ShaderProperty(wireframeThickness, "Thickness");
            }

#if AMPLIFY_SHADER_EDITOR
            if (properties.Length > PropertyCount)
            {
                EditorGUILayout.Space(16);
                EditorGUILayout.LabelField("Amplify", EditorStyles.boldLabel);
                var rect = EditorGUILayout.GetControlRect(false, GUILayout.Height(1));
                EditorGUI.DrawRect(rect, HeaderColor);

                if (GUILayout.Button("Open in Shader Editor"))
                {
#if UNITY_2018_3_OR_NEWER
                    ASEPackageManagerHelper.SetupLateMaterial(materialEditor.target as Material);

#else
					AmplifyShaderEditorWindow.LoadMaterialToASE( materialEditor.target as Material );
#endif
                }

                for (int i = PropertyCount; i < properties.Length; i++)
                {
                    materialEditor.ShaderProperty(properties[i], properties[i].displayName);
                }
            }
#endif

            GUILayout.Space(16);
            materialEditor.RenderQueueField();

            // make sure digit atlas is set.
            if (digitAtlas.textureValue == null)
            {
                digitAtlas.textureValue = GetDigitAtlas(materialEditor);
            }
        }

        private const string atlasGUID = "f2488f903c275a24fa1b752d39924cfd";
        private Texture2D GetDigitAtlas(MaterialEditor materialEditor)
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(atlasGUID));
        }

        private bool DrawHeader(MaterialEditor materialEditor, MaterialProperty toggleProp, string label)
        {
            GUILayout.Space(16);

            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField(label, EditorStyles.boldLabel, GUILayout.Width(EditorGUIUtility.currentViewWidth - 48));
                materialEditor.ShaderProperty(toggleProp, string.Empty);
            }

            var rect = EditorGUILayout.GetControlRect(false, GUILayout.Height(1));
            EditorGUI.DrawRect(rect, HeaderColor);

            return toggleProp.floatValue > 0 || toggleProp.hasMixedValue;
        }
    }
}