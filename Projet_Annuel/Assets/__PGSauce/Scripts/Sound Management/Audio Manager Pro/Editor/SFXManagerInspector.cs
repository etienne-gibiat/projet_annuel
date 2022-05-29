using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using PGSauce.AudioManagement.External.Utilities;

namespace PGSauce.AudioManagement.External.Editor.Music
{
    /// <summary>Custom inspector for the MusicManager.</summary>
    [CustomEditor(typeof(SFXManager))]
    public class SFXManagerInspector : UnityEditor.Editor
    {
        /// <summary>AMP Banner.</summary>
        [SerializeField]
        private Texture2D Banner;

        /// <summary>The SFXManager that this inspector is displaying.</summary>
        private SFXManager SMInstance;

        /// <summary>Display for the SFXObject library.</summary>
        private ReorderableList SFXObjectListDisplay;
        /// <summary>Display for the SFXGroup library.</summary>
        private ReorderableList SFXGroupListDisplay;

        /// <summary>The mixer used for this SFXManager.</summary>
        private SerializedProperty Mixer;
        /// <summary>If the SFXManager is scene persistent.</summary>
        private SerializedProperty ScenePersistent;
        /// <summary>Library of SFXObjects and SFXGroups.</summary>
        private SerializedProperty SFXObjectLibrary;
        private SerializedProperty SFXGroupLibrary;
        private SerializedProperty CollapseObjectLibrary;
        private SerializedProperty CollapseGroupLibrary;

        /// <summary>Text style for an invalid Edition.</summary>
        private GUIStyle InvalidStyle;
        /// <summary>Mini text style for an invalid Track.</summary>
        private GUIStyle InvalidMiniStyle;
        /// <summary>Bold text style for an invalid SFXLayer.</summary>
        private GUIStyle InvalidBoldStyle;
        /// <summary>Style for centered mini labels.</summary>
        private GUIStyle CenteredMiniLabel;
        /// <summary>If the styles have been initialised.</summary>
        private bool InitialisedStyles;

        //Initialises inspector
        private void OnEnable()
        {
            //Retrieves the SFXManager
            SMInstance = (SFXManager)target;
            SMInstance.OnStateChange += Repaint;
            Mixer = serializedObject.FindProperty("Mixer");
            ScenePersistent = serializedObject.FindProperty("ScenePersistent");
            SFXObjectLibrary = serializedObject.FindProperty("SFXObjectLibrary");
            SFXGroupLibrary = serializedObject.FindProperty("SFXGroupLibrary");
            CollapseObjectLibrary = serializedObject.FindProperty("CollapseObjectLibrary");
            CollapseGroupLibrary = serializedObject.FindProperty("CollapseGroupLibrary");

            //Creates the list displays
            SFXObjectListDisplay = new ReorderableList(serializedObject, SFXObjectLibrary, true, true, true, true);
            SFXGroupListDisplay = new ReorderableList(serializedObject, SFXGroupLibrary, true, true, true, true);

            //Overrides list drawing
            SFXObjectListDisplay.drawElementCallback = DrawSFXObjectInspector;
            SFXObjectListDisplay.drawHeaderCallback = DrawSFXObjectListHeader;
            SFXObjectListDisplay.onAddCallback = AppendNewSFXObject;
            SFXGroupListDisplay.drawElementCallback = DrawSFXGroupInspector;
            SFXGroupListDisplay.drawHeaderCallback = DrawSFXGroupListHeader;
            SFXGroupListDisplay.onAddCallback = AppendNewSFXGroup;
        }

        private void OnDisable() { SMInstance.OnStateChange -= Repaint; }

        /// <summary>Creates all the custom GUIStyles necessary.</summary>
        private void CreateStyles()
        {
            if (!InitialisedStyles)
            {
                InitialisedStyles = true;
                //Creates the invalid text styles
                InvalidStyle = new GUIStyle(EditorStyles.label);
                InvalidStyle.normal.textColor = Color.red;
                InvalidBoldStyle = new GUIStyle(EditorStyles.boldLabel);
                InvalidBoldStyle.normal.textColor = Color.red;
                InvalidMiniStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
                InvalidMiniStyle.normal.textColor = Color.red;

                //Creates the mini label style
                CenteredMiniLabel = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
                CenteredMiniLabel.normal.textColor = new Color(1f, 0.3f, 0.3f, 1);
            }
        }

        //Draws Track inspector
        public override void OnInspectorGUI()
        {
            //Updates the SFXManager object
            serializedObject.Update();
            CreateStyles();

            //Banner display
            Rect BannerRect = GUILayoutUtility.GetRect(0.0f, 0.0f);
            BannerRect.height = Screen.width * 250 / 1600;
            GUILayout.Space(BannerRect.height);
            GUI.Label(BannerRect, Banner);

            //Preview
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight * 2));
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = SFXManager.Main;
            EditorGUILayout.LabelField(new GUIContent("Preview", "Preview options for the SFXManager. Only available at runtime."));
            if (GUILayout.Button(new GUIContent("Stop All", "Stops all sound currently being played."))) { SFXManager.Main.StopAll(); }
            if (SFXManager.Main)
            {
                GUI.enabled = true;
                if (SFXManager.Main.Paused) { if (GUILayout.Button(new GUIContent("Unpause", "Unpauses the SFXManager."))) { SFXManager.Main.UnPause(); } }
                else { if (GUILayout.Button(new GUIContent("Pause", "Pauses the SFXManager."))) { SFXManager.Main.Pause(); } }
            }
            else
            {
                GUI.enabled = false;
                GUILayout.Button(new GUIContent("Pause", "Pauses the SFXManager."));
            }
            EditorGUILayout.EndHorizontal();
            GUI.enabled = MusicManager.Main;
            SMInstance.SetVolume(EditorGUILayout.Slider(new GUIContent("Volume", "Global volume of the SFXManager"), SMInstance.MasterVolume, 0, 1));
            SMInstance.Muted = EditorGUILayout.Toggle(new GUIContent("Muted", "If the SFXManager is muted"), SMInstance.Muted);
            EditorGUILayout.LabelField(new GUIContent("Current Voice Count: " + (SMInstance ? SMInstance.CurrentAudioSourceCount.ToString() : "0"), "The number of active playing voices in the SFXManager."));
            GUI.enabled = true;
            EditorGUILayout.EndVertical();

            //Basic Settings
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(Mixer, new GUIContent("Mixer", "(Optional) Audio mixer to set as the output of all audio handled by SFXManager."));
            EditorGUILayout.PropertyField(ScenePersistent, new GUIContent("Scene Persistent", "Marks the GameObject as DontDestroyOnLoad to provide the manager with scene persistence(Note: manager must be present on a root GameObject for this functionality to work)."));
            GUILayout.EndVertical();

            //Updates SFXManager
            CollapseObjectLibrary.boolValue = EditorGUILayout.Foldout(CollapseObjectLibrary.boolValue, new GUIContent("SFXObject Library", "Library of SFXObjects stored on the SFXManager so that SFXObjects can be started by name without needing a reference to the SFXObject."));
            if (CollapseObjectLibrary.boolValue) { SFXObjectListDisplay.DoLayoutList(); }
            CollapseGroupLibrary.boolValue = EditorGUILayout.Foldout(CollapseGroupLibrary.boolValue, new GUIContent("SFXGroup Library", "Library of SFXGroups stored on the SFXManager so that SFXGroups can be started by name without needing a reference to the SFXGroup."));
            if (CollapseGroupLibrary.boolValue) { SFXGroupListDisplay.DoLayoutList(); }
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>Draws the sub-inspector for an SFXObject.</summary>
        /// <param name="DrawRect">The rect in the list display that this SFXObject has to be drawn in.</param>
        /// <param name="Index">The index of this SFXObject in the Library.</param>
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
            SerializedProperty CurrentTrack = SFXObjectListDisplay.serializedProperty.GetArrayElementAtIndex(Index);

            //Determines name and tooltip for the Track display
            string TrackDisplayName, Tooltip;
            if (SMInstance.SFXObjectLibrary[Index] == null)
            {
                //For null Tracks
                TrackDisplayName = "N/A";
                Tooltip = "Please select an SFXObject.";
            }
            else
            {
                //Uses Track Name, then SO name
                if (!String.IsNullOrEmpty(SMInstance.SFXObjectLibrary[Index].SFXName)) { TrackDisplayName = SMInstance.SFXObjectLibrary[Index].SFXName; }
                else { TrackDisplayName = SMInstance.SFXObjectLibrary[Index].name; }
                Tooltip = "SFXObject " + (Index + 1).ToString() + " - " + TrackDisplayName;
            }

            //Determines style
            GUIStyle TrackStyle = SMInstance.SFXObjectLibrary[Index] == null ? InvalidStyle : EditorStyles.label;

            //Draws Track selector
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, LabelWidth, EditorGUIUtility.singleLineHeight), new GUIContent(TrackDisplayName, Tooltip), TrackStyle);
            EditorGUI.PropertyField(new Rect(DrawRect.x + LabelWidth, DrawRect.y, DrawRect.width - LabelWidth - FieldPadding, EditorGUIUtility.singleLineHeight), CurrentTrack, GUIContent.none);
        }

        /// <summary>Draws the header of the SFXObject list.</summary>
        /// <param name="DrawRect">The available rect of the list header.</param>
        private void DrawSFXObjectListHeader(Rect DrawRect)
        {
            //Displays list title
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, 75, DrawRect.height), new GUIContent("SFXObjects", "The different SFXObjects in the SFXManagers's SFXObject library."), SMInstance.SFXObjectLibrary.Length == 0 ? InvalidStyle : EditorStyles.label);

            //Displays Track counter
            EditorGUI.LabelField(new Rect((DrawRect.x + DrawRect.width) / 2 - 40, DrawRect.y, 80, DrawRect.height), SMInstance.SFXObjectLibrary.Length.ToString() + (SMInstance.SFXObjectLibrary.Length == 1 ? " SFXObject" : " SFXObjects"), SMInstance.SFXObjectLibrary.Length == 0 ? InvalidMiniStyle : CenteredMiniLabel);
        }

        /// <summary>Appends a new SFXObject to the Playlist.</summary>
        /// <param name="Target">The ReorderableList associated with this SFXManager.</param>
        private void AppendNewSFXObject(ReorderableList Target)
        {
            //Creates new Edition
            int Index = Target.serializedProperty.arraySize;
            Target.serializedProperty.arraySize++;
            Target.index = Index;
            SerializedProperty NewSFXObject = Target.serializedProperty.GetArrayElementAtIndex(Index);

            //Resets properties
            NewSFXObject.objectReferenceValue = null;
        }

        /// <summary>Draws the sub-inspector for an SFXGroup.</summary>
        /// <param name="DrawRect">The rect in the list display that this SFXGroup has to be drawn in.</param>
        /// <param name="Index">The index of this SFXGroup in the Library.</param>
        /// <param name="IsActive">If the SFXGroup is actively selected.</param>
        /// <param name="IsFocused">If the SFXGroup is in focus.</param>
        private void DrawSFXGroupInspector(Rect DrawRect, int Index, bool IsActive, bool IsFocused)
        {
            //Shifts rect to create border
            DrawRect.y += 2;

            //Calculates commonly used spacings
            float LabelWidth = EditorGUIUtility.labelWidth - 20;
            float FieldPadding = 20f;

            //Caches serialised properties
            SerializedProperty CurrentTrack = SFXGroupListDisplay.serializedProperty.GetArrayElementAtIndex(Index);

            //Determines name and tooltip for the Track display
            string TrackDisplayName, Tooltip;
            if (SMInstance.SFXGroupLibrary[Index] == null)
            {
                //For null Tracks
                TrackDisplayName = "N/A";
                Tooltip = "Please select an SFXGroup.";
            }
            else
            {
                //Uses SFXGroup Name, then SO name
                if (!String.IsNullOrEmpty(SMInstance.SFXGroupLibrary[Index].GroupName)) { TrackDisplayName = SMInstance.SFXGroupLibrary[Index].GroupName; }
                else { TrackDisplayName = SMInstance.SFXGroupLibrary[Index].name; }
                Tooltip = "SFXGroup " + (Index + 1).ToString() + " - " + TrackDisplayName;
            }

            //Determines style
            GUIStyle TrackStyle = SMInstance.SFXGroupLibrary[Index] == null ? InvalidStyle : EditorStyles.label;

            //Draws Track selector
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, LabelWidth, EditorGUIUtility.singleLineHeight), new GUIContent(TrackDisplayName, Tooltip), TrackStyle);
            EditorGUI.PropertyField(new Rect(DrawRect.x + LabelWidth, DrawRect.y, DrawRect.width - LabelWidth - FieldPadding, EditorGUIUtility.singleLineHeight), CurrentTrack, GUIContent.none);
        }

        /// <summary>Draws the header of the SFXGroup list.</summary>
        /// <param name="DrawRect">The available rect of the list header.</param>
        private void DrawSFXGroupListHeader(Rect DrawRect)
        {
            //Displays list title
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, 75, DrawRect.height), new GUIContent("SFXGroups", "The different SFXGroups in the SFXManagers's SFXGroup library."), SMInstance.SFXGroupLibrary.Length == 0 ? InvalidStyle : EditorStyles.label);

            //Displays SFXGroup counter
            EditorGUI.LabelField(new Rect((DrawRect.x + DrawRect.width) / 2 - 40, DrawRect.y, 80, DrawRect.height), SMInstance.SFXGroupLibrary.Length.ToString() + (SMInstance.SFXGroupLibrary.Length == 1 ? " SFXGroup" : " SFXGroups"), SMInstance.SFXGroupLibrary.Length == 0 ? InvalidMiniStyle : CenteredMiniLabel);
        }

        /// <summary>Appends a new SFXGroup to the Playlist.</summary>
        /// <param name="Target">The ReorderableList associated with this SFXManager.</param>
        private void AppendNewSFXGroup(ReorderableList Target)
        {
            //Creates new Edition
            int Index = Target.serializedProperty.arraySize;
            Target.serializedProperty.arraySize++;
            Target.index = Index;
            SerializedProperty NewSFXGroup = Target.serializedProperty.GetArrayElementAtIndex(Index);

            //Resets properties
            NewSFXGroup.objectReferenceValue = null;
        }
    }
}
