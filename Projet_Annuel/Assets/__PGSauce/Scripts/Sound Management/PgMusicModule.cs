using PGSauce.AudioManagement.External.EventBinding;
using UnityEngine;

namespace PGSauce.AudioManagement
{
    /// <summary>
    /// The main way to manipulate music.
    /// The way to access it is PgAudioManager.Music.
    /// AMP Music Manager Wrapper.
    /// </summary>
    public class PgMusicModule : PgSoundModule
    {
        private readonly MusicManager _musicManager;

        /// <summary>
        /// Used by PgAudioManager.
        /// Encapsulates the main AMP Music Manager.
        /// </summary>
        /// <param name="musicManager">Main AMP Music Manager (MusicManager.Main)</param>
        public PgMusicModule(MusicManager musicManager) : base(musicManager)
        {
            _musicManager = musicManager;
        }
        

        /// <summary>Determines if any Track is currently being played.</summary>
        public bool IsPlaying()
        {
            return _musicManager.IsPlaying();
        }

        /// <summary>Determines if the Track is currently being played.</summary>
        /// <param name="track">The Track to test.</param>
        public bool IsPlaying(PgMusicTrack track)
        {
            return _musicManager.IsPlaying(track);
        }

        /// <summary>Plays the desired Track, stopping any other active Tracks, with a crossfade effect if the desired Track has a non-zero fade duration on the main Edition.</summary>
        /// <param name="track">The desired Track to be played.</param>
        public void Play(PgMusicTrack track) { _musicManager.Play(track); }
        
        /// <summary>Plays the desired Track, stopping any other active Tracks, with a crossfade effect if the desired Track has a non-zero fade duation on the main Edition.</summary>
        /// <param name="track">The desired Track to be played.</param>
        /// <param name="delay">The duration of the pause in seconds before the Track is played.</param>
        /// <param name="isOneShot">If the Track will be played as a one shot, with no looping.</param>
        public void Play(PgMusicTrack track, float delay, bool isOneShot = false) { _musicManager.Play(track, delay, isOneShot); }

        /// <summary>Plays the desired Track, stopping any other active Tracks, with a crossfade effect.</summary>
        /// <param name="track">The desired Track to be played.</param>
        /// <param name="delay">The duration of the pause in seconds before the Track is played.</param>
        /// <param name="fadeDuration">The length of the crossfade effect.</param>
        /// <param name="isOneShot">If the Track will be played as a one shot, with no looping.</param>
        public void Play(PgMusicTrack track, float delay, float fadeDuration, bool isOneShot = false)
        {
            _musicManager.Play(track, delay, fadeDuration, isOneShot);
        }

        /// <summary>Stops the currently playing Track.</summary>
        public void Stop() { _musicManager.Stop(); }

        /// <summary>Stops the Track.</summary>
        /// <param name="audioTrack">The desired Track to be stopped.</param>
        public void Stop(PgMusicTrack audioTrack) { _musicManager.Stop(audioTrack); }

        /// <summary>Stops the currently playing Track with a crossfade effect.</summary>
        /// <param name="fadeDuration">The duration of the crossfade effect.</param>
        public void Stop(float fadeDuration) { _musicManager.Stop(fadeDuration); }

        /// <summary>Stops the desired Track with a crossfade effect if it is active.</summary>
        /// <param name="audioTrack">The desired Track to be stopped.</param>
        /// <param name="fadeDuration">The duration of the crossfade effect.</param>
        public void Stop(PgMusicTrack audioTrack, float fadeDuration) { _musicManager.Stop(audioTrack, fadeDuration); }
        
        
        /// <summary>Restarts the currently playing Track.</summary>
        public void Restart() { _musicManager.Restart(); }

        /// <summary>Restarts the desired Track if it is active.</summary>
        /// <param name="audioTrack">The desired Track to be restarted.</param>
        public void Restart(PgMusicTrack audioTrack) { _musicManager.Restart(audioTrack); }

        /// <summary>Restarts the currently playing Track with a crossfade effect.</summary>
        /// <param name="fadeDuration">The duration of the crossfade effect.</param>
        public void Restart(float fadeDuration) { _musicManager.Restart(fadeDuration); }

        /// <summary>Restarts the desired Track with a crossfade effect if it is active.</summary>
        /// <param name="audioTrack">The desired Track to be restarted.</param>
        /// <param name="fadeDuration">The duration of the crossfade effect.</param>
        public void Restart(PgMusicTrack audioTrack, float fadeDuration)
        {
            _musicManager.Restart(audioTrack, fadeDuration);
        }

        /// <summary>Starts an AudioClip in the scene.</summary>
        /// <param name="audioClip">The desired AudioClip to be played.</param>
        /// <returns>The AudioObject containing the audio.</returns>
        public PgAudioObject StartAudio(AudioClip audioClip)
        {
           return new PgAudioObject(_musicManager.StartAudio(audioClip));
        }
        
        /// <summary>Fades the volume of an AudioObject over time to the desired volume.</summary>
        /// <param name="audio">The AudioObject to fade the volume on.</param>
        /// <param name="fadeDuration">The duration of the fade effect.</param>
        /// <param name="target">The desired volume to fade the AudioObject's volume to.</param>
        public void FadeAudio(PgAudioObject audio, float fadeDuration, float target)
        {
            _musicManager.FadeAudio(audio, fadeDuration, target);
        }
        
        
        /// <summary>Switches the Edition of the currently playing Track.</summary>
        /// <param name="editionIndex">The index of the desired Edition in the current Track's array of Editions.</param>
        /// <param name="delay">The duration of the pause in seconds before the Edition is switched.</param>
        public void SwitchEdition(int editionIndex, float delay)
        {
            _musicManager.SwitchEdition(editionIndex, delay);
        }

        /// <summary>Switches the Edition of the currently playing Track.</summary>
        /// <param name="editionIndex">The index of the desired Edition in the current Track's array of Editions.</param>
        public void SwitchEdition(int editionIndex)
        {
            _musicManager.SwitchEdition(editionIndex);
        }
        
        /// <summary>Switches the Edition of the currently playing Track after a period of time.</summary>
        /// <param name="editionIndex">The index of the desired Edition in the current Track's array of Editions.</param>
        /// <param name="delay">The duration of the pause in seconds before the Edition is switched.</param>
        /// <param name="fadeOverride">The duration of the crossfade effect.</param>
        public void SwitchEdition(int editionIndex, float delay, float fadeOverride)
        {
            _musicManager.SwitchEdition(editionIndex, delay, fadeOverride);
        }

        /// <summary>Switches the Edition of the currently playing Track.</summary>
        /// <param name="desiredEdition">The desired Edition to switch to.</param>
        public void SwitchEdition(Edition desiredEdition)
        {
            _musicManager.SwitchEdition(desiredEdition);
        }

        /// <summary>Switches the Edition of the currently playing Track.</summary>
        /// <param name="desiredEdition">The desired Edition to switch to.</param>
        /// <param name="delay">The duration of the pause in seconds before the Edition is switched.</param>
        public void SwitchEdition(Edition desiredEdition, float delay)
        {
            _musicManager.SwitchEdition(desiredEdition, delay);
        }

        /// <summary>Switches the Edition of the currently playing Track after a period of time.</summary>
        /// <param name="desiredEdition">The desired Edition to switch to.</param>
        /// <param name="delay">The duration of the pause in seconds before the Edition is switched.</param>
        /// <param name="fadeOverride">The duration of the crossfade effect.</param>
        public void SwitchEdition(Edition desiredEdition, float delay, float fadeOverride)
        {
            _musicManager.SwitchEdition(desiredEdition, delay, fadeOverride);
        }
    }
}