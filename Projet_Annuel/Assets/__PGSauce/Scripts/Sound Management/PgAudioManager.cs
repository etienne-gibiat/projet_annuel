using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Utilities;
using PGSauce.Unity;

namespace PGSauce.AudioManagement
{
    /// <summary>
    /// The main audio class.
    /// Singleton (<see cref="MonoSingleton{T}"/>).
    /// Must be put in a bootstrap scene.
    /// Has a DontDestroyOnLoad (<see cref="DontDestroyOnLoad"/>).
    /// </summary>
    public class PgAudioManager : MonoSingleton<PgAudioManager>
    {
        #region Public And Serialized Fields
        #endregion
        #region Private Fields

        #endregion
        #region Properties
        /// <summary>
        /// The music module.
        /// Used to manipulate tracks.
        /// All music should be played with PgAudioManager.Music.Play(...)
        /// </summary>
        public static PgMusicModule Music { get; private set; }
        /// <summary>
        /// The SFX module.
        /// Used to manipulate SFX Objects and Groups.
        /// All SFX should be played with PgAudioManager.SFX.Play(...)
        /// </summary>
        public static PgSfxModule Sfx { get; private set; }

        #endregion
        #region Unity Functions
        #endregion
        #region Public Methods
        /// <summary>
        /// Overriden to create Music and SFX modules
        /// </summary>
        public sealed override void Init()
        {
            base.Init();
            Music = new PgMusicModule(MusicManager.Main);
            Sfx = new PgSfxModule(SFXManager.Main);
        }
        #endregion
        #region Private Methods
        #endregion
    }
}
