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
    [CustomEditor(typeof(MusicManager))]
    public class MusicManagerInspector : UnityEditor.Editor
    {
        /// <summary>AMP Banner.</summary>
        [SerializeField]
        private Texture2D Banner;

        [SerializeField]
        public GUIStyle s;

        /// <summary>The MusicManager that this inspector is displaying.</summary>
        private MusicManager MMInstance;

        /// <summary>Display for the Track library.</summary>
        private ReorderableList TrackListDisplay;

        /// <summary>The mixer used for this MusicManager.</summary>
        private SerializedProperty Mixer;
        /// <summary>If the MusicManager is scene persistent.</summary>
        private SerializedProperty ScenePersistent;
        /// <summary>Library of Tracks.</summary>
        private SerializedProperty TrackLibrary;
        private SerializedProperty CollapseLibrary;

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
            //Retrieves the MusicManager
            MMInstance = (MusicManager)target;
            MMInstance.OnStateChange += Repaint;
            Mixer = serializedObject.FindProperty("Mixer");
            ScenePersistent = serializedObject.FindProperty("ScenePersistent");
            TrackLibrary = serializedObject.FindProperty("TrackLibrary");
            CollapseLibrary = serializedObject.FindProperty("CollapseLibrary");

            //Creates the Track list display
            TrackListDisplay = new ReorderableList(serializedObject, TrackLibrary, true, true, true, true);

            //Overrides list drawing
            TrackListDisplay.drawElementCallback = DrawTrackInspector;
            TrackListDisplay.drawHeaderCallback = DrawTrackListHeader;
            TrackListDisplay.onAddCallback = AppendNewTrack;
        }

        private void OnDisable() { MMInstance.OnStateChange -= Repaint; }

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
                CenteredMiniLabel.normal.textColor = new Color(0.3f, 0.3f, 0.3f, 1);
            }
        }

        //Draws Track inspector
        public override void OnInspectorGUI()
        {
            //Updates the MusicManager object
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
            GUI.enabled = MusicManager.Main;
            EditorGUILayout.LabelField(new GUIContent("Preview", "Preview options for the MusicManager. Only available at runtime."));
            GUI.enabled = MusicManager.Main && MusicManager.Main.IsPlaying();
            if (GUILayout.Button(new GUIContent("Stop", "Stops the currently playing Track."))) { MusicManager.Main.Stop(); }
            if (GUILayout.Button(new GUIContent("Restart", "Restarts the currently playing Track."))) { MusicManager.Main.Restart(); }
            if (MusicManager.Main)
            {
                GUI.enabled = true;
                if (MusicManager.Main.Paused) { if (GUILayout.Button(new GUIContent("Unpause", "Unpauses the MusicManager."))) { MusicManager.Main.UnPause(); } }
                else { if (GUILayout.Button(new GUIContent("Pause", "Pauses the MusicManager."))) { MusicManager.Main.Pause(); } }
            }
            else
            {
                GUI.enabled = false;
                GUILayout.Button(new GUIContent("Pause", "Pauses the MusicManager."));
            }
            EditorGUILayout.EndHorizontal();
            GUI.enabled = MusicManager.Main;
            MMInstance.SetVolume(EditorGUILayout.Slider(new GUIContent("Volume", "Global volume of the MusicManager"), MMInstance.MasterVolume, 0, 1));
            MMInstance.Muted = EditorGUILayout.Toggle(new GUIContent("Muted", "If the MusicManager is muted"), MMInstance.Muted);
            EditorGUILayout.LabelField(new GUIContent("Current Voice Count: " + (MMInstance ? MMInstance.CurrentAudioSourceCount.ToString() : "0"), "The number of active playing voices in the MusicManager."));
            GUI.enabled = true;
            EditorGUILayout.EndVertical();

            //Basic Settings
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(Mixer, new GUIContent("Mixer", "(Optional) Audio mixer to set as the output of all audio handled by MusicManager."));
            EditorGUILayout.PropertyField(ScenePersistent, new GUIContent("Scene Persistent", "Marks the GameObject as DontDestroyOnLoad to provide the manager with scene persistence(Note: manager must be present on a root GameObject for this functionality to work)."));
            GUILayout.EndVertical();

            //Updates MusicManager
            CollapseLibrary.boolValue = EditorGUILayout.Foldout(CollapseLibrary.boolValue, new GUIContent("Track Library", "Library of Tracks stored on the MusicManager so that Tracks can be started by name without needing a reference to the Track."));
            if (CollapseLibrary.boolValue) { TrackListDisplay.DoLayoutList(); }
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>Draws the sub-inspector for a Track.</summary>
        /// <param name="DrawRect">The rect in the list display that this Track has to be drawn in.</param>
        /// <param name="Index">The index of this Track in the Track.</param>
        /// <param name="IsActive">If the Track is actively selected.</param>
        /// <param name="IsFocused">If the Track is in focus.</param>
        private void DrawTrackInspector(Rect DrawRect, int Index, bool IsActive, bool IsFocused)
        {
            //Shifts rect to create border
            DrawRect.y += 2;

            //Calculates commonly used spacings
            float LabelWidth = EditorGUIUtility.labelWidth - 20;
            float FieldPadding = 20f;

            //Caches serialised properties
            SerializedProperty CurrentTrack = TrackListDisplay.serializedProperty.GetArrayElementAtIndex(Index);

            //Determines name and tooltip for the Track display
            string TrackDisplayName, Tooltip;
            if (MMInstance.TrackLibrary[Index] == null)
            {
                //For null Tracks
                TrackDisplayName = "N/A";
                Tooltip = "Please select a Track.";
            }
            else
            {
                //Uses Track Name, then SO name
                if (!String.IsNullOrEmpty(MMInstance.TrackLibrary[Index].Name)) { TrackDisplayName = MMInstance.TrackLibrary[Index].Name; }
                else { TrackDisplayName = MMInstance.TrackLibrary[Index].name; }
                Tooltip = "Track " + (Index + 1).ToString() + " - " + TrackDisplayName;
            }

            //Determines style
            GUIStyle TrackStyle = MMInstance.TrackLibrary[Index] == null ? InvalidStyle : EditorStyles.label;

            //Draws Track selector
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, LabelWidth, EditorGUIUtility.singleLineHeight), new GUIContent(TrackDisplayName, Tooltip), TrackStyle);
            EditorGUI.PropertyField(new Rect(DrawRect.x + LabelWidth, DrawRect.y, DrawRect.width - LabelWidth - FieldPadding, EditorGUIUtility.singleLineHeight), CurrentTrack, GUIContent.none);
        }

        /// <summary>Draws the header of the Track list.</summary>
        /// <param name="DrawRect">The available rect of the list header.</param>
        private void DrawTrackListHeader(Rect DrawRect)
        {
            //Displays list title
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, 50, DrawRect.height), new GUIContent("Tracks", "The different Tracks in the MusicManager's Track library."), MMInstance.TrackLibrary.Length == 0 ? InvalidStyle : EditorStyles.label);

            //Displays Track counter
            EditorGUI.LabelField(new Rect((DrawRect.x + DrawRect.width) / 2 - 40, DrawRect.y, 80, DrawRect.height), MMInstance.TrackLibrary.Length.ToString() + (MMInstance.TrackLibrary.Length == 1 ? " Track" : " Tracks"), MMInstance.TrackLibrary.Length == 0 ? InvalidMiniStyle : CenteredMiniLabel);
        }

        /// <summary>Appends a new Track to the Playlist.</summary>
        /// <param name="Target">The ReorderableList associated with this Playlist.</param>
        private void AppendNewTrack(ReorderableList Target)
        {
            //Creates new Edition
            int Index = Target.serializedProperty.arraySize;
            Target.serializedProperty.arraySize++;
            Target.index = Index;
            SerializedProperty NewTrack = Target.serializedProperty.GetArrayElementAtIndex(Index);

            //Resets properties
            NewTrack.objectReferenceValue = null;
        }
    }
}
