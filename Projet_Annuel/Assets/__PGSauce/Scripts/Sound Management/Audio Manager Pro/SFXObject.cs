using System.Collections;
using System.Collections.Generic;
using PGSauce.AudioManagement;
using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.AudioManagement.External
{
    /// <summary>A full sound effect comprised of multiple layers, each containing their own AudioClip and customised randomisation.</summary>
[CreateAssetMenu(fileName = "SFX", menuName =  "AMP/SFX Object", order = 3)]
public abstract class SFXObject : PgSfxBase
{
#if UNITY_EDITOR
    public bool CollapseEditions;
#endif
    /// <summary>(Optional) The name of this SFXObject.</summary>
    [Tooltip("(Optional) The name of this SFXObject.")]
    public string SFXName;

    /// <summary>The different SFXLayers in this SFXObject.</summary>
    [Tooltip("The different SFXLayers in this SFXObject.")]
    public SFXLayer[] SFXLayers = new SFXLayer[1];


    /// <summary>If the volume of the sound should be randomized.</summary>
    [Tooltip("If the volume of the sound should be randomized.")]
    public bool RandomizeVolume;

    /// <summary>The volume multiplier of the SFXObject.</summary>
    [Tooltip("The volume multiplier of the SFXObject.")]
    public float FixedVolume = 1f;

    /// <summary>The minimum volume multiplier of the SFXObject.</summary>
    [Tooltip("The minimum volume multiplier of the SFXObject.")]
    public float MinVolume = 0.5f;

    /// <summary>The maximum volume multiplier of the SFXGroup.</summary>
    [Tooltip("The maximum volume multiplier of the SFXObject.")]
    public float MaxVolume = 1f;


    /// <summary>If the pitch of the sound should be randomized.</summary>
    [Tooltip("If the pitch of the sound should be randomized.")]
    public bool RandomizePitch;

    /// <summary>The pitch multiplier of the SFXObject.</summary>
    [Tooltip("The pitch multiplier of the SFXObject.")]
    public float FixedPitch = 1f;

    /// <summary>The minimum pitch multiplier of the SFXObject.</summary>
    [Tooltip("The minimum pitch multiplier of the SFXObject.")]
    public float MinPitch = 0.75f;

    /// <summary>The maximum pitch multiplier of the SFXObject.</summary>
    [Tooltip("The maximum pitch multiplier of the SFXObject.")]
    public float MaxPitch = 1.5f;


    /// <summary>If there should be a global delay before playing this SFXObject.</summary>
    [Tooltip("If there should be a global delay before playing this SFXObject.")]
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

    /// <summary>If polyphony limits should be applied. With polyphony limiting, the behaviour of simultaneous and overlapping sounds is controlled.</summary>
    [Tooltip("If polyphony limits should be applied. With polyphony limiting, the behaviour of simultaneous and overlapping sounds is controlled.")]
    public bool LimitPolyphony;

    /// <summary>The maximum number of voices for this SFXObject, or how many can be played in tandem. 0 results in infinite voices.</summary>
    [Tooltip("The maximum number of voices for this SFXObject, or how many can be played in tandem. 0 results in infinite voices.")]
    public int MaxVoices;

    /// <summary>The minimum time separation between subsequent plays of this SFXObject; if the duration has not been surpassed, the SFXObject will not be played.</summary>
    [Tooltip("The minimum time separation between subsequent plays of this SFXObject; if the duration has not been surpassed, the SFXObject will not be played.")]
    public float MinSeparation;

    /// <summary>Timestamp of the last time this SFXObject was played.</summary>
    [System.NonSerialized]
    private float LastPlayTimestamp;

	public override string ToString ()
	{
		string NameString = SFXName;
		if (System.String.IsNullOrEmpty(SFXName)) { NameString = name; }
		return NameString;
	}

    /// <summary>Resets the SFXObject to its untampered state.</summary>
    private void Reset()
    {
        LastPlayTimestamp = 0;
    }

    /// <summary>Constructs a new empty SFXObject.</summary>
    public SFXObject()
    {
        SFXLayers[0].FixedVolume = 1f;
        SFXLayers[0].FixedPitch = 1f;
        SFXLayers[0].MinVolume = 0.5f;
        SFXLayers[0].MaxVolume = 1f;
        SFXLayers[0].MinPitch = 0.75f;
        SFXLayers[0].MaxPitch = 1.5f;
        SFXLayers[0].Priority = 128;
        SFXLayers[0].MinDistance = 1;
        SFXLayers[0].MaxDistance = 500;
    }

    /// <summary>Determines how long this SFXObject will be delayed for.</summary>
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

    /// <summary>Determines the volume multiplier for this SFXObject.</summary>
    /// <returns>The volume multiplier.</returns>
    public float GetVolumeMultiplier()
    {
        if (RandomizeVolume) { return Random.Range(MinVolume, MaxVolume); }
        else { return FixedVolume; }
    }

    /// <summary>Determines the pitch multiplier for this SFXObject.</summary>
    /// <returns>The volume multiplier.</returns>
    public float GetPitchMultiplier()
    {
        if (RandomizePitch)
        {
            float Pitch;
            do { Pitch = Random.Range(MinPitch, MaxPitch); } while (Pitch == 0);
            return Pitch;
        }
        else { return FixedPitch; }
    }

    /// <summary>Plays the SFXObject.</summary>
    /// <param name="DelayDuration">The duration of the delay before the SFXObject is played.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine Play(float DelayDuration) { return Play(DelayDuration, GetVolumeMultiplier(), GetPitchMultiplier()); }

    /// <summary>Plays the SFXObject.</summary>
    /// <param name="DelayDuration">The duration of the delay before the SFXObject is played.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine Play(float DelayDuration, float Volume) { return Play(DelayDuration, Volume, GetPitchMultiplier()); }

    /// <summary>Plays the SFXObject.</summary>
    /// <param name="DelayDuration">The duration of the delay before the SFXObject is played.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine Play(float DelayDuration, float Volume, float Pitch)
    {
        //Delegates job to manager if a delay is required
        if (DelayDuration > 0) { return SFXManager.Main.PlaySuspended(this, DelayDuration, Volume, Pitch); }
        else
        {
            if (!LimitPolyphony || (Time.realtimeSinceStartup > LastPlayTimestamp + MinSeparation))
            {
                //Plays SFXLayers
                LastPlayTimestamp = Time.realtimeSinceStartup;
                for (int i = 0; i < SFXLayers.Length; i++) { SFXLayers[i].Play(SFXLayers[i].GetDelayDuration(), Volume, Pitch); }
            }

            //No delay
            return null;
        }
    }

    /// <summary>Plays the SFXObject in 3D space.</summary>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">The duration of the delay before the SFXObject is played.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine Play(Vector3 Position, float DelayDuration, Transform Parent = null, bool IsGlobalPosition = true) { return Play(Position, DelayDuration, GetVolumeMultiplier(), GetPitchMultiplier(), Parent, IsGlobalPosition); }

    /// <summary>Plays the SFXObject in 3D space.</summary>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">The duration of the delay before the SFXObject is played.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine Play(Vector3 Position, float DelayDuration, float Volume, Transform Parent = null, bool IsGlobalPosition = true) { return Play(Position, DelayDuration, Volume, GetPitchMultiplier(), Parent, IsGlobalPosition); }

    /// <summary>Plays the SFXObject in 3D space.</summary>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">The duration of the delay before the SFXObject is played.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine Play(Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true)
    {
        //Delegates job to manager if a delay is required
        if (DelayDuration > 0) { return SFXManager.Main.PlaySuspended(this, Position, DelayDuration, Volume, Pitch, Parent, IsGlobalPosition); }
        else
        {
            if (!LimitPolyphony || (Time.realtimeSinceStartup > LastPlayTimestamp + MinSeparation))
            {
                //Plays SFXLayers
                LastPlayTimestamp = Time.realtimeSinceStartup;
                for (int i = 0; i < SFXLayers.Length; i++)
                {
                    SFXLayers[i].Play(Position, SFXLayers[i].GetDelayDuration(), Volume, Pitch, Parent, IsGlobalPosition);
                }
            }

            //No delay
            return null;
        }
    }
}
}


