using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PGSauce.AudioManagement.External
{
    /// <summary>A full music track containing the various Editions and an optional intro.</summary>
    [CreateAssetMenu(fileName = "Track", menuName = "AudioManager/Track", order = 1)]
    public class Track : ScriptableObject
    {
#if UNITY_EDITOR
        public bool CollapseEditions;
#endif

        /// <summary>(Optional) The name of the Track.</summary>
        [Tooltip("(Optional) The name of the Track.")]
        public string Name;

        /// <summary>(Optional) The intro audio to be played before looping the main Edition.</summary>
        [Tooltip("(Optional) The intro audio to be played before looping the main Edition.")]
        public AudioClip Intro;

        /// <summary>The volume of the intro audio.</summary>
        [Tooltip("The volume of the intro audio.")]
        public float IntroVolume = 1f;

        /// <summary>Duration of the overlap between the end of the intro and the start of the first Edition.</summary>
        [Tooltip("Duration of the overlap between the end of the intro and the start of the first Edition.")]
        public float OverlapDuration;

        /// <summary>The various different Editions contained in this track, where the first element is the main Edition that will be auto-played (Required 1+).</summary>
        [Tooltip(
            "The various different Editions contained in this track, where the first element is the main Edition that will be auto-played (Required 1+).")]
        public Edition[] Editions = new Edition[1];

        /// <summary>The current Coroutine for delaying Edition swaps on this Track.</summary>
        [HideInInspector] public Coroutine DelayJob;

        //Private Variables
        /// <summary>The current Edition being played.</summary>
        private Edition CurrentEdition;

        /// <summary>If the Track will is being played as a one shot, with no looping. (Read Only)</summary>
        public bool IsOneShot { get; private set; }

        /// <summary>All audio objects owned by this Track that are currently active.</summary>
        private List<AudioObject> ActiveAudio = new List<AudioObject>();

        public override string ToString()
        {
            string NameString = Name;
            if (System.String.IsNullOrEmpty(Name))
            {
                NameString = name;
            }

            return NameString;
        }

        private void OnEnable()
        {
            Reset();
        }

        private void OnDisable()
        {
            Reset();
        }

        /// <summary>Resets the track to its untampered state.</summary>
        private void Reset()
        {
            CurrentEdition = default(Edition);
            IsOneShot = false;
            DelayJob = null;
        }

        /// <summary>Constructs a new empty Track.</summary>
        public Track()
        {
            Editions[0].SoundtrackVolume = 1f;
            Editions[0].TransitionVolume = 1f;
        }

        /// <summary>Stops the current Coroutine for switching Editions on this Track.</summary>
        public void StopDelay()
        {
            if (DelayJob != null)
            {
                MusicManager.Main.StopCoroutine(DelayJob);
                DelayJob = null;
            }
        }

        /// <summary>Plays the track with a crossfade effect.</summary>
        /// <param name="FadeDuration">The duration of the crossfade.</param>
        /// <param name="IsOneShot">If the Track will be played as a one shot, with no looping.</param>
        public void PlayTrack(float FadeDuration = 0f, bool IsOneShot = false)
        {
            this.IsOneShot = IsOneShot;
            if (Intro != null)
            {
                //Plays the intro
                AudioObject IntroAudio = MusicManager.Main.StartAudio(Intro, false);
                IntroAudio.SourceVolume = IntroVolume;
                IntroAudio.Source.loop = false;
                ActiveAudio.Add(IntroAudio);

                if (FadeDuration > 0)
                {
                    IntroAudio.volume = 0f;
                    MusicManager.Main.FadeAudio(IntroAudio, FadeDuration, 1f);
                }

                //Delays start of main Edition
                float Length = Intro.length;
                float Overlap = OverlapDuration;
                System.Func<bool> DelayDelegate = () =>
                {
                    if (!IntroAudio.Source.isPlaying && !MusicManager.Main.Paused)
                        return true;

                    return IntroAudio.Source.time >= Length - Overlap;
                };

                MusicManager.Main.SwitchEdition(0, DelayDelegate);
            }
            //Starts main Edition
            else
            {
                SwitchEdition(0, FadeDuration);
            }
        }

        /// <summary>Switches the Edition of the track to be played.</summary>
        /// <param name="EditionIndex">The index of the desired Edition in the Track's array of Editions.</param>
        public void SwitchEdition(int EditionIndex)
        {
            if (EditionIndex >= 0 && EditionIndex < Editions.Length)
            {
                SwitchEdition(Editions[EditionIndex], Editions[EditionIndex].FadeLength);
            }
            else
            {
                Debug.LogError("The desired Edition could not be started as the current Track only contains " +
                               Editions.Length.ToString() + " Edition" + (Editions.Length == 1 ? "." : "s."));
            }
        }

        /// <summary>Switches the Edition of the track to be played.</summary>
        /// <param name="EditionIndex">The index of the desired Edition in the Track's array of Editions.</param>
        /// <param name="FadeOverride">The duration of the crossfade effect.</param>
        public void SwitchEdition(int EditionIndex, float FadeOverride)
        {
            if (EditionIndex >= 0 && EditionIndex < Editions.Length)
            {
                SwitchEdition(Editions[EditionIndex], FadeOverride);
            }
            else
            {
                Debug.LogError("The desired Edition could not be started as the current Track only contains " +
                               Editions.Length.ToString() + " Edition" + (Editions.Length == 1 ? "." : "s."));
            }
        }

        /// <summary>Switches the Edition of the track to be played.</summary>
        /// <param name="DesiredEdition">The desired Edition to switch to.</param>
        public void SwitchEdition(Edition DesiredEdition)
        {
            SwitchEdition(DesiredEdition, DesiredEdition.FadeLength);
        }

        /// <summary>Switches the Edition of the track to be played.</summary>
        /// <param name="DesiredEdition">The desired Edition to switch to.</param>
        /// <param name="FadeOverride">The duration of the crossfade effect.</param>
        public void SwitchEdition(Edition DesiredEdition, float FadeOverride)
        {
            if (CurrentEdition != DesiredEdition)
            {
                AudioObject NewEditionAudio = null;

                //Fade Length
                float OriginalFade = DesiredEdition.FadeLength;
                DesiredEdition.FadeLength = FadeOverride;

                //If there is already an Edition playing
                if (!CurrentEdition.IsNull)
                {
                    //Plays transition
                    DesiredEdition.PlayTransition();

                    //Retains playback time
                    if (DesiredEdition.KeepPlaybackTime)
                    {
                        NewEditionAudio = DesiredEdition.Play(CurrentEdition.Audio.Source.time);
                        ActiveAudio.Add(NewEditionAudio);
                    }

                    //Stops previous Edition
                    if (DesiredEdition.FadeLength > 0)
                    {
                        MusicManager.Main.FadeAudio(CurrentEdition.Audio, DesiredEdition.FadeLength, 0f);
                        ActiveAudio.Remove(CurrentEdition.Audio);
                    }
                    else
                    {
                        Stop(CurrentEdition.Audio);
                    }
                }

                //Starts Edition without retaining playback time
                if (!DesiredEdition.KeepPlaybackTime || CurrentEdition.IsNull)
                {
                    NewEditionAudio = DesiredEdition.Play();
                    ActiveAudio.Add(NewEditionAudio);
                }

                //Sequences end if its a one shot
                if (IsOneShot && NewEditionAudio != null)
                {
                    NewEditionAudio.Source.loop = false;
                    MusicManager.Main.SequenceEnd(this, NewEditionAudio.length - NewEditionAudio.Source.time);
                }

                CurrentEdition = DesiredEdition;
                CurrentEdition.FadeLength = OriginalFade;
            }
        }

        /// <summary>Stops an AudioObject and marks it as inactive.</summary>
        /// <param name="Audio">The AudioObject to stop.</param>
        public void Stop(AudioObject Audio)
        {
            MusicManager.Main.StopAudio(Audio);
            ActiveAudio.Remove(Audio);
        }

        /// <summary>Stops the track and all the audio associated with it.</summary>
        public void StopTrack()
        {
            StopTrack(false, 1f);
        }

        /// <summary>Stops the track and all the audio associated with it.</summary>
        /// <param name="FadeDuration">The duration of the fade effect.</param>
        public void StopTrack(float FadeDuration)
        {
            StopTrack(true, FadeDuration);
        }

        /// <summary>Stops the track and all the audio associated with it.</summary>
        /// <param name="Fade">Whether to fade it out or not.</param>
        public void StopTrack(bool FadeDuration)
        {
            StopTrack(FadeDuration, 1f);
        }

        /// <summary>Stops the track and all the audio associated with it.</summary>
        /// <param name="Fade">Whether to fade it out or not.</param>
        /// <param name="FadeDuration">The duration of the fade effect.</param>
        public void StopTrack(bool Fade, float FadeDuration)
        {
            //Stops all audio
            for (int i = 0; i < ActiveAudio.Count; i++)
            {
                if (Fade)
                {
                    MusicManager.Main.FadeAudio(ActiveAudio[i], FadeDuration, 0f);
                }
                else
                {
                    MusicManager.Main.StopAudio(ActiveAudio[i]);
                }
            }

            ActiveAudio.Clear();

            //Stops any sequenced delays
            StopDelay();

            IsOneShot = false;
            CurrentEdition = default(Edition);
        }

    }
}
