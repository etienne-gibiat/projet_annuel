using PGSauce.AudioManagement.External;

namespace PGSauce.AudioManagement
{
    /// <summary>
    /// Abstract Sound Module. PgAudioManager.SFX and PgAudioManager.Music are SoundModules. They can be globally paused, muted etc.
    /// </summary>
    public abstract class PgSoundModule
    {
        private readonly SoundManager _soundManager;
        
        /// <summary>If the SoundManager has been paused.</summary>
        public bool Paused
        {
            get => _soundManager.Paused;
            set => _soundManager.Paused = value;
        }
        
        /// <summary>If the SoundManager has been muted.</summary>
        public bool Muted
        {
            get => _soundManager.Muted;
            set => _soundManager.Muted = value;
        }

        protected PgSoundModule(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }
        
        /// <summary>Pauses all audio, fades and delays on the SoundManager.</summary>
        public void Pause()
        {
            _soundManager.Pause();
        }

        /// <summary>UnPauses all audio, fades and delays on the SoundManager.</summary>
        public void UnPause()
        {
            _soundManager.UnPause();
        }

        /// <summary>Mutes all audio on the SoundManager.</summary>
        public void Mute()
        {
            _soundManager.Mute();
        }
        
        /// <summary>UnMutes all audio on the SoundManager.</summary>
        public void UnMute()
        {
            _soundManager.UnMute();
        }

        /// <summary>Sets the global volume of the SoundManager, stopping any current fade effects.</summary>
        /// <param name="volume">The desired volume.</param>
        public void SetVolume(float volume)
        {
            _soundManager.SetVolume(volume);
        }
        
        /// <summary>Sets the global volume of the SoundManager with a fade effect, stopping any current fade effects.</summary>
        /// <param name="volume">The desired volume.</param>
        /// <param name="fadeDuration">The duration of the fade effect.</param>
        public void SetVolume(float volume, float fadeDuration)
        {
            _soundManager.SetVolume(volume, fadeDuration);
        }
        
    }
}