using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using PGSauce.AudioManagement;
using PGSauce.AudioManagement.External.Utilities;

namespace PGSauce.AudioManagement.External.Editor.Music
{
    /// <summary>Custom inspector for Tracks.</summary>
    [CustomEditor(typeof(PgMusicTrack))]
    public class TrackInspector : UnityEditor.Editor
    {
        /// <summary>AMP Banner.</summary>
        [SerializeField]
        private Texture2D Banner;

        /// <summary>The Track that this inspector is displaying.</summary>
        private Track TrackInstance;

        /// <summary>Display for the Track's Editions.</summary>
        private ReorderableList EditionListDisplay;

        /// <summary>The name of the Track.</summary>
        private SerializedProperty TrackName;
        /// <summary>The intro of the Track.</summary>
        private SerializedProperty Intro;
        /// <summary>The volume of the intro.</summary>
        private SerializedProperty IntroVolume;
        /// <summary>The overlap duration of the intro.</summary>
        private SerializedProperty IntroOverlap;
        /// <summary>The Editions contained within this Track.</summary>
        private SerializedProperty EditionList;

        /// <summary>Text style for an invalid Edition.</summary>
        private GUIStyle InvalidStyle;
        /// <summary>Bold text style for an invalid SFXLayer.</summary>
        private GUIStyle InvalidBoldStyle;
        /// <summary>Style for centered mini labels.</summary>
        private GUIStyle CenteredMiniLabel;
        /// <summary>If the styles have been initialised.</summary>
        private bool InitialisedStyles;

        //Initialises inspector
        private void OnEnable()
        {
            //Retrieves the Track and adds an Edition if none are present
            TrackInstance = (Track)target;
            if (TrackInstance.Editions == null || TrackInstance.Editions.Length == 0) { TrackInstance.Editions = new Edition[1]; }

            //Caches serialised properties
            TrackName = serializedObject.FindProperty("Name");
            EditionList = serializedObject.FindProperty("Editions");
            Intro = serializedObject.FindProperty("Intro");
            IntroOverlap = serializedObject.FindProperty("OverlapDuration");
            IntroVolume = serializedObject.FindProperty("IntroVolume");

            //Creates the Edition list display
            EditionListDisplay = new ReorderableList(serializedObject, EditionList, true, true, true, true);

            //Overrides list drawing
            EditionListDisplay.onAddCallback = AppendNewEdition;
            EditionListDisplay.elementHeightCallback = DetermineEditionInspectorHeight;
            EditionListDisplay.drawElementCallback = DrawEditionInspector;
            EditionListDisplay.drawHeaderCallback = DrawEditionListHeader;
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

                //Creates the mini label style
                CenteredMiniLabel = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
                CenteredMiniLabel.normal.textColor = new Color(0.3f, 0.3f, 0.3f, 1);
            }
        }

        //Draws Track inspector
        public override void OnInspectorGUI()
        {
            //Updates the Track object
            serializedObject.Update();
            CreateStyles();

            //Banner display
            Rect BannerRect = GUILayoutUtility.GetRect(0.0f, 0.0f);
            BannerRect.height = Screen.width * 250 / 1600;
            GUILayout.Space(BannerRect.height);
            GUI.Label(BannerRect, Banner);

            //Displays Track name
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(TrackName, new GUIContent("Track Name", "(Optional) The name of the Track."));
            GUILayout.EndVertical();

            //Draws Intro field
            GUILayout.BeginVertical(GUILayout.Height((TrackInstance.Intro != null ? 3 : 1) * EditorGUIUtility.singleLineHeight));
            Rect IntroRect = EditorGUILayout.GetControlRect();
            EditorGUI.LabelField(new Rect(IntroRect.x, IntroRect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight), new GUIContent("Intro Audio", "(Optional) The intro audio to be played before looping the main Edition."));
            EditorGUI.PropertyField(new Rect(IntroRect.x + EditorGUIUtility.labelWidth, IntroRect.y, IntroRect.width - EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight), Intro, GUIContent.none);

            //Full intro details
            if (TrackInstance.Intro != null)
            {
                //Draws intro duration
                EditorGUI.LabelField(new Rect(IntroRect.x + 82, IntroRect.y, EditorGUIUtility.labelWidth - 20 - 82, EditorGUIUtility.singleLineHeight), StringFormatting.FormatTime(TrackInstance.Intro.length), CenteredMiniLabel);

                //Draws Volume slider
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Intro Volume", "The volume of the intro audio."), GUILayout.Width(EditorGUIUtility.labelWidth - 4));
                IntroVolume.floatValue = EditorGUILayout.Slider(IntroVolume.floatValue, 0, 1);
                EditorGUILayout.EndHorizontal();

                //Draws overlap slider
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Intro Overlap Duration", "Duration of the overlap between the end of the intro and the start of the first Edition."), GUILayout.Width(EditorGUIUtility.labelWidth - 4));
                IntroOverlap.floatValue = EditorGUILayout.Slider(IntroOverlap.floatValue, 0, TrackInstance.Intro.length);
                EditorGUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();

            //Preview
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = MusicManager.Main;
            EditorGUILayout.LabelField(new GUIContent("Preview", "Preview options for the Track. Only available at runtime."));
            if (GUILayout.Button(new GUIContent("Play", "Plays the Track."))) { MusicManager.Main.Play(TrackInstance); }
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            //Adds new Edition if the list is empty
            if (TrackInstance.Editions.Length == 0)
            {
                TrackInstance.Editions = new Edition[1];
                TrackInstance.Editions[0].SoundtrackVolume = 1f;
                TrackInstance.Editions[0].TransitionVolume = 1f;
            }

            //Enables Edition removal button if more than one is present
            EditionListDisplay.displayRemove = TrackInstance.Editions.Length > 1;

            //Draws list and updates Track
            EditionListDisplay.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>Draws the sub-inspector for an Edition.</summary>
        /// <param name="DrawRect">The rect in the list display that this Edition has to be drawn in.</param>
        /// <param name="Index">The index of this Edition in the Track.</param>
        /// <param name="IsActive">If the Edition is actively selected.</param>
        /// <param name="IsFocused">If the Edition is in focus.</param>
        private void DrawEditionInspector(Rect DrawRect, int Index, bool IsActive, bool IsFocused)
        {
            //Shifts rect to create border
            DrawRect.y += 2;

            //Calculates commonly used spacings
            float Spacing = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            float LabelWidth = EditorGUIUtility.labelWidth - 20;
            float FieldPadding = 20f;

            //Starts counter for current displayed property
            int PropertyIndex = 0;

            //Caches serialised properties
            SerializedProperty CurrentEdition = EditionListDisplay.serializedProperty.GetArrayElementAtIndex(Index);
            SerializedProperty NameProperty = CurrentEdition.FindPropertyRelative("Name");
            SerializedProperty SoundtrackProperty = CurrentEdition.FindPropertyRelative("Soundtrack");
            SerializedProperty SoundtrackVolumeProperty = CurrentEdition.FindPropertyRelative("SoundtrackVolume");
            SerializedProperty TransitionSoundProperty = CurrentEdition.FindPropertyRelative("TransitionSound");
            SerializedProperty TransitionVolumeProperty = CurrentEdition.FindPropertyRelative("TransitionVolume");
            SerializedProperty PlaybackProperty = CurrentEdition.FindPropertyRelative("KeepPlaybackTime");
            SerializedProperty FadeProperty = CurrentEdition.FindPropertyRelative("FadeLength");

            //Draws a property with a custom style
            Action<SerializedProperty, string, string, GUIStyle> DrawPropertyStyled = (SerializedProperty Property, string PropertyName, string Tooltip, GUIStyle TextStyle) =>
            {
                //Draws label
                EditorGUI.LabelField(new Rect(DrawRect.x, (PropertyIndex + 1) * Spacing + DrawRect.y, LabelWidth, EditorGUIUtility.singleLineHeight), new GUIContent(PropertyName, Tooltip), TextStyle);

                //Draws property field
                EditorGUI.PropertyField(new Rect(DrawRect.x + LabelWidth, (PropertyIndex + 1) * Spacing + DrawRect.y, DrawRect.width - LabelWidth - FieldPadding, EditorGUIUtility.singleLineHeight), Property, GUIContent.none);

                //Increments property counter
                PropertyIndex++;
            };

            //Draws a slider for a volume property
            Action<SerializedProperty, string, string> DrawVolumeSlider = (SerializedProperty Property, string PropertyName, string Tooltip) =>
            {
                //Draws label
                EditorGUI.LabelField(new Rect(DrawRect.x, (PropertyIndex + 1) * Spacing + DrawRect.y, LabelWidth, EditorGUIUtility.singleLineHeight), new GUIContent(PropertyName, Tooltip));

                //Draws the slider
                Property.floatValue = EditorGUI.Slider(new Rect(DrawRect.x + LabelWidth, (PropertyIndex + 1) * Spacing + DrawRect.y, DrawRect.width - LabelWidth - FieldPadding, EditorGUIUtility.singleLineHeight), Property.floatValue, 0, 1f);

                //Increments property counter
                PropertyIndex++;
            };

            //Draws a property
            Action<SerializedProperty, string, string> DrawProperty = (SerializedProperty Property, string PropertyName, string Tooltip) =>
            {
                DrawPropertyStyled(Property, PropertyName, Tooltip, EditorStyles.label);
            };

            //Gets and draws edition name
            string Name = NameProperty.stringValue;
            if (string.IsNullOrEmpty(Name.Trim())) { Name = "Untitled"; }
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, DrawRect.width - 90, EditorGUIUtility.singleLineHeight), Name, (TrackInstance.Editions[Index].Soundtrack == null && TrackInstance.CollapseEditions ? InvalidBoldStyle : EditorStyles.boldLabel));

            //Draws main edition if it is the first edition
            if (Index == 0) { EditorGUI.LabelField(new Rect(DrawRect.x + DrawRect.width - 90, DrawRect.y, 70, EditorGUIUtility.singleLineHeight), "Main Edition", EditorStyles.miniLabel); }

            //Draws sountrack length
            if (TrackInstance.Editions[Index].Soundtrack != null)
            {
                EditorGUI.LabelField(new Rect((DrawRect.x + DrawRect.width) / 2 - 40, DrawRect.y, 80, EditorGUIUtility.singleLineHeight), StringFormatting.FormatTime(TrackInstance.Editions[Index].Soundtrack.length), CenteredMiniLabel);
            }

            //Displays basic properties if uncollapsed
            if (!TrackInstance.CollapseEditions)
            {
                DrawProperty(NameProperty, "Edition Name", "(Optional) The name of the Edition.");
                DrawPropertyStyled(SoundtrackProperty, "Soundtrack", "The AudioClip containing the soundtrack for this Edition.", (TrackInstance.Editions[Index].Soundtrack == null ? InvalidStyle : EditorStyles.label));

                //Shows full properties if the sountrack is not null
                if (TrackInstance.Editions[Index].Soundtrack != null)
                {
                    DrawVolumeSlider(SoundtrackVolumeProperty, "Soundtrack Volume", "The volume of the soundtrack in this Edition.");
                    DrawProperty(TransitionSoundProperty, "Transition Sound", "(Optional) The AudioClip for the transition sound to be played when switching to this Edition of the Track.");

                    //Displays transiton sound properties
                    if (TrackInstance.Editions[Index].TransitionSound != null)
                    {
                        EditorGUI.LabelField(new Rect(DrawRect.x + 96, PropertyIndex * Spacing + DrawRect.y, LabelWidth - 96, EditorGUIUtility.singleLineHeight), StringFormatting.FormatTime(TrackInstance.Editions[Index].TransitionSound.length), CenteredMiniLabel);
                        DrawVolumeSlider(TransitionVolumeProperty, "Transition Volume", "The volume of the transition sound to this Edition.");
                    }

                    DrawProperty(PlaybackProperty, "Keep Playback Time", "Whether to play the soundtrack from the beginning when switching to this Edition or whether to retain the playback time from the previous Edition.");
                    DrawProperty(FadeProperty, "Fade Duration", "The duration of the cross-fade when switching to this Edition.");
                    FadeProperty.floatValue = Mathf.Max(0, FadeProperty.floatValue);
                }
                else
                {
                    //Displays error if no soundtrack is selected
                    EditorGUI.HelpBox(new Rect(DrawRect.x, (PropertyIndex + 1) * Spacing + DrawRect.y + 2f, DrawRect.width - FieldPadding, 30), "Soundtrack required. Please assign an AudioClip to this Edition.", MessageType.Error);
                }
            }
        }

        /// <summary>Draws the header of the Edition list.</summary>
        /// <param name="DrawRect">The available rect of the list header.</param>
        private void DrawEditionListHeader(Rect DrawRect)
        {
            //Displays list title
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, 50, DrawRect.height), new GUIContent("Editions", "The different editions or versions of the soundtrack contained in this Track."));

            //Displays Edition counter
            EditorGUI.LabelField(new Rect((DrawRect.x + DrawRect.width) / 2 - 40, DrawRect.y, 80, DrawRect.height), TrackInstance.Editions.Length.ToString() + (TrackInstance.Editions.Length == 1 ? " Edition" : " Editions"), CenteredMiniLabel);

            //Collapse button
            TrackInstance.CollapseEditions = EditorGUI.Toggle(new Rect(DrawRect.width - 25, DrawRect.y + 2, 45, DrawRect.height - 4), TrackInstance.CollapseEditions, EditorStyles.miniButton);
            EditorGUI.LabelField(new Rect(DrawRect.width - 75, DrawRect.y, 50, DrawRect.height), "Collapse", EditorStyles.miniLabel);
        }

        /// <summary>Determines the required height for this Edition's sub-inspector.</summary>
        /// <param name="Index">The index of this Edition in the Track.</param>
        /// <returns>The determined height for this sub-inspector.</returns>
        private float DetermineEditionInspectorHeight(int Index)
        {
            //Calculates commonly used spacings
            float Spacing = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            //Base height
            float Height = Spacing + 2.5f;
            if (!TrackInstance.CollapseEditions)
            {
                //Uncollapsed height addition
                Height += Spacing * 2 + 2.5f;
                if (TrackInstance.Editions[Index].Soundtrack != null)
                {
                    //Extra height for properties displayed
                    Height += Spacing * 4;
                    if (TrackInstance.Editions[Index].TransitionSound != null) { Height += Spacing; }
                }

                //Error height
                else { Height += 35f; }
            }

            //Returns calculated height
            return Height;
        }

        /// <summary>Appends a new Edition to the track.</summary>
        /// <param name="Target">The ReorderableList associated with this Track.</param>
        private void AppendNewEdition(ReorderableList Target)
        {
            //Creates new Edition
            int Index = Target.serializedProperty.arraySize;
            Target.serializedProperty.arraySize++;
            Target.index = Index;
            SerializedProperty NewEdition = Target.serializedProperty.GetArrayElementAtIndex(Index);

            //Resets properties
            NewEdition.FindPropertyRelative("Name").stringValue = "";
            NewEdition.FindPropertyRelative("Soundtrack").objectReferenceValue = null;
            NewEdition.FindPropertyRelative("SoundtrackVolume").floatValue = 1f;
            NewEdition.FindPropertyRelative("TransitionSound").objectReferenceValue = null;
            NewEdition.FindPropertyRelative("TransitionVolume").floatValue = 1f;
            NewEdition.FindPropertyRelative("KeepPlaybackTime").boolValue = false;
            NewEdition.FindPropertyRelative("FadeLength").floatValue = 0f;
        }
    }
}
