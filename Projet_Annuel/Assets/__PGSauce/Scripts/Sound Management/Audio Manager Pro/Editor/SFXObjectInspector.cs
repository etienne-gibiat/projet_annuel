using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using PGSauce.AudioManagement;
using PGSauce.AudioManagement.External.Utilities;

/// <summary>Internal editor code for AMP editors for SFX.</summary>
namespace PGSauce.AudioManagement.External.Editor.SFX
{
    /// <summary>Custom inspector for SFXObjects.</summary>
    [CustomEditor(typeof(PgSfxObject))]
    public class SFXObjectInspector : UnityEditor.Editor
    {
        /// <summary>AMP Banner.</summary>
        [SerializeField]
        private Texture2D Banner;

        /// <summary>The SFXObject that this inspector is displaying.</summary>
        private SFXObject SFXObjectInstance;

        /// <summary>Display for the SFXObject's SFXLayers.</summary>
        private ReorderableList SFXLayerListDisplay;

        /// <summary>Name of the SFXObject.</summary>
        private SerializedProperty SFXName;
        /// <summary>If the volume multiplier for this SFXObject is randomized.</summary>
        private SerializedProperty RandomizeVolume;
        /// <summary>Fixed volume multiplier for this SFXObject.</summary>
        private SerializedProperty FixedVolume;
        /// <summary>Minimum volume multiplier for this SFXObject.</summary>
        private SerializedProperty MinVolume;
        /// <summary>Maximum volume multiplier for this SFXObject.</summary>
        private SerializedProperty MaxVolume;
        /// <summary>If the pitch multiplier for this SFXObject is randomized.</summary>
        private SerializedProperty RandomizePitch;
        /// <summary>Fixed pitch multiplier for this SFXObject.</summary>
        private SerializedProperty FixedPitch;
        /// <summary>Minimum pitch multiplier for this SFXObject.</summary>
        private SerializedProperty MinPitch;
        /// <summary>Maximum pitch multiplier for this SFXObject.</summary>
        private SerializedProperty MaxPitch;
        /// <summary>If this SFXObject has a global delay.</summary>
        private SerializedProperty DelaySFX;
        /// <summary>If the global delay for this SFXObject is randomized.</summary>
        private SerializedProperty RandomizeDelay;
        /// <summary>Fixed global delay for this SFXObject.</summary>
        private SerializedProperty FixedDelay;
        /// <summary>Minimum global delay for this SFXObject.</summary>
        private SerializedProperty MinDelay;
        /// <summary>Maximum global delay for this SFXObject.</summary>
        private SerializedProperty MaxDelay;
        /// <summary>The SFXLayers contained within this SFXObject.</summary>
        private SerializedProperty SFXLayers;
        /// <summary>Limits the polyphony.</summary>
        private SerializedProperty LimitPolyphony;
        /// <summary>Maximum voice count.</summary>
        private SerializedProperty MaxVoices;
        /// <summary>Minimum voice separation.</summary>
        private SerializedProperty MinSeparation;

        /// <summary>Text style for an invalid SFXLayer.</summary>
        private GUIStyle InvalidStyle;
        /// <summary>Bold text style for an invalid SFXLayer.</summary>
        private GUIStyle InvalidBoldStyle;
        /// <summary>Style for centered mini labels.</summary>
        private GUIStyle CenteredMiniLabel;
        /// <summary>Style for left aligned mini labels.</summary>
        private GUIStyle LeftMiniLabel;
        /// <summary>Style for right aligned mini labels.</summary>
        private GUIStyle RightMiniLabel;
        /// <summary>If the styles have been initialised.</summary>
        private bool InitialisedStyles;

        //Initialises inspector
        private void OnEnable()
        {
            //Retrieves the SFXObject and adds a SFXLayer if none are present
            SFXObjectInstance = (SFXObject)target;
            if (SFXObjectInstance.SFXLayers == null || SFXObjectInstance.SFXLayers.Length == 0) { SFXObjectInstance.SFXLayers = new SFXLayer[1]; }

            //Caches serialised properties
            SFXName = serializedObject.FindProperty("SFXName");
            FixedVolume = serializedObject.FindProperty("FixedVolume");
            MinVolume = serializedObject.FindProperty("MinVolume");
            MaxVolume = serializedObject.FindProperty("MaxVolume");
            RandomizeVolume = serializedObject.FindProperty("RandomizeVolume");
            FixedPitch = serializedObject.FindProperty("FixedPitch");
            MinPitch = serializedObject.FindProperty("MinPitch");
            MaxPitch = serializedObject.FindProperty("MaxPitch");
            RandomizePitch = serializedObject.FindProperty("RandomizePitch");
            DelaySFX = serializedObject.FindProperty("DelaySFX");
            FixedDelay = serializedObject.FindProperty("FixedDelayTime");
            MinDelay = serializedObject.FindProperty("MinDelayTime");
            MaxDelay = serializedObject.FindProperty("MaxDelayTime");
            RandomizeDelay = serializedObject.FindProperty("RandomizeDelay");
            SFXLayers = serializedObject.FindProperty("SFXLayers");
            LimitPolyphony = serializedObject.FindProperty("LimitPolyphony");
            MaxVoices = serializedObject.FindProperty("MaxVoices");
            MinSeparation = serializedObject.FindProperty("MinSeparation");

            //Creates the Edition list display
            SFXLayerListDisplay = new ReorderableList(serializedObject, SFXLayers, true, true, true, true);

            //Overrides list drawing
            SFXLayerListDisplay.onAddCallback = AppendNewSFXLayer;
            SFXLayerListDisplay.drawHeaderCallback = DrawSFXLayerListHeader;
            SFXLayerListDisplay.drawElementCallback = DrawSFXLayerInspector;
            SFXLayerListDisplay.elementHeightCallback = DetermineSFXLayerInspectorHeight;
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
                LeftMiniLabel = new GUIStyle(CenteredMiniLabel);
                LeftMiniLabel.fontSize = 9;
                LeftMiniLabel.alignment = TextAnchor.MiddleLeft;
                RightMiniLabel = new GUIStyle(CenteredMiniLabel);
                RightMiniLabel.alignment = TextAnchor.MiddleRight;
            }
        }

        //Draws Track inspector
        public override void OnInspectorGUI()
        {
            //Updates the SFXObject object
            serializedObject.Update();
            CreateStyles();

            //Banner display
            Rect BannerRect = GUILayoutUtility.GetRect(0.0f, 0.0f);
            BannerRect.height = Screen.width * 250 / 1600;
            GUILayout.Space(BannerRect.height);
            GUI.Label(BannerRect, Banner);

            //Displays SFXObject name
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(SFXName, new GUIContent("SFX Name", "(Optional) The name of this SFXObject."));
            GUILayout.EndVertical();

            //Displays volume settings
            GUILayout.BeginVertical(GUILayout.Height(2 * EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(RandomizeVolume, new GUIContent("Randomize Volume", "If the volume of the sound should be randomized."));
            Rect VolumeRect = EditorGUILayout.GetControlRect();
            if (SFXObjectInstance.RandomizeVolume)
            {
                AMPEditorFields.MinMaxSlider(VolumeRect, new GUIContent("Volume", "The range of values that the volume multiplier of the SFXObject can be."), MinVolume, MaxVolume, 0, 1);
                EditorGUI.LabelField(new Rect(VolumeRect.x + 82, VolumeRect.y, EditorGUIUtility.labelWidth - 20 - 82, EditorGUIUtility.singleLineHeight), NumberManipulation.DecimalPlaces(SFXObjectInstance.MinVolume, 2).ToString() + " - " + NumberManipulation.DecimalPlaces(SFXObjectInstance.MaxVolume, 2).ToString(), CenteredMiniLabel);
            }
            else { EditorGUI.Slider(VolumeRect, FixedVolume, 0, 1, new GUIContent("Volume", "The volume multiplier of the SFXObject.")); }
            GUILayout.EndVertical();

            //Displays pitch settings
            GUILayout.BeginVertical(GUILayout.Height(2 * EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(RandomizePitch, new GUIContent("Randomize Pitch", "If the pitch of the sound should be randomized."));
            Rect PitchRect = EditorGUILayout.GetControlRect();
            if (SFXObjectInstance.RandomizePitch)
            {
                AMPEditorFields.MinMaxSlider(PitchRect, new GUIContent("Pitch", "The range of values that the pitch multiplier of the SFXObject can be."), MinPitch, MaxPitch, 0, 3);
                EditorGUI.LabelField(new Rect(PitchRect.x + 82, PitchRect.y, EditorGUIUtility.labelWidth - 20 - 82, EditorGUIUtility.singleLineHeight), NumberManipulation.DecimalPlaces(SFXObjectInstance.MinPitch, 2).ToString() + " - " + NumberManipulation.DecimalPlaces(SFXObjectInstance.MaxPitch, 2).ToString(), CenteredMiniLabel);
            }
            else { EditorGUI.Slider(PitchRect, FixedPitch, 0, 3, new GUIContent("Pitch", "The Pitch multiplier of the SFXObject.")); }
            GUILayout.EndVertical();

            //Displays delay settings
            GUILayout.BeginVertical(GUILayout.Height((SFXObjectInstance.DelaySFX ? (SFXObjectInstance.RandomizeDelay ? 4 : 3) : 1) * EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(DelaySFX, new GUIContent("Delay", "If there should be a global delay before playing this SFXObject."));
            if (SFXObjectInstance.DelaySFX)
            {
                EditorGUILayout.PropertyField(RandomizeDelay, new GUIContent("Randomize Delay", "If the delay time should be randomized."));
                if (SFXObjectInstance.RandomizeDelay)
                {
                    MinDelay.floatValue = Mathf.Max(0f, EditorGUILayout.FloatField(new GUIContent("Minimum Delay", "Minimum duration of the delay."), SFXObjectInstance.MinDelayTime));
                    MaxDelay.floatValue = Mathf.Max(SFXObjectInstance.MinDelayTime, EditorGUILayout.FloatField(new GUIContent("Maximum Delay", "Maximum duration of the delay."), SFXObjectInstance.MaxDelayTime));
                }
                else { FixedDelay.floatValue = Mathf.Max(0f, EditorGUILayout.FloatField(new GUIContent("Delay Duration", "Duration of the delay."), SFXObjectInstance.FixedDelayTime)); }
            }
            GUILayout.EndVertical();

            //Displays polyphony settings
            GUILayout.BeginVertical(GUILayout.Height((SFXObjectInstance.LimitPolyphony ? 2 : 1) * EditorGUIUtility.singleLineHeight));
            EditorGUILayout.PropertyField(LimitPolyphony, new GUIContent("Polyphony Limit", "If polyphony limits should be applied. With polyphony limiting, the behaviour of simultaneous and overlapping sounds is controlled."));
            if (SFXObjectInstance.LimitPolyphony)
            {
                //EditorGUILayout.PropertyField(MaxVoices, new GUIContent("Maximum Voices", "The maximum number of voices for this SFXObject, or how many can be played in tandem. 0 results in infinite voices."));
                EditorGUILayout.PropertyField(MinSeparation, new GUIContent("Minimum Separation", "The minimum time separation between subsequent plays of this SFXObject; if the duration has not been surpassed, the SFXObject will not be played."));
                MaxVoices.intValue = Mathf.Max(0, MaxVoices.intValue);
                MinSeparation.floatValue = Mathf.Max(0, MinSeparation.floatValue);
            }
            GUILayout.EndVertical();

            //Preview
            GUILayout.BeginVertical(GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = SFXManager.Main;
            EditorGUILayout.LabelField(new GUIContent("Preview", "Preview options for the SFXObject. Only available at runtime."));
            if (GUILayout.Button(new GUIContent("Play", "Plays the SFXObject."))) { SFXManager.Main.Play(SFXObjectInstance); }
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            //Adds new SFXLayer if the list is empty
            if (SFXObjectInstance.SFXLayers.Length == 0)
            {
                SFXObjectInstance.SFXLayers = new SFXLayer[1];
                SFXObjectInstance.SFXLayers[0].FixedVolume = 1f;
                SFXObjectInstance.SFXLayers[0].FixedPitch = 1f;
            }

            //Enables SFXLayer removal button if more than one is present
            SFXLayerListDisplay.displayRemove = SFXObjectInstance.SFXLayers.Length > 1;

            //Draws list and updates SFXObject
            SFXLayerListDisplay.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>Draws the header of the SFXLayer list.</summary>
        /// <param name="DrawRect">The available rect of the list header.</param>
        private void DrawSFXLayerListHeader(Rect DrawRect)
        {
            //Displays list title
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, 50, DrawRect.height), new GUIContent("Layers", "The different layers of parts of this SFXObjects."));

            //Displays Edition counter
            EditorGUI.LabelField(new Rect((DrawRect.x + DrawRect.width) / 2 - 40, DrawRect.y, 80, DrawRect.height), SFXObjectInstance.SFXLayers.Length.ToString() + (SFXObjectInstance.SFXLayers.Length == 1 ? " Layer" : " Layers"), CenteredMiniLabel);

            //Collapse button
            SFXObjectInstance.CollapseEditions = EditorGUI.Toggle(new Rect(DrawRect.width - 25, DrawRect.y + 2, 45, DrawRect.height - 4), SFXObjectInstance.CollapseEditions, EditorStyles.miniButton);
            EditorGUI.LabelField(new Rect(DrawRect.width - 75, DrawRect.y, 50, DrawRect.height), "Collapse", EditorStyles.miniLabel);
        }

#if !NET_4_6
    //Creates Actions
    public delegate void Action<T1, T2, T3, T4, T5>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5);
    public delegate void Action<T1, T2, T3, T4, T5, T6>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6);
#endif

        /// <summary>Draws the sub-inspector for an SFXLayer.</summary>
        /// <param name="DrawRect">The rect in the list display that this SFXLayer has to be drawn in.</param>
        /// <param name="Index">The index of this SFXLayer in the Track.</param>
        /// <param name="IsActive">If the SFXLayer is actively selected.</param>
        /// <param name="IsFocused">If the SFXLayer is in focus.</param>
        private void DrawSFXLayerInspector(Rect DrawRect, int Index, bool IsActive, bool IsFocused)
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
            SerializedProperty CurrentSFXLayer = SFXLayerListDisplay.serializedProperty.GetArrayElementAtIndex(Index);
            SerializedProperty NameProperty = CurrentSFXLayer.FindPropertyRelative("LayerName");
            SerializedProperty SFXProperty = CurrentSFXLayer.FindPropertyRelative("SFX");
            SerializedProperty RandomizeVolumeProperty = CurrentSFXLayer.FindPropertyRelative("RandomizeVolume");
            SerializedProperty FixedVolumeProperty = CurrentSFXLayer.FindPropertyRelative("FixedVolume");
            SerializedProperty MinVolumeProperty = CurrentSFXLayer.FindPropertyRelative("MinVolume");
            SerializedProperty MaxVolumeProperty = CurrentSFXLayer.FindPropertyRelative("MaxVolume");
            SerializedProperty RandomizePitchProperty = CurrentSFXLayer.FindPropertyRelative("RandomizePitch");
            SerializedProperty FixedPitchProperty = CurrentSFXLayer.FindPropertyRelative("FixedPitch");
            SerializedProperty MinPitchProperty = CurrentSFXLayer.FindPropertyRelative("MinPitch");
            SerializedProperty MaxPitchProperty = CurrentSFXLayer.FindPropertyRelative("MaxPitch");
            SerializedProperty DelayProperty = CurrentSFXLayer.FindPropertyRelative("DelaySFX");
            SerializedProperty RandomizeDelayProperty = CurrentSFXLayer.FindPropertyRelative("RandomizeDelay");
            SerializedProperty FixedDelayProperty = CurrentSFXLayer.FindPropertyRelative("FixedDelayTime");
            SerializedProperty MinDelayProperty = CurrentSFXLayer.FindPropertyRelative("MinDelayTime");
            SerializedProperty MaxDelayProperty = CurrentSFXLayer.FindPropertyRelative("MaxDelayTime");
            SerializedProperty PriorityProperty = CurrentSFXLayer.FindPropertyRelative("Priority");
            SerializedProperty StereoProperty = CurrentSFXLayer.FindPropertyRelative("StereoPan");
            SerializedProperty SpatialProperty = CurrentSFXLayer.FindPropertyRelative("SpatialBlend");
            SerializedProperty ReverbProperty = CurrentSFXLayer.FindPropertyRelative("ReverbZoneMix");
            SerializedProperty BypassEffectsProperty = CurrentSFXLayer.FindPropertyRelative("BypassEffects");
            SerializedProperty BypassReverbProperty = CurrentSFXLayer.FindPropertyRelative("BypassReverbZones");
            SerializedProperty MuteProperty = CurrentSFXLayer.FindPropertyRelative("Mute");
            SerializedProperty MixerProperty = CurrentSFXLayer.FindPropertyRelative("MixerOverride");
            SerializedProperty MinDistanceProperty = CurrentSFXLayer.FindPropertyRelative("MinDistance");
            SerializedProperty MaxDistanceProperty = CurrentSFXLayer.FindPropertyRelative("MaxDistance");
            SerializedProperty RolloffModeProperty = CurrentSFXLayer.FindPropertyRelative("RolloffMode");

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

            //Draws a slider for a property
            Action<SerializedProperty, string, string, float, float> DrawSlider = (SerializedProperty Property, string PropertyName, string Tooltip, float MinLim, float MaxLim) =>
            {
                //Draws label
                EditorGUI.LabelField(new Rect(DrawRect.x, (PropertyIndex + 1) * Spacing + DrawRect.y, LabelWidth, EditorGUIUtility.singleLineHeight), new GUIContent(PropertyName, Tooltip));

                //Draws the slider
                Property.floatValue = EditorGUI.Slider(new Rect(DrawRect.x + LabelWidth, (PropertyIndex + 1) * Spacing + DrawRect.y, DrawRect.width - LabelWidth - FieldPadding, EditorGUIUtility.singleLineHeight), Property.floatValue, MinLim, MaxLim);

                //Increments property counter
                PropertyIndex++;
            };

            //Draws an int slider for a property
            Action<SerializedProperty, string, string, int, int> DrawIntSlider = (SerializedProperty Property, string PropertyName, string Tooltip, int MinLim, int MaxLim) =>
            {
                //Draws label
                EditorGUI.LabelField(new Rect(DrawRect.x, (PropertyIndex + 1) * Spacing + DrawRect.y, LabelWidth, EditorGUIUtility.singleLineHeight), new GUIContent(PropertyName, Tooltip));

                //Draws the slider
                Property.intValue = (int)EditorGUI.Slider(new Rect(DrawRect.x + LabelWidth, (PropertyIndex + 1) * Spacing + DrawRect.y, DrawRect.width - LabelWidth - FieldPadding, EditorGUIUtility.singleLineHeight), Property.intValue, MinLim, MaxLim);

                //Increments property counter
                PropertyIndex++;
            };

            //Draws a min max slider for a property
            Action<SerializedProperty, SerializedProperty, string, string, float, float> DrawMinMaxSlider = (SerializedProperty PropertyLeft, SerializedProperty PropertyRight, string PropertyName, string Tooltip, float MinLim, float MaxLim) =>
            {
                //Draws label
                EditorGUI.LabelField(new Rect(DrawRect.x, (PropertyIndex + 1) * Spacing + DrawRect.y, LabelWidth, EditorGUIUtility.singleLineHeight), new GUIContent(PropertyName, Tooltip));

                //Draws the slider
                AMPEditorFields.MinMaxSlider(new Rect(DrawRect.x + LabelWidth, (PropertyIndex + 1) * Spacing + DrawRect.y, DrawRect.width - LabelWidth - FieldPadding, EditorGUIUtility.singleLineHeight), GUIContent.none, PropertyLeft, PropertyRight, MinLim, MaxLim);

                //Increments property counter
                PropertyIndex++;
            };

            //Draws a property
            Action<SerializedProperty, string, string> DrawProperty = (SerializedProperty Property, string PropertyName, string Tooltip) =>
            {
                DrawPropertyStyled(Property, PropertyName, Tooltip, EditorStyles.label);
            };

            //Gets and draws SFXLayer name
            string Name = NameProperty.stringValue;
            if (string.IsNullOrEmpty(Name.Trim())) { Name = "Untitled"; }
            EditorGUI.LabelField(new Rect(DrawRect.x, DrawRect.y, DrawRect.width - 90, EditorGUIUtility.singleLineHeight), Name, (SFXObjectInstance.SFXLayers[Index].SFX == null && SFXObjectInstance.CollapseEditions ? InvalidBoldStyle : EditorStyles.boldLabel));

            //Draws SFX length
            if (SFXObjectInstance.SFXLayers[Index].SFX != null)
            {
                EditorGUI.LabelField(new Rect((DrawRect.x + DrawRect.width) / 2 - 40, DrawRect.y, 80, EditorGUIUtility.singleLineHeight), StringFormatting.FormatTime(SFXObjectInstance.SFXLayers[Index].SFX.length), CenteredMiniLabel);
            }

            //Displays basic properties if uncollapsed
            if (!SFXObjectInstance.CollapseEditions)
            {
                DrawProperty(NameProperty, "Layer Name", "(Optional) The name of the Edition.");
                DrawPropertyStyled(SFXProperty, "SFX", "The audio to be played in this SFXLayer.", (SFXObjectInstance.SFXLayers[Index].SFX == null ? InvalidStyle : EditorStyles.label));

                //Shows full properties if the SFX is not null
                if (SFXObjectInstance.SFXLayers[Index].SFX != null)
                {
                    //Volume properties
                    DrawProperty(RandomizeVolumeProperty, "Randomize Volume", "If the volume of the sound should be randomised.");
                    if (SFXObjectInstance.SFXLayers[Index].RandomizeVolume)
                    {
                        DrawMinMaxSlider(MinVolumeProperty, MaxVolumeProperty, "Volume", "The range of values that the volume of the SFXLayer can be.", 0, 1);
                        EditorGUI.LabelField(new Rect(DrawRect.x + 82, PropertyIndex * Spacing + DrawRect.y, LabelWidth - 82, EditorGUIUtility.singleLineHeight), NumberManipulation.DecimalPlaces(SFXObjectInstance.SFXLayers[Index].MinVolume, 2).ToString() + " - " + NumberManipulation.DecimalPlaces(SFXObjectInstance.SFXLayers[Index].MaxVolume, 2).ToString(), CenteredMiniLabel);
                    }
                    else { DrawSlider(FixedVolumeProperty, "Volume", "The volume of the SFXLayer.", 0, 1); }

                    //Pitch properties
                    DrawProperty(RandomizePitchProperty, "Randomize Pitch", "If the pitch of the sound should be randomised.");
                    if (SFXObjectInstance.SFXLayers[Index].RandomizePitch)
                    {
                        DrawMinMaxSlider(MinPitchProperty, MaxPitchProperty, "Pitch", "The range of values that the pitch of the SFXLayer can be.", 0, 3);
                        EditorGUI.LabelField(new Rect(DrawRect.x + 82, PropertyIndex * Spacing + DrawRect.y, LabelWidth - 82, EditorGUIUtility.singleLineHeight), NumberManipulation.DecimalPlaces(SFXObjectInstance.SFXLayers[Index].MinPitch, 2).ToString() + " - " + NumberManipulation.DecimalPlaces(SFXObjectInstance.SFXLayers[Index].MaxPitch, 2).ToString(), CenteredMiniLabel);
                    }
                    else { DrawSlider(FixedPitchProperty, "Pitch", "The pitch of the SFXLayer.", 0, 3); }

                    //Delay properties
                    DrawProperty(DelayProperty, "Delay", "If there should be a delay before playing this SFXLayer.");
                    if (SFXObjectInstance.SFXLayers[Index].DelaySFX)
                    {
                        DrawProperty(RandomizeDelayProperty, "Randomize Delay", "If the delay time should be randomized.");
                        if (SFXObjectInstance.SFXLayers[Index].RandomizeDelay)
                        {
                            DrawProperty(MinDelayProperty, "Minimum Delay", "Minimum duration of the delay.");
                            DrawProperty(MaxDelayProperty, "Maximum Delay", "Maximum duration of the delay.");
                            MinDelayProperty.floatValue = Mathf.Max(0, MinDelayProperty.floatValue);
                            MaxDelayProperty.floatValue = Mathf.Max(SFXObjectInstance.SFXLayers[Index].MinDelayTime, MaxDelayProperty.floatValue);
                        }
                        else
                        {
                            DrawProperty(FixedDelayProperty, "Delay Duration", "Duration of the delay.");
                            FixedDelayProperty.floatValue = Mathf.Max(0, FixedDelayProperty.floatValue);
                        }
                    }

                    //Other properties
                    DrawProperty(MuteProperty, "Mute", "Mutes the sound.");
                    DrawProperty(BypassEffectsProperty, "Bypass Effects", "Bypass any applied effects on the SFXLayer.");
                    DrawProperty(BypassReverbProperty, "Bypass Reverb Zones", "Bypasses any reverb zones");

                    DrawIntSlider(PriorityProperty, "Priority", "The priority of the SFXLayer.", 0, 256);
                    EditorGUI.LabelField(new Rect(DrawRect.x + LabelWidth, PropertyIndex * Spacing + DrawRect.y + 8, 30, EditorGUIUtility.singleLineHeight), "High", LeftMiniLabel);
                    EditorGUI.LabelField(new Rect(DrawRect.x + DrawRect.width - FieldPadding - 95, PropertyIndex * Spacing + DrawRect.y + 8, 40, EditorGUIUtility.singleLineHeight), "Low", RightMiniLabel);

                    DrawSlider(StereoProperty, "Stero Pan", "Pans a playing sound in a stereo way (left or right). This only applies to sounds that are Mono or Stereo.", -1, 1);
                    EditorGUI.LabelField(new Rect(DrawRect.x + LabelWidth, PropertyIndex * Spacing + DrawRect.y + 8, 25, EditorGUIUtility.singleLineHeight), "Left", LeftMiniLabel);
                    EditorGUI.LabelField(new Rect(DrawRect.x + DrawRect.width - FieldPadding - 95, PropertyIndex * Spacing + DrawRect.y + 8, 40, EditorGUIUtility.singleLineHeight), "Right", RightMiniLabel);

                    DrawSlider(SpatialProperty, "Spatial Blend", "Sets how much this SFXLayer is affected by 3D spatialisation calculations. 0.0 makes the sound full 2D, 1.0 makes it full 3D.", 0, 1);
                    EditorGUI.LabelField(new Rect(DrawRect.x + LabelWidth, PropertyIndex * Spacing + DrawRect.y + 8, 25, EditorGUIUtility.singleLineHeight), "2D", LeftMiniLabel);
                    EditorGUI.LabelField(new Rect(DrawRect.x + DrawRect.width - FieldPadding - 95, PropertyIndex * Spacing + DrawRect.y + 8, 40, EditorGUIUtility.singleLineHeight), "3D", RightMiniLabel);

                    DrawSlider(ReverbProperty, "Reverb Zone Mix", "The amount by which the signal from the AudioSource will be mixed into the global reverb associated with the Reverb Zones.", 0, 1.1f);
                    DrawProperty(MixerProperty, "Mixer Override", "Overrides the audio mixer of the SFXManager for this SFXLayer if set.");
                    DrawProperty(MinDistanceProperty, "Min Distance", "Within the Min distance the AudioSource will cease to grow louder in volume.");
                    DrawProperty(MaxDistanceProperty, "Max Distance", "(Logarithmic rolloff) MaxDistance is the distance a sound stops attenuating at.");
                    DrawProperty(RolloffModeProperty, "Audio Roloff", "How the AudioSource attenuates over distance.");
                }
                else
                {
                    //Displays error if no SFX is selected
                    EditorGUI.HelpBox(new Rect(DrawRect.x, (PropertyIndex + 1) * Spacing + DrawRect.y + 2f, DrawRect.width - FieldPadding, 30), "SFX required. Please assign an AudioClip to this SFXLayer.", MessageType.Error);
                }
            }
        }

        /// <summary>Determines the required height for this SFXLayer's sub-inspector.</summary>
        /// <param name="Index">The index of this SFXLayer in the SFXObject.</param>
        /// <returns>The determined height for this sub-inspector.</returns>
        private float DetermineSFXLayerInspectorHeight(int Index)
        {
            //Calculates commonly used spacings
            float Spacing = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            //Base height
            float Height = Spacing + 2.5f;
            if (!SFXObjectInstance.CollapseEditions)
            {
                //Uncollapsed height addition
                Height += Spacing * 2 + 2.5f;
                if (SFXObjectInstance.SFXLayers[Index].SFX != null)
                {
                    //Extra height for properties displayed
                    Height += Spacing * 16;
                    if (SFXObjectInstance.SFXLayers[Index].DelaySFX)
                    {
                        Height += 2 * Spacing;
                        if (SFXObjectInstance.SFXLayers[Index].RandomizeDelay) { Height += Spacing; }
                    }
                }

                //Error height
                else { Height += 35f; }
            }

            //Returns calculated height
            return Height;
        }

        /// <summary>Appends a new SFXLayer to the SFXObject.</summary>
        /// <param name="Target">The ReorderableList associated with this SFXObject.</param>
        private void AppendNewSFXLayer(ReorderableList Target)
        {
            //Creates new SFXLayer
            int Index = Target.serializedProperty.arraySize;
            Target.serializedProperty.arraySize++;
            Target.index = Index;
            SerializedProperty NewSFXLayer = Target.serializedProperty.GetArrayElementAtIndex(Index);

            //Resets properties
            NewSFXLayer.FindPropertyRelative("LayerName").stringValue = "";
            NewSFXLayer.FindPropertyRelative("SFX").objectReferenceValue = null;
            NewSFXLayer.FindPropertyRelative("RandomizeVolume").boolValue = false;
            NewSFXLayer.FindPropertyRelative("FixedVolume").floatValue = 1;
            NewSFXLayer.FindPropertyRelative("MaxVolume").floatValue = 1;
            NewSFXLayer.FindPropertyRelative("MinVolume").floatValue = 0.5f;
            NewSFXLayer.FindPropertyRelative("RandomizePitch").boolValue = false;
            NewSFXLayer.FindPropertyRelative("FixedPitch").floatValue = 1;
            NewSFXLayer.FindPropertyRelative("MaxPitch").floatValue = 1.5f;
            NewSFXLayer.FindPropertyRelative("MinPitch").floatValue = 0.5f;
            NewSFXLayer.FindPropertyRelative("DelaySFX").boolValue = false;
            NewSFXLayer.FindPropertyRelative("RandomizeDelay").boolValue = false;
            NewSFXLayer.FindPropertyRelative("FixedDelayTime").floatValue = 0;
            NewSFXLayer.FindPropertyRelative("MaxDelayTime").floatValue = 0;
            NewSFXLayer.FindPropertyRelative("MinDelayTime").floatValue = 0;
            NewSFXLayer.FindPropertyRelative("Priority").intValue = 128;
        }
    }
}
