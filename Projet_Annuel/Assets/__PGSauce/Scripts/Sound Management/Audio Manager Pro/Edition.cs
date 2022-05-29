using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGSauce.AudioManagement.External;

/// <summary>One of the editions or versions of the soundtrack contained in the Track.</summary>
[System.Serializable]
public struct Edition
{
    /// <summary>(Optional) The name of the Edition.</summary>
    [Tooltip("(Optional) The name of the Edition.")]
    public string Name;

    /// <summary>The AudioClip containing the soundtrack for this Edition.</summary>
    [Tooltip("The AudioClip containing the soundtrack for this Edition.")]
    public AudioClip Soundtrack;

    /// <summary>The volume of the soundtrack in this Edition.</summary>
    [Tooltip("The volume of the soundtrack in this Edition.")]
    public float SoundtrackVolume;

    /// <summary>(Optional) The AudioClip for the transition sound to be played when switching to this Edition of the Track.</summary>
    [Tooltip("(Optional) The AudioClip for the transition sound to be played when switching to this Edition of the Track.")]
    public AudioClip TransitionSound;

    /// <summary>The volume of the transition sound to this Edition.</summary>
    [Tooltip("The volume of the transition sound to this Edition.")]
    public float TransitionVolume;

    /// <summary>Whether to play the soundtrack from the beginning when switching to this Edition or whether to retain the playback time from the previous Edition.</summary>
    [Tooltip("Whether to play the soundtrack from the beginning when switching to this Edition or whether to retain the playback time from the previous Edition.")]
    public bool KeepPlaybackTime;

    /// <summary>The duration of the cross-fade when switching to this Edition.</summary>
    [Tooltip("The duration of the cross-fade when switching to this Edition.")]
    public float FadeLength;

    /// <summary>The AudioObject containing the soundtrack of this Edition.</summary>
    [HideInInspector]
    public AudioObject Audio;

    /// <summary>Starts the current Edition from the beginning of the soundtrack.</summary>
    /// <returns>The AudioObject containing the soundtrack from the current Edition.</returns>
    public AudioObject Play() { return Play(0); }
    /// <summary>Starts the current Edition from a specified playback time.</summary>
    /// <param name="PlayBackTime">The playback time in seconds to start the soundtrack from.</param>
    /// <returns>The AudioObject containing the soundtrack from the current Edition.</returns>
    public AudioObject Play(float PlayBackTime)
    {
        //Plays the soundtrack
        Audio = MusicManager.Main.StartAudio(Soundtrack);
        Audio.Source.time = PlayBackTime;
        Audio.SourceVolume = SoundtrackVolume;
        if (FadeLength > 0) { Audio.volume = 0; MusicManager.Main.FadeAudio(Audio, FadeLength, 1f); }

        return Audio;
    }

    /// <summary>Plays the transition sound if available.</summary>
    /// <returns>The AudioObject containing the transition sound (null if the Edition does not contain one).</returns>
    public AudioObject PlayTransition()
    {
        if (TransitionSound != null)
        {
            AudioObject TransitionAudio = MusicManager.Main.StartAudio(TransitionSound, false);
            MusicManager.Main.SequenceEnd(TransitionAudio, TransitionAudio.length);
            TransitionAudio.SourceVolume = TransitionVolume;
            return TransitionAudio;
        }
        else { return null; }
    }

    /// <summary>If the Edition is the equivalent of a null (unplayable).</summary>
    public bool IsNull { get { return Soundtrack == null; } }

    public static bool operator ==(Edition a, Edition b)
    {
        return a.Soundtrack == b.Soundtrack
            && a.TransitionSound == b.TransitionSound
            && a.Name == b.Name;
    }
    public override bool Equals(object o) { return o.GetType() == typeof(Edition) ? this == (Edition)o : false; }
    public override int GetHashCode() { return base.GetHashCode(); }
    public static bool operator !=(Edition a, Edition b) { return !(a == b); }
}