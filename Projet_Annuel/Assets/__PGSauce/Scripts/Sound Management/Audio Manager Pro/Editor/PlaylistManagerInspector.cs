using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using PGSauce.AudioManagement.External.Utilities;

namespace PGSauce.AudioManagement.External.Editor.Music
{
    /// <summary>Custom inspector for the PlaylistManager.</summary>
    [CustomEditor(typeof(PlaylistManager))]
    public class PlaylistManagerInspector : UnityEditor.Editor
    {
        /// <summary>AMP Banner.</summary>
        [SerializeField]
        private Texture2D Banner;

        /// <summary>The PlaylistManager that this inspector is displaying.</summary>
        private PlaylistManager PMInstance;

        /// <summary>Display for the Playlist library.</summary>
        private ReorderableList PlaylistListDisplay;

        /// <summary>If the PlaylistManager is scene persistent.</summary>
        private SerializedProperty ScenePersistent;
        /// <summary>Library of Playlists.</summary>
        private SerializedProperty PlaylistLibrary;
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
            //Retrieves the PlaylistManager
            PMInstance = (PlaylistManager)target;
            ScenePersistent = serializedObject.FindProperty("ScenePersistent");
            PlaylistLibrary = serializedObject.FindProperty("PlaylistLibrary");
            CollapseLibrary = serializedObject.FindProperty("CollapseLibrary");

            //Creates the Playlist list display
            PlaylistListDisplay = new ReorderableList(serializedObject, PlaylistLibrary, true, true, true, true);

            //Overrides list drawing
            PlaylistListDisplay.drawElementCallback = DrawPlaylistInspector;
            PlaylistListDisplay.drawHeaderCallback = DrawPlaylistListHeader;
            PlaylistListDisplay.onAddCallback = AppendNewPlaylist;
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
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = PlaylistManager.Main;
            EditorGUILayout.LabelField(new GUIContent("Preview", "Preview options for the PlaylistManager. Only available at runtime."));
            if (GUILayout.Button(new GUIContent("Stop", "Stops the currently playing Playlist."))) { PlaylistManager.Main.Stop(); }
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            //Basic Settings
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(ScenePersistent, new GUIContent("Scene Persistent", "Marks the GameObject as DontDestroyOnLoad to provide the manager with scene persistence(Note: manager must be present on a root GameObject for this functionality to work)."));
            GUILayout.EndVertical();

            //Updates Playlist
            CollapseLibrary.boolValue = EditorGUILayout.Foldout(CollapseLibrary.boolValue, new GUIContent("Playlist Library", "Library of Playlists stored on the PlaylistManager so that Playlists can be started by name without needing a reference to the Playlist."));
            if (CollapseLibrary.boolValue) { PlaylistListDisplay.DoLayoutList(); }
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>Draws the sub-inspector for a Playlist.</summary>
        /// <param name="DrawRect">The rect in the list display that this Track has to be drawn in.</param>
        /// <param name="Index">The index of this Playlist in the library.</param>
        /// <param name="IsActive">If the Playlist is actively selected.</param>
        /// <param name="IsFocused">If the Playlist is in focus.</param>
        private void DrawPlaylistInspector(Rect DrawRect, int Index, bool IsActive, bool IsFocused)
        {
            //Shifts rect to create border
            DrawRect.y += 2;

            //Calculates commonly used spacings
            float LabelWidth = EditorGUIUtility.labelWidth - 20;
            float FieldPadding = 20f;

            //Caches serialised properties
            SerializedProperty CurrentTrack = PlaylistListDisplay.serializedProperty.GetArrayElementAtIndex(Index);

            //Determines name and tooltip for the Track display
            string TrackDisplayName, Tooltip;
            if (PMInstance.PlaylistLibrary[Index] == null)
            {
                //For null Tracks
                TrackDisplayName = "N/A";
                Tooltip = "Please select a Playlist.";
            }
            else
            {
                //Uses Track Name, then SO name
                if (!String.IsNullOrEmpty(PMInstance.PlaylistLibrary[Index].PlaylistName)) { TrackDisplayName = PMInstance.PlaylistLibrary[Index].PlaylistName; }
                else { TrackDisplayName = PMInstance.PlaylistLibrary[Index].name; }
                Tooltip = "Track " + (Index + 1).ToString() + " - " + TrackDisplayName;
            }

            //Determines style
            GUIStyle TrackStyle = PMInstance.PlaylistLibrary[Index] == null ? InvalidStyle : EditorStyles.label;

            //Draws Track selector
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, LabelWidth, EditorGUIUtility.singleLineHeight), new GUIContent(TrackDisplayName, Tooltip), TrackStyle);
            EditorGUI.PropertyField(new Rect(DrawRect.x + LabelWidth, DrawRect.y, DrawRect.width - LabelWidth - FieldPadding, EditorGUIUtility.singleLineHeight), CurrentTrack, GUIContent.none);
        }

        /// <summary>Draws the header of the Playlist list.</summary>
        /// <param name="DrawRect">The available rect of the list header.</param>
        private void DrawPlaylistListHeader(Rect DrawRect)
        {
            //Displays list title
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, 50, DrawRect.height), new GUIContent("Playlists", "The different Playlists in the PlaylistManager's Playlist library."), PMInstance.PlaylistLibrary.Length == 0 ? InvalidStyle : EditorStyles.label);

            //Displays Track counter
            EditorGUI.LabelField(new Rect((DrawRect.x + DrawRect.width) / 2 - 40, DrawRect.y, 80, DrawRect.height), PMInstance.PlaylistLibrary.Length.ToString() + (PMInstance.PlaylistLibrary.Length == 1 ? " Playlist" : " Playlists"), PMInstance.PlaylistLibrary.Length == 0 ? InvalidMiniStyle : CenteredMiniLabel);
        }

        /// <summary>Appends a new Playlist to the Playlist.</summary>
        /// <param name="Target">The ReorderableList associated with this PlaylistManager.</param>
        private void AppendNewPlaylist(ReorderableList Target)
        {
            //Creates new Edition
            int Index = Target.serializedProperty.arraySize;
            Target.serializedProperty.arraySize++;
            Target.index = Index;
            SerializedProperty NewPlaylist = Target.serializedProperty.GetArrayElementAtIndex(Index);

            //Resets properties
            NewPlaylist.objectReferenceValue = null;
        }
    }
}
