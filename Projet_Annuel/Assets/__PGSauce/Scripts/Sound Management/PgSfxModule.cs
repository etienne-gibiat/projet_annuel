using PGSauce.AudioManagement.External;
using PGSauce.AudioManagement.External.EventBinding;
using UnityEngine;

namespace PGSauce.AudioManagement
{
    /// <summary>
    /// The main way to manipulate SFXs.
    /// The way to access it is PgAudioManager.SFX.
    /// AMP SFX Manager Wrapper.
    /// </summary>
    public class PgSfxModule : PgSoundModule
    {
        /// <summary>
        /// AMP SFX Manager
        /// </summary>
        public SFXManager SfxManager { get; }

        /// <summary>
        /// Used by PgAudioManager.
        /// Encapsulates the main AMP SFX Manager.
        /// </summary>
        /// <param name="sfxManager">Main AMP SFX Manager (SFXManager.Main)</param>
        public PgSfxModule(SFXManager sfxManager): base(sfxManager)
        {
            SfxManager = sfxManager;
        }

        /// <summary>Plays an SFXObject with the default delay settings.</summary>
        /// <param name="sfx">The SFXObject to play.</param>
        /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
        public Coroutine Play(IPgSfx sfx)
        {
            return sfx.Play(this);
        }

        /// <summary>Plays an SFXObject.</summary>
        /// <param name="sfx">The SFXObject to play.</param>
        /// <param name="delayDuration">Duration of the global delay for the SFXObject.</param>
        /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
        public Coroutine Play(IPgSfx sfx, float delayDuration)
        {
            return sfx.Play(this, delayDuration);
        }

        /// <summary>Plays an SFXObject.</summary>
        /// <param name="sfx">The SFXObject to play.</param>
        /// <param name="delayDuration">Duration of the global delay for the SFXObject.</param>
        /// <param name="volume">The volume multiplier of the SFXObject to be used.</param>
        /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
        public Coroutine Play(IPgSfx sfx, float delayDuration, float volume)
        {
            return sfx.Play(this, delayDuration, volume);
        }

        /// <summary>Plays an SFXObject.</summary>
        /// <param name="sfx">The SFXObject to play.</param>
        /// <param name="delayDuration">Duration of the global delay for the SFXObject.</param>
        /// <param name="volume">The volume multiplier of the SFXObject to be used.</param>
        /// <param name="pitch">The pitch multiplier of the SFXObject to be used.</param>
        /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
        public Coroutine Play(IPgSfx sfx, float delayDuration, float volume, float pitch)
        {
            return sfx.Play(this, delayDuration, volume, pitch);
        }

        /// <summary>Plays an SFXObject with the default delay settings.</summary>
        /// <param name="sfx">The SFXObject to play.</param>
        /// <param name="position">The position in 3D space to play the SFXObject at.</param>
        /// <param name="parent">Parent transform to assign to the SFXObject (Optional).</param>
        /// <param name="isGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
        /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
        public Coroutine Play(IPgSfx sfx, Vector3 position, Transform parent = null, bool isGlobalPosition = true)
        {
            return sfx.Play(this, position, parent, isGlobalPosition);
        }

        /// <summary>Plays an SFXObject.</summary>
        /// <param name="sfx">The SFXObject to play.</param>
        /// <param name="position">The position in 3D space to play the SFXObject at.</param>
        /// <param name="delayDuration">Duration of the global delay for the SFXObject.</param>
        /// <param name="parent">Parent transform to assign to the SFXObject (Optional).</param>
        /// <param name="isGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
        /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
        public Coroutine Play(IPgSfx sfx, Vector3 position, float delayDuration, Transform parent = null,
            bool isGlobalPosition = true)
        {
            return sfx.Play(this, position, delayDuration, parent, isGlobalPosition);
        }

        /// <summary>Plays an SFXObject.</summary>
        /// <param name="sfx">The SFXObject to play.</param>
        /// <param name="position">The position in 3D space to play the SFXObject at.</param>
        /// <param name="delayDuration">Duration of the global delay for the SFXObject.</param>
        /// <param name="volume">The volume multiplier of the SFXObject to be used.</param>
        /// <param name="parent">Parent transform to assign to the SFXObject (Optional).</param>
        /// <param name="isGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
        /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
        public Coroutine Play(IPgSfx sfx, Vector3 position, float delayDuration, float volume,
            Transform parent = null, bool isGlobalPosition = true)
        {
            return sfx.Play(this, position, delayDuration, volume, parent, isGlobalPosition);
        }

        /// <summary>Plays an SFXObject.</summary>
        /// <param name="sfx">The SFXObject to play.</param>
        /// <param name="position">The position in 3D space to play the SFXObject at.</param>
        /// <param name="delayDuration">Duration of the global delay for the SFXObject.</param>
        /// <param name="volume">The volume multiplier of the SFXObject to be used.</param>
        /// <param name="pitch">The pitch multiplier of the SFXObject to be used.</param>
        /// <param name="parent">Parent transform to assign to the SFXObject (Optional).</param>
        /// <param name="isGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
        /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
        public Coroutine Play(IPgSfx sfx, Vector3 position, float delayDuration, float volume, float pitch,
            Transform parent = null, bool isGlobalPosition = true)
        {
            return sfx.Play(this, position, delayDuration, volume, pitch, parent, isGlobalPosition);
        }
        
        /// <summary>Delays the playing of an SFXObject.</summary>
        /// <param name="sfx">The SFXObject to play.</param>
        /// <param name="delayDuration">Duration of the global delay for the SFXObject.</param>
        /// <param name="volume">The volume multiplier of the SFXObject to be used.</param>
        /// <param name="pitch">The pitch multiplier of the SFXObject to be used.</param>
        /// <returns>The Coroutine for delaying this SFXObject.</returns>
        public Coroutine PlaySuspended(IPgSfx sfx, float delayDuration, float volume, float pitch)
        {
            return sfx.PlaySuspended(this, delayDuration, volume, pitch);
        }

        /// <summary>Delays the playing of an SFXObject.</summary>
        /// <param name="sfx">The SFXObject to play.</param>
        /// <param name="position">The position in 3D space to play the SFXObject at.</param>
        /// <param name="delayDuration">Duration of the global delay for the SFXObject.</param>
        /// <param name="volume">The volume multiplier of the SFXObject to be used.</param>
        /// <param name="pitch">The pitch multiplier of the SFXObject to be used.</param>
        /// <param name="parent">Parent transform to assign to the SFXObject (Optional).</param>
        /// <param name="isGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
        /// <returns>The Coroutine for delaying this SFXObject.</returns>
        public Coroutine PlaySuspended(IPgSfx sfx, Vector3 position, float delayDuration, float volume, float pitch, Transform parent = null, bool isGlobalPosition = true)
        {
            return sfx.PlaySuspended(this, position, delayDuration, volume, pitch, parent, isGlobalPosition);
        }

        /// <summary>Delays the playing of an SFXLayer.</summary>
        /// <param name="sfx">The SFXLayer to play.</param>
        /// <param name="delayDuration">Duration of the delay for the SFXLayer.</param>
        /// <param name="sfxVolume">The volume multiplier applied.</param>
        /// <param name="sfxPitch">The pitch multiplier applied.</param>
        /// <returns>The Coroutine for delaying this SFXLayer.</returns>
        public Coroutine PlaySuspended(SFXLayer sfx, float delayDuration, float sfxVolume, float sfxPitch)
        {
            return SfxManager.PlaySuspended(sfx, delayDuration, sfxVolume, sfxPitch);
        }

        /// <summary>Delays the playing of an SFXLayer in 3D space.</summary>
        /// <param name="sfx">The SFXLayer to play.</param>
        /// <param name="position">The position in 3D space to play the SFXObject at.</param>
        /// <param name="delayDuration">Duration of the delay for the SFXLayer.</param>
        /// <param name="sfxVolume">The volume multiplier applied.</param>
        /// <param name="sfxPitch">The pitch multiplier applied.</param>
        /// <param name="parent">Parent transform to assign to the SFXLayer (Optional).</param>
        /// <param name="isGlobalPosition">If the position to play the SFXLayer at is global or local to the parent.</param>
        /// <returns>The Coroutine for delaying this SFXLayer.</returns>
        public Coroutine PlaySuspended(SFXLayer sfx, Vector3 position, float delayDuration, float sfxVolume, float sfxPitch, Transform parent = null, bool isGlobalPosition = true)
        {
            return SfxManager.PlaySuspended(sfx, position, delayDuration, sfxVolume, sfxPitch, parent, isGlobalPosition);
        }

        /// <summary>Stops all sound currently being played.</summary>
        public void StopAll()
        {
            SfxManager.StopAll();
        }

        /// <summary>Sequences the end of a one shot AudioObject.</summary>
        /// <param name="audio">The AudioObject attempting to initiate the end.</param>
        /// <param name="remainingTime">How much time remains before the AudioObject ends.</param>
        public void SequenceEnd(PgAudioObject audio, float remainingTime)
        {
            SfxManager.SequenceEnd(audio, remainingTime);
        }

        /// <summary>Sequences the end of a one shot AudioObject.</summary>
        /// <param name="audio">The AudioObject attempting to initiate the end.</param>
        public void SequenceEnd(PgAudioObject audio)
        {
            SfxManager.SequenceEnd(audio);
        }
    }
}