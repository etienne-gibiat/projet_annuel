using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using PGSauce.AudioManagement.External;
using PGSauce.AudioManagement.External.Coroutines.Music;
using PGSauce.AudioManagement.External.EventBinding;

/// <summary>The MusicManager handles all the music in the scene. The component should only be used once in the scene, and all of its functionality can be accessed via MusicManager.Main</summary>
public class MusicManager : SoundManager
{
    #region Initialization
#if UNITY_EDITOR
    public bool CollapseLibrary;
#endif

    /// <summary>The type of audio this sound manager handles.</summary>
    public override AudioObject.SoundType ManagerType { get { return AudioObject.SoundType.Music; } }

    /// <summary>Library of Tracks stored on the MusicManager so that Tracks can be started by name without needing a reference to the Track.</summary>
    [Tooltip("Library of Tracks stored on the MusicManager so that Tracks can be started by name without needing a reference to the Track.")]
    public Track[] TrackLibrary = new Track[0];

    /// <summary>Static access point to the MusicManager object active in the scene.</summary>
    [HideInInspector]
    public static MusicManager Main;

    /// <summary>Public callback hook for when the Track changes.</summary>
    public static Action<Track> OnTrackChangeCallback;

    /// <summary>Public callback hook for when the Track ends.</summary>
    public static Action<Track, bool> OnTrackStopCallback;

    /// <summary>The Track currently being played by the MusicManager.</summary>
    private Track TargetTrack;

    /// <summary>The current Coroutine for delaying the start of a Track.</summary>
    private Coroutine DelayJob;

    /// <summary>The current Coroutine for ending a OneShot Track.</summary>
    private Coroutine EndJob;

    //Initializes
    protected override void Awake()
    {
        base.Awake();
        Parent.name = "Music Objects";

        if (Main == null)
        {
            Main = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
#endregion

    #region LibraryPlayback
    /// <summary>Finds the desired Track in the MusicManager's Library.</summary>
    /// <param name="TrackName">The name of the Track in the TrackLibrary.</param>
    /// <returns>The retrieved Track.</returns>
    private Track FindTrackInLibrary(string TrackName)
    {
        Track DesiredTrack = TrackLibrary.FirstOrDefault((Track x) => x.Name == TrackName);
        if (DesiredTrack == null) { Debug.LogError("No Track named '" + TrackName + "' was found in the TrackLibrary of the MusicManager."); }
        return DesiredTrack;
    }

    [BindableMethod]
    /// <summary>Plays the desired Track from the TrackLibrary on the MusicManager, stopping any other active Tracks, with a crossfade effect if the desired Track has a non-zero fade duation on the main Edition.</summary>
    /// <param name="TrackName">The name of the Track in the TrackLibrary.</param>
    /// <returns>The Track from the TrackLibrary that will be played.</returns>
    public Track PlayFromLibrary(string TrackName)
    {
        Track DesiredTrack = FindTrackInLibrary(TrackName);
        if (DesiredTrack != null) { Play(DesiredTrack); }
        return DesiredTrack;
    }

    [BindableMethod]
    /// <summary>Plays the desired Track from the TrackLibrary on the MusicManager, stopping any other active Tracks, with a crossfade effect if the desired Track has a non-zero fade duation on the main Edition.</summary>
    /// <param name="TrackName">The name of the Track in the TrackLibrary.</param>
    /// <param name="Delay">The duration of the pause in seconds before the Track is played.</param>
    /// <param name="IsOneShot">If the Track will be played as a one shot, with no looping.</param>
    /// <returns>The Track from the TrackLibrary that will be played.</returns>
    public Track PlayFromLibrary(string TrackName, float Delay, bool IsOneShot = false)
    {
        Track DesiredTrack = FindTrackInLibrary(TrackName);
        if (DesiredTrack != null) { Play(DesiredTrack, Delay, IsOneShot); }
        return DesiredTrack;
    }

    [BindableMethod]
    /// <summary>Plays the desired Track from the TrackLibrary on the MusicManager, stopping any other active Tracks, with a crossfade effect if the desired Track has a non-zero fade duation on the main Edition.</summary>
    /// <param name="TrackName">The name of the Track in the TrackLibrary.</param>
    /// <param name="Delay">The duration of the pause in seconds before the Track is played.</param>
    /// <param name="FadeDuration">The length of the crossfade effect.</param>
    /// <param name="IsOneShot">If the Track will be played as a one shot, with no looping.</param>
    /// <returns>The Track from the TrackLibrary that will be played.</returns>
    public Track PlayFromLibrary(string TrackName, float Delay, float FadeDuration, bool IsOneShot = false)
    {
        Track DesiredTrack = FindTrackInLibrary(TrackName);
        if (DesiredTrack != null) { Play(DesiredTrack, Delay, FadeDuration, IsOneShot); }
        return DesiredTrack;
    }
#endregion

    #region Playback
    /// <summary>Determines if a Track is currently being played.</summary>
    /// <returns>If a Track is currently being played.</returns>
    public bool IsPlaying() { return TargetTrack != null; }

    /// <summary>Determines if the Track is currently being played.</summary>
    /// <param name="DesiredTrack">The Track to test.</param>
    /// <returns>If the Track is currently being played.</returns>
    public bool IsPlaying(Track DesiredTrack) { return DesiredTrack == TargetTrack; }

    /// <summary>Determines if the Track is currently being played.</summary>
    /// <param name="TrackName">The name of the Track to test.</param>
    /// <returns>If the Track is currently being played.</returns>
    public bool IsPlaying(string TrackName) { return TargetTrack != null && TrackName == TargetTrack.Name; }

    [BindableMethod]
    /// <summary>Plays the desired Track, stopping any other active Tracks, with a crossfade effect if the desired Track has a non-zero fade duation on the main Edition.</summary>
    /// <param name="DesiredTrack">The desired Track to be played.</param>
    public void Play(Track DesiredTrack) { Play(DesiredTrack, 0); }

    [BindableMethod]
    /// <summary>Plays the desired Track, stopping any other active Tracks, with a crossfade effect if the desired Track has a non-zero fade duation on the main Edition.</summary>
    /// <param name="DesiredTrack">The desired Track to be played.</param>
    /// <param name="Delay">The duration of the pause in seconds before the Track is played.</param>
    /// <param name="IsOneShot">If the Track will be played as a one shot, with no looping.</param>
    public void Play(Track DesiredTrack, float Delay, bool IsOneShot = false) { Play(DesiredTrack, Delay, DesiredTrack.Intro == null ? DesiredTrack.Editions[0].FadeLength : 0, IsOneShot); }

    [BindableMethod]
    /// <summary>Plays the desired Track, stopping any other active Tracks, with a crossfade effect.</summary>
    /// <param name="DesiredTrack">The desired Track to be played.</param>
    /// <param name="Delay">The duration of the pause in seconds before the Track is played.</param>
    /// <param name="FadeDuration">The length of the crossfade effect.</param>
    /// <param name="IsOneShot">If the Track will be played as a one shot, with no looping.</param>
    public void Play(Track DesiredTrack, float Delay, float FadeDuration, bool IsOneShot = false)
    {
        if (DesiredTrack.Editions.Length == 0) { Debug.LogError("The Track could not be played as it contained 0 Editions of the Track. A minimum of one is required for it to be played."); }
        else
        {
            //Stops any sequenced jobs
            StopTrackDelay();
            StopTrackEnd();

            if (TargetTrack != DesiredTrack)
            {
                //Starts Delay
                if (Delay > 0 || Paused) { DelayJob = StartCoroutine(DelayedPlay(DesiredTrack, Delay, FadeDuration)); }
                else
                {
                    //Stops current Track
                    Stop(FadeDuration);

                    //Starts new Track
                    TargetTrack = DesiredTrack;
                    TargetTrack.PlayTrack(FadeDuration, IsOneShot);
                    if (OnTrackChangeCallback != null) { OnTrackChangeCallback(TargetTrack); }
                }
            }
        }
    }

    /// <summary>Plays the desired Track, stopping any other active Tracks, with a crossfade effect.</summary>
    /// <param name="DesiredTrack">The desired Track to be played.</param>
    /// <param name="Delay">The duration of the pause in seconds before the Track is played.</param>
    /// <param name="FadeDuration">The length of the crossfade effect.</param>
    private IEnumerator DelayedPlay(Track DesiredTrack, float Delay, float FadeDuration)
    {
        //Waits before playing Track
        yield return new WaitForUnpausedSeconds(Delay);
        Play(DesiredTrack, 0f, FadeDuration);
    }

    [BindableMethod]
    /// <summary>Stops the currently playing Track.</summary>
    public void Stop() { Stop(TargetTrack, 0f); }

    [BindableMethod]
    /// <summary>Stops the currently playing Track.</summary>
    /// <param name="AudioTrack">The desired Track to be stopped.</param>
    public void Stop(Track AudioTrack) { Stop(AudioTrack, 0f); }

    [BindableMethod]
    /// <summary>Stops the currently playing Track with a crossfade effect.</summary>
    /// <param name="FadeDuration">The duration of the crossfade effect.</param>
    public void Stop(float FadeDuration) { Stop(TargetTrack, FadeDuration); }

    [BindableMethod]
    /// <summary>Stops the desired Track with a crossfade effect if it is active.</summary>
    /// <param name="AudioTrack">The desired Track to be stopped.</param>
    /// <param name="FadeDuration">The duration of the crossfade effect.</param>
    public void Stop(Track AudioTrack, float FadeDuration) { Stop(AudioTrack, FadeDuration, false); }

    /// <summary>Stops the desired Track with a crossfade effect if it is active.</summary>
    /// <param name="AudioTrack">The desired Track to be stopped.</param>
    /// <param name="FadeDuration">The duration of the crossfade effect.</param>
    /// <param name="OneStopEnd">If this stop is to stop a one shot Track.</param>
    private void Stop(Track AudioTrack, float FadeDuration, bool OneStopEnd)
    {
        if (AudioTrack == TargetTrack && AudioTrack != null)
        {
            //Stops the Track
            if (OnTrackStopCallback != null) { OnTrackStopCallback(TargetTrack, OneStopEnd || TargetTrack.IsOneShot); }
            StopTrackDelay();
            StopTrackEnd();
            TargetTrack.StopTrack(FadeDuration > 0, FadeDuration);
            TargetTrack = null;
        }
    }

    /// <summary>Sequences the end of a one shot Track.</summary>
    /// <param name="AudioTrack">The Track attempting to initiate the end.</param>
    /// <param name="RemainingTime">How much time remains before the Track ends.</param>
    public void SequenceEnd(Track AudioTrack, float RemainingTime)
    {
        if (AudioTrack == TargetTrack && AudioTrack != null)
        {
            //Clears any current job and begins a new one
            StopTrackEnd();
            EndJob = StartCoroutine(DelayedEndTrack(RemainingTime));
        }
    }

    /// <summary>Sequences the end of a one shot AudioObject.</summary>
    /// <param name="Audio">The AudioObject attempting to initiate the end.</param>
    /// <param name="RemainingTime">How much time remains before the AudioObject ends.</param>
    public void SequenceEnd(AudioObject Audio, float RemainingTime) { StartCoroutine(DelayedEndAudio(Audio, RemainingTime)); }

    /// <summary>Delays the end of a one shot Track.</summary>
    /// <param name="Duration">How much time remains before the Track ends.</param>
    private IEnumerator DelayedEndTrack(float Duration)
    {
        //Waits and ends the Track
        yield return new WaitForUnpausedSeconds(Duration);
        Stop(TargetTrack, 0, true);
    }

    /// <summary>Delays the end of a one shot AudioObject.</summary>
    /// <param name="Audio">The AudioObject attempting to initiate the end.</param>
    /// <param name="RemainingTime">How much time remains before the AudioObject ends.</param>
    private IEnumerator DelayedEndAudio(AudioObject Audio, float Duration)
    {
        //Waits and ends the AudioObject
        yield return new WaitForUnpausedSeconds(Duration);
        StopAudio(Audio);
    }

    [BindableMethod]
    /// <summary>Restarts the currently playing Track.</summary>
    public void Restart() { Restart(TargetTrack, 0f); }

    [BindableMethod]
    /// <summary>Restarts the desired Track if it is active.</summary>
    /// <param name="AudioTrack">The desired Track to be restarted.</param>
    public void Restart(Track AudioTrack) { Restart(AudioTrack, 0f); }

    [BindableMethod]
    /// <summary>Restarts the currently playing Track with a crossfade effect.</summary>
    /// <param name="FadeDuration">The duration of the crossfade effect.</param>
    public void Restart(float FadeDuration) { Restart(TargetTrack, FadeDuration); }

    [BindableMethod]
    /// <summary>Restarts the desired Track with a crossfade effect if it is active.</summary>
    /// <param name="AudioTrack">The desired Track to be restarted.</param>
    /// <param name="FadeDuration">The duration of the crossfade effect.</param>
    public void Restart(Track AudioTrack, float FadeDuration)
    {
        if (AudioTrack == TargetTrack && AudioTrack != null)
        {
            //Restarts the Track
            Stop(AudioTrack, FadeDuration);
            Play(AudioTrack, FadeDuration);
        }
    }

    /// <summary>Starts an AudioClip in the scene.</summary>
    /// <param name="Audio">The desired AudioClip to be played.</param>
    /// <returns>The AudioObject containing the audio.</returns>
    public AudioObject StartAudio(AudioClip Audio) { return StartAudio(Audio, true); }

    /// <summary>Stops the current Coroutine for delaying a Track.</summary>
    private void StopTrackDelay()
    {
        if (DelayJob != null)
        {
            StopCoroutine(DelayJob);
            DelayJob = null;
        }
    }

    /// <summary>Stops the current Coroutine for ending a Track.</summary>
    private void StopTrackEnd()
    {
        if (EndJob != null)
        {
            StopCoroutine(EndJob);
            EndJob = null;
        }
    }
    #endregion

    #region EditionSwitching

    [BindableMethod]
    /// <summary>Switches the Edition of the currently playing Track.</summary>
    /// <param name="EditionIndex">The index of the desired Edition in the current Track's array of Editions.</param>
    /// <param name="Delay">The duration of the pause in seconds before the Edition is switched.</param>
    public void SwitchEdition(int EditionIndex, float Delay)
    {
        if (TargetTrack != null)
        {
            //Switches Edition if it is available
            if (EditionIndex >= 0 && EditionIndex < TargetTrack.Editions.Length) { SwitchEdition(TargetTrack.Editions[EditionIndex], Delay); }
            else { Debug.LogError("The desired Edition could not be started as the current Track only contains " + TargetTrack.Editions.Length.ToString() + " Edition" + (TargetTrack.Editions.Length == 1 ? "." : "s.")); }
        }
    }

    [BindableMethod]
    /// <summary>Switches the Edition of the currently playing Track.</summary>
    /// <param name="EditionIndex">The index of the desired Edition in the current Track's array of Editions.</param>
    public void SwitchEdition(int EditionIndex) { SwitchEdition(EditionIndex, 0); }

    [BindableMethod]
    /// <summary>Switches the Edition of the currently playing Track after a period of time.</summary>
    /// <param name="EditionIndex">The index of the desired Edition in the current Track's array of Editions.</param>
    /// <param name="Delay">The duration of the pause in seconds before the Edition is switched.</param>
    /// <param name="FadeOverride">The duration of the crossfade effect.</param>
    public void SwitchEdition(int EditionIndex, float Delay, float FadeOverride)
    {
        if (TargetTrack != null)
        {
            //Switches Edition if it is available
            if (EditionIndex >= 0 && EditionIndex < TargetTrack.Editions.Length) { SwitchEdition(TargetTrack.Editions[EditionIndex], Delay, FadeOverride); }
            else { Debug.LogError("The desired Edition could not be started as the current Track only contains " + TargetTrack.Editions.Length.ToString() + " Edition" + (TargetTrack.Editions.Length == 1 ? "." : "s.")); }
        }
    }

    [BindableMethod]
    /// <summary>Switches the Edition of the currently playing Track.</summary>
    /// <param name="EditionName">The name of the desired Edition in the current Track's array of Editions.</param>
    /// <param name="Delay">The duration of the pause in seconds before the Edition is switched.</param>
    public void SwitchEdition(string EditionName, float Delay = 0)
    {
        if (TargetTrack != null)
        {
            //Switches Edition if it is available
            Edition DesiredEdition = TargetTrack.Editions.First((Edition x) => x.Name == EditionName);
            if (!DesiredEdition.IsNull) { SwitchEdition(DesiredEdition, Delay); }
            else { Debug.LogError("The desired Edition could not be started as the current Track did not contain an Edition named " + EditionName + "."); }
        }
    }

    [BindableMethod]
    /// <summary>Switches the Edition of the currently playing Track after a period of time.</summary>
    /// <param name="EditionName">The name of the desired Edition in the current Track's array of Editions.</param>
    /// <param name="Delay">The duration of the pause in seconds before the Edition is switched.</param>
    /// <param name="FadeOverride">The duration of the crossfade effect.</param>
    public void SwitchEdition(string EditionName, float Delay, float FadeOverride)
    {
        if (TargetTrack != null)
        {
            //Switches Edition if it is available
            Edition DesiredEdition = TargetTrack.Editions.First((Edition x) => x.Name == EditionName);
            if (!DesiredEdition.IsNull) { SwitchEdition(DesiredEdition, Delay, FadeOverride); }
            else { Debug.LogError("The desired Edition could not be started as the current Track did not contain an Edition named " + EditionName + "."); }
        }
    }
		
    /// <summary>Switches the Edition of the currently playing Track.</summary>
    /// <param name="DesiredEdition">The desired Edition to switch to.</param>
    public void SwitchEdition(Edition DesiredEdition) { SwitchEdition(DesiredEdition, 0f, DesiredEdition.FadeLength); }

    /// <summary>Switches the Edition of the currently playing Track.</summary>
    /// <param name="DesiredEdition">The desired Edition to switch to.</param>
    /// <param name="Delay">The duration of the pause in seconds before the Edition is switched.</param>
    public void SwitchEdition(Edition DesiredEdition, float Delay) { SwitchEdition(DesiredEdition, Delay, DesiredEdition.FadeLength); }

    /// <summary>Switches the Edition of the currently playing Track after a period of time.</summary>
    /// <param name="DesiredEdition">The desired Edition to switch to.</param>
    /// <param name="Delay">The duration of the pause in seconds before the Edition is switched.</param>
    /// <param name="FadeOverride">The duration of the crossfade effect.</param>
    public void SwitchEdition(Edition DesiredEdition, float Delay, float FadeOverride)
    {
        if (TargetTrack != null)
        {
            //Stops any sequenced delays
            TargetTrack.StopDelay();

            //Switches Edition
            if (Delay > 0f || Paused) { TargetTrack.DelayJob = StartCoroutine(DelayedSwitchEdition(DesiredEdition, Delay, FadeOverride)); }
            else { TargetTrack.SwitchEdition(DesiredEdition, FadeOverride); }
        }
    }

    /// <summary>Switches the Edition of the currently playing Track after a period of time.</summary>
    /// <param name="DesiredEdition">The desired Edition to switch to.</param>
    /// <param name="Delay">The duration of the pause in seconds before the Edition is switched.</param>
    /// <param name="FadeOverride">The duration of the crossfade effect.</param>
    private IEnumerator DelayedSwitchEdition(Edition DesiredEdition, float Delay, float FadeOverride)
    {
        //Waits before switching Edition.
        yield return new WaitForUnpausedSeconds(Delay);
        TargetTrack.SwitchEdition(DesiredEdition, FadeOverride);
    }

    /// <summary>Switches the Edition of the currently playing Track.</summary>
    /// <param name="EditionIndex">The index of the desired Edition in the current Track's array of Editions.</param>
    /// <param name="DelayDelegate">The delegate that will return false until it is time for the edition to switch.</param>
    public void SwitchEdition(int EditionIndex, Func<bool> DelayDelegate)
    {
        if (TargetTrack != null)
        {
            //Switches Edition if it is available
            if (EditionIndex >= 0 && EditionIndex < TargetTrack.Editions.Length) { SwitchEdition(TargetTrack.Editions[EditionIndex], DelayDelegate); }
            else { Debug.LogError("The desired Edition could not be started as the current Track only contains " + TargetTrack.Editions.Length.ToString() + " Edition" + (TargetTrack.Editions.Length == 1 ? "." : "s.")); }
        }
    }

    /// <summary>Switches the Edition of the currently playing Track after a period of time.</summary>
    /// <param name="EditionIndex">The index of the desired Edition in the current Track's array of Editions.</param>
    /// <param name="DelayDelegate">The delegate that will return false until it is time for the edition to switch.</param>
    /// <param name="FadeOverride">The duration of the crossfade effect.</param>
    public void SwitchEdition(int EditionIndex, Func<bool> DelayDelegate, float FadeOverride)
    {
        if (TargetTrack != null)
        {
            //Switches Edition if it is available
            if (EditionIndex >= 0 && EditionIndex < TargetTrack.Editions.Length) { SwitchEdition(TargetTrack.Editions[EditionIndex], DelayDelegate, FadeOverride); }
            else { Debug.LogError("The desired Edition could not be started as the current Track only contains " + TargetTrack.Editions.Length.ToString() + " Edition" + (TargetTrack.Editions.Length == 1 ? "." : "s.")); }
        }
    }

    /// <summary>Switches the Edition of the currently playing Track.</summary>
    /// <param name="EditionName">The name of the desired Edition in the current Track's array of Editions.</param>
    /// <param name="DelayDelegate">The delegate that will return false until it is time for the edition to switch.</param>
    public void SwitchEdition(string EditionName, Func<bool> DelayDelegate)
    {
        if (TargetTrack != null)
        {
            //Switches Edition if it is available
            Edition DesiredEdition = TargetTrack.Editions.First((Edition x) => x.Name == EditionName);
            if (!DesiredEdition.IsNull) { SwitchEdition(DesiredEdition, DelayDelegate); }
            else { Debug.LogError("The desired Edition could not be started as the current Track did not contain an Edition named " + EditionName + "."); }
        }
    }

    /// <summary>Switches the Edition of the currently playing Track after a period of time.</summary>
    /// <param name="EditionName">The name of the desired Edition in the current Track's array of Editions.</param>
    /// <param name="DelayDelegate">The delegate that will return false until it is time for the edition to switch.</param>
    /// <param name="FadeOverride">The duration of the crossfade effect.</param>
    public void SwitchEdition(string EditionName, Func<bool> DelayDelegate, float FadeOverride)
    {
        if (TargetTrack != null)
        {
            //Switches Edition if it is available
            Edition DesiredEdition = TargetTrack.Editions.First((Edition x) => x.Name == EditionName);
            if (!DesiredEdition.IsNull) { SwitchEdition(DesiredEdition, DelayDelegate, FadeOverride); }
            else { Debug.LogError("The desired Edition could not be started as the current Track did not contain an Edition named " + EditionName + "."); }
        }
    }

    /// <summary>Switches the Edition of the currently playing Track.</summary>
    /// <param name="DesiredEdition">The desired Edition to switch to.</param>
    /// <param name="DelayDelegate">The delegate that will return false until it is time for the edition to switch.</param>
    private void SwitchEdition(Edition DesiredEdition, Func<bool> DelayDelegate) { SwitchEdition(DesiredEdition, DelayDelegate, DesiredEdition.FadeLength); }

    /// <summary>Switches the Edition of the currently playing Track after a period of time.</summary>
    /// <param name="DesiredEdition">The desired Edition to switch to.</param>
    /// <param name="DelayDelegate">The delegate that will return false until it is time for the edition to switch.</param>
    /// <param name="FadeOverride">The duration of the crossfade effect.</param>
    private void SwitchEdition(Edition DesiredEdition, Func<bool> DelayDelegate, float FadeOverride)
    {
        if (TargetTrack != null)
        {
            //Stops any sequenced delays
            TargetTrack.StopDelay();

            //Switches Edition
            TargetTrack.DelayJob = StartCoroutine(DelayedSwitchEdition(DesiredEdition, DelayDelegate, FadeOverride));
        }
    }

    /// <summary>Switches the Edition of the currently playing Track after a period of time.</summary>
    /// <param name="DesiredEdition">The desired Edition to switch to.</param>
    /// <param name="DelayDelegate">The delegate that will return false until it is time for the edition to switch.</param>
    /// <param name="FadeOverride">The duration of the crossfade effect.</param>
    private IEnumerator DelayedSwitchEdition(Edition DesiredEdition, Func<bool> DelayDelegate, float FadeOverride)
    {
        //Waits before switching Edition.
        yield return new WaitUntil(DelayDelegate);
        TargetTrack.SwitchEdition(DesiredEdition, FadeOverride);
    }
#endregion

    #region AudioFading
    /// <summary>Fades the volume of an AudioObject over time to the desired volume.</summary>
    /// <param name="Audio">The AudioObject to fade the volume on.</param>
    /// <param name="FadeDuration">The duration of the fade effect.</param>
    /// <param name="Target">The desired volume to fade the AudioObject's volume to.</param>
    public void FadeAudio(AudioObject Audio, float FadeDuration, float Target)
    {
        //Stops any current fade on the desired AudioObject
        Audio.StopFade();
        if (FadeDuration > 0)
        {
            //Starts fade effect
            if (Audio.volume < Target) { Audio.FadeJob = StartCoroutine(FadeUpAudio(Audio, FadeDuration, Target)); }
            else { Audio.FadeJob = StartCoroutine(FadeDownAudio(Audio, FadeDuration, Target)); }
        }

        else { Audio.volume = Target; }
    }

    /// <summary>Fades down the volume of an AudioObject over time to the desired volume.</summary>
    /// <param name="Audio">The AudioObject to fade the volume on.</param>
    /// <param name="FadeDuration">The duration of the fade effect.</param>
    /// <param name="Target">The desired volume to fade the AudioObject's volume to.</param>
    private IEnumerator FadeDownAudio(AudioObject Audio, float FadeDuration, float Target)
    {
        //Calculates rate of change independant of current volume
        float Rate = (Audio.volume - Target) / FadeDuration;

        //Continues until fade is complete
        while (Audio.volume > Target)
        {
            //Gradually decreases volume
            Audio.volume = Mathf.MoveTowards(Audio.volume, Target, Rate * Time.unscaledDeltaTime);
            yield return new WaitForNextUnpausedFrame();
        }

        //Stops AudioObject if it has been faded out completely
        if (Audio.volume == 0) { StopAudio(Audio); }
        yield break;
    }

    /// <summary>Fades up the volume of an AudioObject over time to the desired volume.</summary>
    /// <param name="Audio">The AudioObject to fade the volume on.</param>
    /// <param name="FadeDuration">The duration of the fade effect.</param>
    /// <param name="Target">The desired volume to fade the AudioObject's volume to.</param>
    private IEnumerator FadeUpAudio(AudioObject Audio, float FadeDuration, float Target)
    {
        //Calculates rate of change independant of current volume
        float Rate = (Target - Audio.volume) / FadeDuration;

        //Continues until fade is complete
        while (Audio.volume < Target)
        {
            //Gradually increases volume
            Audio.volume = Mathf.MoveTowards(Audio.volume, Target, Rate * Time.unscaledDeltaTime);
            yield return new WaitForNextUnpausedFrame();
        }
        yield break;
    }

    /// <summary>Sets the global volume of the SoundManager with a fade effect.</summary>
    /// <param name="Volume">The desired volume.</param>
    /// <param name="FadeDuration">The duration of the fade effect.</param>
    protected override IEnumerator VolumeFade(float Volume, float FadeDuration)
    {
        //Calculates rate of change
        float Rate = Mathf.Abs(Volume - this.Volume) / FadeDuration;

        //Continues until fade is complete
        while (this.Volume != Volume)
        {
            //Changes volume gradually
            MasterVolume = Mathf.MoveTowards(this.Volume, Volume, Rate * Time.unscaledDeltaTime);
            yield return new WaitForNextUnpausedFrame();
        }
        yield break;
    }
#endregion
}

/// <summary>Custom coroutine instructions for the MusicManager.</summary>
namespace PGSauce.AudioManagement.External.Coroutines.Music
{
    /// <summary>Waits for a set duration of time, independant of the current timescale. Time when the MusicManager is paused will not be counted.</summary>
    public sealed class WaitForUnpausedSeconds : CustomYieldInstruction
    {
        private float TimeRemaining;

        /// <summary>Waits for a set duration of time, independant of the current timescale. Time when the MusicManager is paused will not be counted.</summary>
        /// <param name="Duration">The duration of time to wait for in seconds.</param>
        public WaitForUnpausedSeconds(float Duration) { TimeRemaining = Duration; }

        public override bool keepWaiting
        {
            get
            {
                if (MusicManager.Main.Paused) { return true; }
                else
                {
                    TimeRemaining -= Time.unscaledDeltaTime;
                    return TimeRemaining > 0;
                }
            }
        }
    }

    /// <summary>Waits for the next frame in which the MusicManager is not paused.</summary>
    public sealed class WaitForNextUnpausedFrame : CustomYieldInstruction
    {
        public override bool keepWaiting { get { return MusicManager.Main.Paused; } }
    }
}
