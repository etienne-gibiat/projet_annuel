using PGSauce.AudioManagement.External;

namespace PGSauce.AudioManagement
{
    /// <summary>Container and wrapper for an AudioSource used by <see cref="PgAudioManager"/></summary>
    public class PgAudioObject : External.AudioObject
    {
        private PgAudioObject(SoundType audioType) : base(audioType)
        {
        }

        /// <summary>
        /// Used to copy a AMP Audio Object (<see cref="AudioObject"/>)
        /// </summary>
        /// <param name="audio">The AMP audio to copy</param>
        public PgAudioObject(External.AudioObject audio) : this(audio.AudioType)
        {
        }
    }
}