using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGSauce.AudioManagement.External;
using UnityEngine.Audio;

namespace PGSauce.AudioManagement.External
{
    
/// <summary>An individual sound layer in an SFXObject.</summary>
[System.Serializable]
public struct SFXLayer
{
    /// <summary>The name of this SFXLayer.</summary>
    [Tooltip("The name of this SFXLayer.")]
    public string LayerName;


    /// <summary>The audio to be played in this SFXLayer.</summary>
    [Tooltip("The audio to be played in this SFXLayer.")]
    public AudioClip SFX;


    /// <summary>If the volume of the sound should be randomised.</summary>
    [Tooltip("If the volume of the sound should be randomised.")]
    public bool RandomizeVolume;

    /// <summary>The volume of the SFXLayer.</summary>
    [Tooltip("The volume of the SFXLayer.")]
    public float FixedVolume;

    /// <summary>The minimum volume of the SFXLayer.</summary>
    [Tooltip("The minimum volume of the SFXLayer.")]
    public float MinVolume;

    /// <summary>The maximum volume of the SFXLayer.</summary>
    [Tooltip("The maximum volume of the SFXLayer.")]
    public float MaxVolume;


    /// <summary>If the pitch of the sound should be randomised.</summary>
    [Tooltip("If the pitch of the sound should be randomised.")]
    public bool RandomizePitch;

    /// <summary>The pitch of the SFXLayer.</summary>
    [Tooltip("The pitch of the SFXLayer.")]
    public float FixedPitch;

    /// <summary>The minimum pitch of the SFXLayer.</summary>
    [Tooltip("The minimum pitch of the SFXLayer.")]
    public float MinPitch;

    /// <summary>The maximum pitch of the SFXLayer.</summary>
    [Tooltip("The maximum pitch of the SFXLayer.")]
    public float MaxPitch;


    /// <summary>If there should be a delay before playing this SFXLayer.</summary>
    [Tooltip("If there should be a delay before playing this SFXLayer.")]
    public bool DelaySFX;

    /// <summary>If the delay time should be randomized.</summary>
    [Tooltip("If the delay time should be randomized.")]
    public bool RandomizeDelay;

    /// <summary>Duration of the delay.</summary>
    [Tooltip("Duration of the delay.")]
    public float FixedDelayTime;

    /// <summary>Minimum duration of the delay.</summary>
    [Tooltip("Minimum duration of the delay.")]
    public float MinDelayTime;

    /// <summary>Maximum duration of the delay.</summary>
    [Tooltip("Maximum duration of the delay.")]
    public float MaxDelayTime;


    /// <summary>Mutes the sound.</summary>
    [Tooltip("Mutes the sound.")]
    public bool Mute;

    /// <summary>Bypass any applied effects on the SFXLayer.</summary>
    [Tooltip("Bypass any applied effects on the SFXLayer.")]
    public bool BypassEffects;

    /// <summary>Bypass any reverb zones.</summary>
    [Tooltip("Bypass any reverb zones.")]
    public bool BypassReverbZones;

    /// <summary>The priority of the SFXLayer.</summary>
    [Tooltip("The priority of the SFXLayer.")]
    public int Priority;

    /// <summary>Pans a playing sound in a stereo way (left or right). This only applies to sounds that are Mono or Stereo.</summary>
    [Tooltip("Pans a playing sound in a stereo way (left or right). This only applies to sounds that are Mono or Stereo.")]
    public float StereoPan;

    [Tooltip("Sets how much this SFXLayer is affected by 3D spatialisation calculations. 0.0 makes the sound full 2D, 1.0 makes it full 3D.")]
    /// <summary>Sets how much this SFXLayer is affected by 3D spatialisation calculations. 0.0 makes the sound full 2D, 1.0 makes it full 3D.</summary>
    public float SpatialBlend;

    [Tooltip("The amount by which the signal from the AudioSource will be mixed into the global reverb associated with the Reverb Zones.")]
    /// <summary>The amount by which the signal from the AudioSource will be mixed into the global reverb associated with the Reverb Zones.</summary>
    public float ReverbZoneMix;

    /// <summary>
    /// Overrides the audio mixer of the SFXManager for this SFXLayer if set.
    /// </summary>
    public AudioMixerGroup MixerOverride;

    /// <summary>
    /// Within the Min distance the AudioSource will cease to grow louder in volume.
    /// </summary>
    public float MinDistance;

    /// <summary>
    /// (Logarithmic rolloff) MaxDistance is the distance a sound stops attenuating at.
    /// </summary>
    public float MaxDistance;

    /// <summary>
    /// How the AudioSource attenuates over distance.
    /// </summary>
    public AudioRolloffMode RolloffMode;

    /// <summary>Determines how long this SFXLayer will be delayed for.</summary>
    /// <returns>The delay duration in seconds.</returns>
    public float GetDelayDuration()
    {
        if (DelaySFX)
        {
            //Calculates the delay time
            float Delay;
            if (RandomizeDelay) { Delay = Random.Range(MinDelayTime, MaxDelayTime); }
            else { Delay = FixedDelayTime; }
            return Delay;
        }
        else { return 0; }
    }

    /// <summary>Gets the final volume to use for this SFXLayer.</summary>
    /// <param name="VolumeMultiplier">The volume multiplier applied by the SFXObject.</param>
    /// <returns>The final volume.</returns>
    public float GetVolume(float VolumeMultiplier = 1)
    {
        float Volume;
        if (RandomizeVolume) { Volume = Random.Range(MinVolume, MaxVolume); }
        else { Volume = FixedVolume; }
        return Volume * VolumeMultiplier;
    }

    /// <summary>Gets the final pitch to use for this SFXLayer.</summary>
    /// <param name="PitchMultiplier">The pitch multiplier applied by the SFXObject.</param>
    /// <returns>The final pitch.</returns>
    public float GetPitch(float PitchMultiplier = 1)
    {
        float Pitch;
        if (RandomizePitch) { do { Pitch = Random.Range(MinPitch, MaxPitch); } while (Pitch == 0); }
        else { Pitch = FixedPitch; }
        return Mathf.Min(Pitch * PitchMultiplier, 3);
    }

    /// <summary>Plays the audio in this SFXLayer.</summary>
    /// <param name="DelayDuration">The duration of the delay before the audio is played.</param>
    /// <param name="SFXVolume">The volume multiplier applied.</param>
    /// <param name="SFXPitch">The pitch multiplier applied.</param>
    /// <returns>The Coroutine for delaying this SFXLayer (null if no delay is applied).</returns>
    public Coroutine Play(float DelayDuration, float SFXVolume, float SFXPitch)
    {
        //Delegates job to manager if a delay is required
        if (DelayDuration > 0) { return SFXManager.Main.PlaySuspended(this, DelayDuration, SFXVolume, SFXPitch); }
        else
        {
            //Plays SFXLayer
            PlaySFXAudio(SFXVolume, SFXPitch);
            return null;
        }
    }

    /// <summary>Plays the audio in this SFXLayer in 3D space.</summary>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">The duration of the delay before the audio is played.</param>
    /// <param name="SFXVolume">The volume multiplier applied.</param>
    /// <param name="SFXPitch">The pitch multiplier applied.</param>
    /// <param name="Parent">Parent transform to assign to the SFXLayer (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXLayer at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXLayer (null if no delay is applied).</returns>
    public Coroutine Play(Vector3 Position, float DelayDuration, float SFXVolume, float SFXPitch, Transform Parent = null, bool IsGlobalPosition = true)
    {
        //Delegates job to manager if a delay is required
        if (DelayDuration > 0)
        {
            return SFXManager.Main.PlaySuspended(this, SFXVolume, SFXPitch, DelayDuration);
        }

        //Plays SFXLayer
        Transform AudioTransform = PlaySFXAudio(SFXVolume, SFXPitch).Source.gameObject.transform;
        AudioTransform.parent = Parent
            ? Parent
            : SFXManager.Main.Parent;

        if (IsGlobalPosition) { AudioTransform.position = Position; }
        else { AudioTransform.localPosition = Position; }

        return null;
    }

    /// <summary>Plays the audio of this SFXLayer on a new AudioObject.</summary>
    /// <param name="SFXVolume">The volume multiplier applied.</param>
    /// <param name="SFXPitch">The pitch multiplier applied.</param>
    /// <returns>The AudioObject that this SFXLayer is being played on.</returns>
    private AudioObject PlaySFXAudio(float SFXVolume, float SFXPitch)
    {
        //Calculates volume and pitch
        float Volume = GetVolume(SFXVolume);
        float Pitch = GetPitch(SFXPitch);

        //Creates AudioObject and sequences end
        AudioObject LayerAudio = SFXManager.Main.StartAudio(SFX, false, false);
        CopySFXSettings(LayerAudio);
        LayerAudio.SourceVolume = Volume;
        LayerAudio.Source.pitch = Pitch;
        LayerAudio.Play();
        SFXManager.Main.SequenceEnd(LayerAudio);

        return LayerAudio;
    }

    /// <summary>Copies over settings from the SFXLayer to the target AudioObject.</summary>
    /// <param name="TargetAudio">The AudioObject that will be playing this SFXLayer.</param>
    private void CopySFXSettings(AudioObject TargetAudio)
    {
        TargetAudio.Source.spatialBlend = SpatialBlend;
        TargetAudio.Source.panStereo = StereoPan;
        TargetAudio.Source.reverbZoneMix = ReverbZoneMix;
        TargetAudio.Source.bypassEffects = BypassEffects;
        TargetAudio.Source.mute = Mute || SFXManager.Main.Muted;
        TargetAudio.Source.bypassReverbZones = BypassReverbZones;
        TargetAudio.Source.priority = Priority;
        TargetAudio.Source.minDistance = MinDistance;
        TargetAudio.Source.maxDistance = MaxDistance;
        TargetAudio.Source.rolloffMode = RolloffMode;

        if (MixerOverride != null)
        {
            TargetAudio.Source.outputAudioMixerGroup = MixerOverride;
        }
    }
}

}
