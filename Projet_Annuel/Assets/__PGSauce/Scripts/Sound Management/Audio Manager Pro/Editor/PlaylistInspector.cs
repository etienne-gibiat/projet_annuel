using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;

/// <summary>Internal editor code for AMP editors for music.</summary>
namespace PGSauce.AudioManagement.External.Editor.Music
{
    [CustomEditor(typeof(Playlist))]
    public class PlaylistInspector : UnityEditor.Editor
    {
        /// <summary>AMP Banner.</summary>
        [SerializeField]
        private Texture2D Banner;

        /// <summary>The Playlist that this inspector is displaying.</summary>
        private Playlist PlaylistInstance;

        /// <summary>Display for the Track's Editions.</summary>
        private ReorderableList TrackListDisplay;

        /// <summary>The name the Playlist.</summary>
        private SerializedProperty PlaylistName;
        /// <summary>The shuffle mode of the Playlist.</summary>
        private SerializedProperty Shuffle;
        /// <summary>The crossfade mode of the Playlist.</summary>
        private SerializedProperty Crossfade;
        /// <summary>The crossfade duration of the Playlist.</summary>
        private SerializedProperty CrossfadeDuration;
        /// <summary>The Track gap duration of the Playlist.</summary>
        private SerializedProperty TrackGap;
        /// <summary>The list of tracks in the Playlist</summary>
        private SerializedProperty TrackList;

        /// <summary>Text style for an invalid Track.</summary>
        private GUIStyle InvalidStyle;
        /// <summary>Mini text style for an invalid Track.</summary>
        private GUIStyle InvalidMiniStyle;
        /// <summary>Style for centered mini labels.</summary>
        private GUIStyle CenteredMiniLabel;
        /// <summary>If the styles have been initialised.</summary>
        private bool InitialisedStyles;

        //Initialises inspector
        private void OnEnable()
        {
            //Retrieves the Playlist
            PlaylistInstance = (Playlist)target;

            //Caches serialised properties
            PlaylistName = serializedObject.FindProperty("PlaylistName");
            Shuffle = serializedObject.FindProperty("Shuffle");
            Crossfade = serializedObject.FindProperty("Crossfade");
            CrossfadeDuration = serializedObject.FindProperty("CrossfadeDuration");
            TrackGap = serializedObject.FindProperty("TrackGapDuration");
            TrackList = serializedObject.FindProperty("Tracks");

            //Creates the Track list display
            TrackListDisplay = new ReorderableList(serializedObject, TrackList, true, true, true, true);

            //Overrides list drawing
            TrackListDisplay.drawElementCallback = DrawTrackInspector;
            TrackListDisplay.drawHeaderCallback = DrawTrackListHeader;
            TrackListDisplay.onAddCallback = AppendNewTrack;
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

        //Draws Playlist inspector
        public override void OnInspectorGUI()
        {
            //Updates the Playlist object
            serializedObject.Update();
            CreateStyles();

            //Banner display
            Rect BannerRect = GUILayoutUtility.GetRect(0.0f, 0.0f);
            BannerRect.height = Screen.width * 250 / 1600;
            GUILayout.Space(BannerRect.height);
            GUI.Label(BannerRect, Banner);

            //Displays Playlist name
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(PlaylistName, new GUIContent("Playlist Name", "(Optional) The name of the Playlist."));
            GUILayout.EndVertical();

            //Shuffle settings
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(Shuffle, new GUIContent("Suffle Mode", "The shuffling mode used for this Playlist."));
            GUILayout.EndVertical();

            //Crossfade settings
            GUILayout.BeginVertical(GUILayout.Height((PlaylistInstance.Crossfade == Playlist.CrossfadeMode.ForcedOn ? 2 : 1) * EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(Crossfade, new GUIContent("Crossfade Mode", "The crossfading mode for this Playlist."));
            if (PlaylistInstance.Crossfade == Playlist.CrossfadeMode.ForcedOn)
            {
                //Duration Settings
                EditorGUILayout.PropertyField(CrossfadeDuration, new GUIContent("Crossfade Duration", "The duration of the crossfade between different Tracks."));
                CrossfadeDuration.floatValue = Mathf.Max(0f, CrossfadeDuration.floatValue);
            }
            GUILayout.EndVertical();

            //Gap Settings
            if (PlaylistInstance.Crossfade == Playlist.CrossfadeMode.ForcedOff)
            {
                GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
                EditorGUILayout.PropertyField(TrackGap, new GUIContent("Track Gap Duration", "The duration of the gap between subsequent tracks."));
                GUILayout.EndVertical();
            }

            //Preview
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = PlaylistManager.Main;
            EditorGUILayout.LabelField(new GUIContent("Preview", "Preview options for the Playlist. Only available at runtime."));
            if (GUILayout.Button(new GUIContent("Play", "Plays the Playlist."))) { PlaylistManager.Main.Play(PlaylistInstance); }
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            //Draws list and updates Playlist
            TrackListDisplay.DoLayoutList();
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
            if (PlaylistInstance.Tracks[Index] == null)
            {
                //For null Tracks
                TrackDisplayName = "N/A";
                Tooltip = "Please select a Track.";
            }
            else
            {
                //Uses Track Name, then SO name
                if (!String.IsNullOrEmpty(PlaylistInstance.Tracks[Index].Name)) { TrackDisplayName = PlaylistInstance.Tracks[Index].Name; }
                else { TrackDisplayName = PlaylistInstance.Tracks[Index].name; }
                Tooltip = "Track " + (Index + 1).ToString() + " - " + TrackDisplayName;
            }

            //Determines style
            GUIStyle TrackStyle = PlaylistInstance.Tracks[Index] == null ? InvalidStyle : EditorStyles.label;

            //Draws Track selector
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, LabelWidth, EditorGUIUtility.singleLineHeight), new GUIContent(TrackDisplayName, Tooltip), TrackStyle);
            EditorGUI.PropertyField(new Rect(DrawRect.x + LabelWidth, DrawRect.y, DrawRect.width - LabelWidth - FieldPadding, EditorGUIUtility.singleLineHeight), CurrentTrack, GUIContent.none);
        }

        /// <summary>Draws the header of the Track list.</summary>
        /// <param name="DrawRect">The available rect of the list header.</param>
        private void DrawTrackListHeader(Rect DrawRect)
        {
            //Displays list title
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, 50, DrawRect.height), new GUIContent("Tracks", "The different Tracks included in this Playlist."), PlaylistInstance.Tracks.Length == 0 ? InvalidStyle : EditorStyles.label);

            //Displays Track counter
            EditorGUI.LabelField(new Rect((DrawRect.x + DrawRect.width) / 2 - 40, DrawRect.y, 80, DrawRect.height), PlaylistInstance.Tracks.Length.ToString() + (PlaylistInstance.Tracks.Length == 1 ? " Track" : " Tracks"), PlaylistInstance.Tracks.Length == 0 ? InvalidMiniStyle : CenteredMiniLabel);
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
