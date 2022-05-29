using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Internal code used by AudioManagerPro.</summary>
namespace PGSauce.AudioManagement.External
{
    /// <summary>Container and wrapper for an AudioSource used by MusicManager and SFXManager.</summary>
    public class AudioObject
    {
        /// <summary>Different types of audio.</summary>
        public enum SoundType
        {
            /// <summary>Music AudioObjects.</summary>
            Music = 0,
            /// <summary>Sound effect AudioObjects.</summary>
            SFX = 1
        }

        //Public Variables
        /// <summary>The AudioSource containing the audio for this AudioObject.</summary>
        public AudioSource Source;

        /// <summary>The length of the AudioObject in seconds. (Read Only)</summary>
        public float length { get { return Source.clip.length; } }

        /// <summary>The current Coroutine for fading audio on this AudioObject.</summary>
        public Coroutine FadeJob;

        /// <summary>The type of audio this AudioObject handles.</summary>
        public readonly SoundType AudioType;

        /// <summary>Creates an AudioObject.</summary>
        /// <param name="AudioType">The type of audio this AudioObject will handle.</param>
        public AudioObject(SoundType AudioType) { this.AudioType = AudioType; }

        private float _AudioVolume = 1f;
        /// <summary>The volume of the audio source (0.0 to 1.0).</summary>
        public float volume
        {
            get { return _AudioVolume; }
            set
            {
                _AudioVolume = value;
                float Volume = _AudioVolume * _SourceVolume;
                switch (AudioType)
                {
                    case SoundType.Music: { Volume *= MusicManager.Main.MasterVolume; break; }
                    case SoundType.SFX: { Volume *= SFXManager.Main.MasterVolume; break; }
                    default: break;
                }
                Source.volume = Volume;
            }
        }

        private float _SourceVolume = 1f;
        /// <summary>The volume of the unchanged source audio.</summary>
        public float SourceVolume
        {
            get { return _SourceVolume; }
            set
            {
                _SourceVolume = value;
                volume = _AudioVolume;
            }
        }

        /// <summary>Pauses the playing clip.</summary>
        public void Pause() { Source.Pause(); }
        /// <summary>Unpauses the paused playback of this AudioSource.</summary>
        public void UnPause() { Source.UnPause(); }
        /// <summary>Plays the audio in the AudioObject.</summary>
        public void Play()
        {
            bool _Muted = Source.mute;
            Source.Play();
            Source.mute = _Muted;
        }
        /// <summary>Stops the audio in the AudioObject.</summary>
        public void Stop()
        {
            StopFade();
            Source.Stop();
        }

        /// <summary>Stops the current Coroutine for fading audio on this AudioObject.</summary>
        public void StopFade()
        {
            if (FadeJob != null)
            {
                switch (AudioType)
                {
                    case SoundType.Music: { MusicManager.Main.StopCoroutine(FadeJob); ; break; }
                    default: break;
                }
                FadeJob = null;
            }
        }
    }
}
