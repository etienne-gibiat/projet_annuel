using UnityEngine;
using UnityEditor;


using AmazingAssets.DynamicRadialMasks;

namespace AmazingAssets.DynamicRadialMasksEditor
{
    [CustomPropertyDrawer(typeof(DRMAnimatableObject))]
    public class DRMAnimatableObjectDrawer : PropertyDrawer
    {
        EditorContext.STATE state = EditorContext.STATE.None;

        static float height = 0;

#if UNITY_2019_3_OR_NEWER
        static float propertyHeight = 20;
#else
            static float propertyHeight = 16;
#endif

        static GUIStyle miniTextBox;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CatchEditorContext(property);

            height = 18;

            position.y += 5;
            position.height = 18;
            EditorGUI.BeginProperty(position, label, property);



            GUIStyle style = (GUIStyle)"ProgressBarBar";
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleLeft;

            if (GUI.Button(new Rect(position.x, position.y, position.width - 18, position.height), (property.isExpanded ? "▼ " : "► ") + label.text, style))
            {
                property.isExpanded = !property.isExpanded;
            }

            style.alignment = TextAnchor.MiddleCenter;
            style.fontStyle = FontStyle.Normal;
            if (GUI.Button(new Rect(position.xMax - 20, position.y, 20, position.height), "≡", style))
            {
                ShowHeaderContextMenu(new Rect(position.xMax - 16, position.y, 16, position.height));
            }



            if (property.isExpanded)
            {
                using (new EditorGUIHelper.EditorGUIIndentLevel(1))
                {
                    height += DrawLifeProperty(ref position, property);



                    height += DrawProperties(ref position, property.FindPropertyRelative("radius"), new GUIContent("Radius"));

                    height += DrawProperties(ref position, property.FindPropertyRelative("intensity"), new GUIContent("Intensity"));

                    height += DrawProperties(ref position, property.FindPropertyRelative("noise"), new GUIContent("Noise"));

                    height += DrawProperties(ref position, property.FindPropertyRelative("edgeSize"), new GUIContent("Edge Size"));

                    height += DrawProperties(ref position, property.FindPropertyRelative("ringCount"), new GUIContent("Ring Count"));

                    height += DrawProperties(ref position, property.FindPropertyRelative("phaseSpeed"), new GUIContent("Phase Speed"));

                    height += DrawProperties(ref position, property.FindPropertyRelative("frequency"), new GUIContent("Frequency"));

                    height += DrawProperties(ref position, property.FindPropertyRelative("smooth"), new GUIContent("Smooth"));
                }
            }

            height += 12;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return height < 18 ? 18 : height;
        }

        float DrawLifeProperty(ref Rect position, SerializedProperty property)
        {
            position.y += 21;
            position.height = 16;


            // Draw label     
            DrawMinMax(property, "lifeLength", position, "Life Length", false, true);


            return 18;
        }

        float DrawProperties(ref Rect position, SerializedProperty property, GUIContent label)
        {
            float height = 24;


            position.y += 24;
            position.height = 16;


            // Draw label      
            EditorGUI.PropertyField(position, property.FindPropertyRelative("evolutionType"), label);


            using (new EditorGUIHelper.EditorGUIIndentLevel(1))
            {
                switch ((DRMAnimatableProperty.EVOLUTION_TYPE)property.FindPropertyRelative("evolutionType").enumValueIndex)
                {
                    case DRMAnimatableProperty.EVOLUTION_TYPE.Constant:
                        {
                            position.y += propertyHeight;
                            DrawValue(property, "startValue", position, "Value", true, true);

                            height += propertyHeight;
                        }
                        break;

                    case DRMAnimatableProperty.EVOLUTION_TYPE.ConstantRange:
                        {
                            position.y += propertyHeight;
                            DrawMinMax(property, "startValue", position, "Value", true, true);

                            height += propertyHeight;
                        }
                        break;

                    case DRMAnimatableProperty.EVOLUTION_TYPE.Lerp:
                        {
                            bool checkForZero = property.FindPropertyRelative("startValue").vector2Value.x == property.FindPropertyRelative("endValue").vector2Value.x ? true : false;


                            position.y += propertyHeight;
                            DrawValue(property, "startValue", position, "Start", true, checkForZero);

                            position.y += propertyHeight;
#if UNITY_2019_3_OR_NEWER
                            position.y -= 2;
#endif
                            DrawValue(property, "endValue", position, "End", true, checkForZero);


                            height += propertyHeight * 2;
                        }
                        break;

                    case DRMAnimatableProperty.EVOLUTION_TYPE.LerpRange:
                        {
                            bool checkForZero = property.FindPropertyRelative("startValue").vector2Value == property.FindPropertyRelative("endValue").vector2Value ? true : false;


                            position.y += propertyHeight;
                            DrawMinMax(property, "startValue", position, "Start", true, checkForZero);

                            position.y += propertyHeight;
#if UNITY_2019_3_OR_NEWER
                            position.y -= 2;
#endif
                            DrawMinMax(property, "endValue", position, "End", true, checkForZero);


                            height += propertyHeight * 2;
                        }
                        break;

                    case DRMAnimatableProperty.EVOLUTION_TYPE.Curve:
                        {
                            position.y += propertyHeight;
                            EditorGUI.PropertyField(position, property.FindPropertyRelative("curve"), new GUIContent(" "));
                            EditorGUI.LabelField(position, "Value", EditorStyles.miniLabel);


                            height += propertyHeight;
                        }
                        break;
                }
            }

            return height;
        }


        void DrawValue(SerializedProperty property, string propName, Rect position, string label, bool miniLabel, bool checkForZero)
        {
            EditorGUI.LabelField(position, label, miniLabel ? EditorStyles.miniLabel : EditorStyles.label);


            Vector2 value = property.FindPropertyRelative(propName).vector2Value;

            Color errorColor = checkForZero ? (value.x == 0 ? Color.red : Color.white) : Color.white;



            if (miniTextBox == null)
            {
                miniTextBox = new GUIStyle(EditorStyles.helpBox);

                miniTextBox.alignment = TextAnchor.MiddleLeft;
            }


            using (new EditorGUIHelper.GUIBackgroundColor(errorColor))
            {
                EditorGUI.BeginChangeCheck();
                value.x = EditorGUI.FloatField(position, " ", value.x, miniTextBox);

                if (EditorGUI.EndChangeCheck())
                {
                    property.FindPropertyRelative(propName).vector2Value = value;
                }
            }
        }

        void DrawMinMax(SerializedProperty property, string propName, Rect position, string label, bool miniLabel, bool checkForZero)
        {
            EditorGUI.LabelField(position, label, miniLabel ? EditorStyles.miniLabel : EditorStyles.label);


            Rect dataRect = position;
            dataRect.xMin += UnityEditor.EditorGUIUtility.labelWidth;
            dataRect.width *= 0.5f;


            Vector2 value = property.FindPropertyRelative(propName).vector2Value;

            Color errorColor = checkForZero ? ((value.x == value.y && value.x == 0) ? Color.red : Color.white) : Color.white;


            int indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;


            if (miniTextBox == null)
            {
                miniTextBox = new GUIStyle(EditorStyles.helpBox);

                miniTextBox.alignment = TextAnchor.MiddleLeft;
            }


            //Min
            Rect minRect = dataRect;
            using (new EditorGUIHelper.EditorGUIUtilityLabelWidth(EditorStyles.label.CalcSize(new GUIContent("Mim")).x))     //yes, 'Mim' and not 'Min'. 'm' gives just a little bit more space than 'n'
            {
                EditorGUI.LabelField(minRect, "Min", EditorStyles.miniLabel);
                using (new EditorGUIHelper.GUIBackgroundColor(errorColor))
                {
                    EditorGUI.BeginChangeCheck();
                    value.x = EditorGUI.FloatField(minRect, " ", value.x, miniTextBox);

                    if (EditorGUI.EndChangeCheck())
                        property.FindPropertyRelative(propName).vector2Value = value;
                }
            }


            //Max
            Rect maxRect = minRect;
            maxRect.xMin = maxRect.xMax;
            maxRect.width = dataRect.width;

            using (new EditorGUIHelper.EditorGUIUtilityLabelWidth(EditorStyles.label.CalcSize(new GUIContent(" Max")).x))
            {
                EditorGUI.LabelField(maxRect, " Max", EditorStyles.miniLabel);
                using (new EditorGUIHelper.GUIBackgroundColor(errorColor))
                {
                    EditorGUI.BeginChangeCheck();
                    value.y = EditorGUI.FloatField(maxRect, " ", value.y, miniTextBox);

                    if (EditorGUI.EndChangeCheck())
                        property.FindPropertyRelative(propName).vector2Value = value;

                }
            }



            EditorGUI.indentLevel = indentLevel;
        }

        void ShowHeaderContextMenu(Rect position)
        {
            var menu = new GenericMenu();

            menu.AddItem(new GUIContent("Copy"), false, OnContextMenuClick, EditorContext.STATE.Copy);
            if (EditorContext.drmObject == null)
                menu.AddDisabledItem(new GUIContent("Paste"));
            else
                menu.AddItem(new GUIContent("Paste"), false, OnContextMenuClick, EditorContext.STATE.Paste);
            menu.AddItem(new GUIContent("Reset"), false, OnContextMenuClick, EditorContext.STATE.Reset);

            menu.DropDown(position);
        }

        void OnContextMenuClick(object obj)
        {
            state = (EditorContext.STATE)obj;
        }

        void CatchEditorContext(SerializedProperty property)
        {
            switch (state)
            {
                case EditorContext.STATE.None:
                    break;

                case EditorContext.STATE.Copy:
                    EditorContext.Copy(property);
                    break;

                case EditorContext.STATE.Paste:
                    EditorContext.Paste(property);
                    break;

                case EditorContext.STATE.Reset:
                    EditorContext.Reset(property);
                    break;
            }

            //Reset
            state = EditorContext.STATE.None;
        }


        public static class EditorContext
        {
            public enum STATE { None, Copy, Paste, Reset };


            public static DRMAnimatableObject drmObject;

            public static void Copy(SerializedProperty property)
            {
                drmObject = new DRMAnimatableObject();


                drmObject.lifeLength = property.FindPropertyRelative("lifeLength").vector2Value;

                drmObject.radius = MemberCopy(property.FindPropertyRelative("radius"));
                drmObject.intensity = MemberCopy(property.FindPropertyRelative("intensity"));
                drmObject.noise = MemberCopy(property.FindPropertyRelative("noise"));
                drmObject.edgeSize = MemberCopy(property.FindPropertyRelative("edgeSize"));
                drmObject.ringCount = MemberCopy(property.FindPropertyRelative("ringCount"));
                drmObject.phaseSpeed = MemberCopy(property.FindPropertyRelative("phaseSpeed"));
                drmObject.frequency = MemberCopy(property.FindPropertyRelative("frequency"));
                drmObject.smooth = MemberCopy(property.FindPropertyRelative("smooth"));
            }

            public static void Paste(SerializedProperty property)
            {
                if (drmObject == null || property == null)
                    return;

                property.FindPropertyRelative("lifeLength").vector2Value = drmObject.lifeLength;

                MemberCopy(ref property, drmObject.radius, "radius");
                MemberCopy(ref property, drmObject.intensity, "intensity");
                MemberCopy(ref property, drmObject.noise, "noise");
                MemberCopy(ref property, drmObject.edgeSize, "edgeSize");
                MemberCopy(ref property, drmObject.ringCount, "ringCount");
                MemberCopy(ref property, drmObject.phaseSpeed, "phaseSpeed");
                MemberCopy(ref property, drmObject.frequency, "frequency");
                MemberCopy(ref property, drmObject.smooth, "smooth");
            }

            public static void Reset(SerializedProperty property)
            {
                if (property == null)
                    return;

                DRMAnimatableObject resetObject = new DRMAnimatableObject();

                property.FindPropertyRelative("lifeLength").vector2Value = resetObject.lifeLength;

                MemberCopy(ref property, resetObject.radius, "radius");
                MemberCopy(ref property, resetObject.intensity, "intensity");
                MemberCopy(ref property, resetObject.noise, "noise");
                MemberCopy(ref property, resetObject.edgeSize, "edgeSize");
                MemberCopy(ref property, resetObject.ringCount, "ringCount");
                MemberCopy(ref property, resetObject.phaseSpeed, "phaseSpeed");
                MemberCopy(ref property, resetObject.frequency, "frequency");
                MemberCopy(ref property, resetObject.smooth, "smooth");
            }

            static DRMAnimatableProperty MemberCopy(SerializedProperty property)
            {
                DRMAnimatableProperty prop = new DRMAnimatableProperty();

                prop.evolutionType = (DRMAnimatableProperty.EVOLUTION_TYPE)property.FindPropertyRelative("evolutionType").enumValueIndex;
                prop.startValue = property.FindPropertyRelative("startValue").vector2Value;
                prop.endValue = property.FindPropertyRelative("endValue").vector2Value;
                prop.curve = property.FindPropertyRelative("curve").animationCurveValue;

                return prop;
            }

            static void MemberCopy(ref SerializedProperty property, DRMAnimatableProperty objectProperty, string name)
            {
                property.FindPropertyRelative(name).FindPropertyRelative("evolutionType").enumValueIndex = (int)objectProperty.evolutionType;
                property.FindPropertyRelative(name).FindPropertyRelative("startValue").vector2Value = objectProperty.startValue;
                property.FindPropertyRelative(name).FindPropertyRelative("endValue").vector2Value = objectProperty.endValue;
                property.FindPropertyRelative(name).FindPropertyRelative("curve").animationCurveValue = objectProperty.curve;
            }
        }
    }
}