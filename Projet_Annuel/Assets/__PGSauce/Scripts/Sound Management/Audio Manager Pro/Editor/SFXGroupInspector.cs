using System.Collections;
using System.Collections.Generic;
using System;
using PGSauce.AudioManagement;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace PGSauce.AudioManagement.External.Editor.SFX
{
    /// <summary>Custom inspector for SFXGroups.</summary>
    [CustomEditor(typeof(PgSfxGroup))]
    public class SFXGroupInspector : UnityEditor.Editor
    {
        /// <summary>AMP Banner.</summary>
        [SerializeField]
        private Texture2D Banner;

        /// <summary>The SFXGroup that this inspector is displaying.</summary>
        private SFXGroup SFXGroupInstance;

        /// <summary>Display for the SFXGroup's SFXObjects.</summary>
        private ReorderableList SFXObjectListDisplay;

        /// <summary>Name of the SFXGroup.</summary>
        private SerializedProperty SFXGroupName;
        /// <summary>The Randomization mode used for this SFXGroup.</summary>
        private SerializedProperty Randomization;
        /// <summary>The list of SFXObjects in the SFXGroup</summary>
        private SerializedProperty SFXObjectList;

        /// <summary>Text style for an invalid SFXObject.</summary>
        private GUIStyle InvalidStyle;
        /// <summary>Mini text style for an invalid SFXObject.</summary>
        private GUIStyle InvalidMiniStyle;
        /// <summary>Style for centered mini labels.</summary>
        private GUIStyle CenteredMiniLabel;
        /// <summary>If the styles have been initialised.</summary>
        private bool InitialisedStyles;

        //Initialises inspector
        private void OnEnable()
        {
            //Retrieves the Playlist
            SFXGroupInstance = (SFXGroup)target;

            //Caches serialised properties
            SFXGroupName = serializedObject.FindProperty("GroupName");
            Randomization = serializedObject.FindProperty("Randomization");
            SFXObjectList = serializedObject.FindProperty("SFXObjects");

            //Creates the Track list display
            SFXObjectListDisplay = new ReorderableList(serializedObject, SFXObjectList, true, true, true, true);

            //Overrides list drawing
            SFXObjectListDisplay.drawElementCallback = DrawSFXObjectInspector;
            SFXObjectListDisplay.drawHeaderCallback = DrawSFXObjectListHeader;
            SFXObjectListDisplay.onAddCallback = AppendNewSFXObject;
        }

        /// <summary>Creates all the custom GUIStyles necessary.</summary>
        private void CreateStyles()
        {
            if (!InitialisedStyles)
            {
                InitialisedStyles = true;
                //Creates the invalid text styles
                InvalidStyle = new GUIStyle(EditorStyles.label);
                InvalidStyle.normal.textColor = Color.red;
                InvalidMiniStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
                InvalidMiniStyle.normal.textColor = Color.red;

                //Creates the mini label style
                CenteredMiniLabel = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
                CenteredMiniLabel.normal.textColor = new Color(0.3f, 0.3f, 0.3f, 1);
            }
        }

        //Draws Track inspector
        public override void OnInspectorGUI()
        {
            //Updates the SFXGroup object
            serializedObject.Update();
            CreateStyles();

            //Banner display
            Rect BannerRect = GUILayoutUtility.GetRect(0.0f, 0.0f);
            BannerRect.height = Screen.width * 250 / 1600;
            GUILayout.Space(BannerRect.height);
            GUI.Label(BannerRect, Banner);

            //Displays SFXGroup name
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(SFXGroupName, new GUIContent("Group Name", "(Optional) The name of this SFXGroup."));
            GUILayout.EndVertical();

            //Displays randomization settings
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(Randomization, new GUIContent("Randomization Mode", "The degree of random behaviour to use when selecting a random SFXObject from the SFXGroup."));
            GUILayout.EndVertical();

            //Preview
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = SFXManager.Main;
            EditorGUILayout.LabelField(new GUIContent("Preview", "Preview options for the SFXGroup. Only available at runtime."));
            if (GUILayout.Button(new GUIContent("Play", "Plays the SFXGroup."))) { SFXManager.Main.Play(SFXGroupInstance); }
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            //Draws list and updates SFXGroup
            SFXObjectListDisplay.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>Draws the sub-inspector for a SFXObject.</summary>
        /// <param name="DrawRect">The rect in the list display that this SFXObject has to be drawn in.</param>
        /// <param name="Index">The index of this SFXObject in the Track.</param>
        /// <param name="IsActive">If the SFXObject is actively selected.</param>
        /// <param name="IsFocused">If the SFXObject is in focus.</param>
        private void DrawSFXObjectInspector(Rect DrawRect, int Index, bool IsActive, bool IsFocused)
        {
            //Shifts rect to create border
            DrawRect.y += 2;

            //Calculates commonly used spacings
            float LabelWidth = EditorGUIUtility.labelWidth - 20;
            float FieldPadding = 20f;

            //Caches serialised properties
            SerializedProperty CurrentSFXObject = SFXObjectListDisplay.serializedProperty.GetArrayElementAtIndex(Index);

            //Determines name and tooltip for the SFXObject display
            string SFXObjectDisplayName, Tooltip;
            if (SFXGroupInstance.SFXObjects[Index] == null)
            {
                //For null SFXObjects
                SFXObjectDisplayName = "N/A";
                Tooltip = "Please select an SFXObject.";
            }
            else
            {
                //Uses SFXObject Name, then SO name
                if (!String.IsNullOrEmpty(SFXGroupInstance.SFXObjects[Index].SFXName)) { SFXObjectDisplayName = SFXGroupInstance.SFXObjects[Index].SFXName; }
                else { SFXObjectDisplayName = SFXGroupInstance.SFXObjects[Index].name; }
                Tooltip = "SFXObject " + (Index + 1).ToString() + " - " + SFXObjectDisplayName;
            }

            //Determines style
            GUIStyle TrackStyle = SFXGroupInstance.SFXObjects[Index] == null ? InvalidStyle : EditorStyles.label;

            //Draws SFXObject selector
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, LabelWidth, EditorGUIUtility.singleLineHeight), new GUIContent(SFXObjectDisplayName, Tooltip), TrackStyle);
            EditorGUI.PropertyField(new Rect(DrawRect.x + LabelWidth, DrawRect.y, DrawRect.width - LabelWidth - FieldPadding, EditorGUIUtility.singleLineHeight), CurrentSFXObject, GUIContent.none);
        }

        /// <summary>Draws the header of the SFXObject list.</summary>
        /// <param name="DrawRect">The available rect of the list header.</param>
        private void DrawSFXObjectListHeader(Rect DrawRect)
        {
            //Displays list title
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, 75, DrawRect.height), new GUIContent("SFXObjects", "The various different SFXObjects that belong to this SFXGroup."), SFXGroupInstance.SFXObjects.Length == 0 ? InvalidStyle : EditorStyles.label);

            //Displays SFXObject counter
            EditorGUI.LabelField(new Rect((DrawRect.x + DrawRect.width) / 2 - 40, DrawRect.y, 80, DrawRect.height), SFXGroupInstance.SFXObjects.Length.ToString() + (SFXGroupInstance.SFXObjects.Length == 1 ? " SFXObject" : " SFXObjects"), SFXGroupInstance.SFXObjects.Length == 0 ? InvalidMiniStyle : CenteredMiniLabel);
        }

        /// <summary>Appends a new SFXObject to the Playlist.</summary>
        /// <param name="Target">The ReorderableList associated with this Playlist.</param>
        private void AppendNewSFXObject(ReorderableList Target)
        {
            //Creates new SFXObject
            int Index = Target.serializedProperty.arraySize;
            Target.serializedProperty.arraySize++;
            Target.index = Index;
            SerializedProperty NewSFXObject = Target.serializedProperty.GetArrayElementAtIndex(Index);

            //Resets properties
            NewSFXObject.objectReferenceValue = null;
        }
    }
}
