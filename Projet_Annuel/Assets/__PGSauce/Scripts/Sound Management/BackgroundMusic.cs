using System;
using UnityEngine;
using PGSauce.Unity;

namespace PGSauce.AudioManagement
{
    /// <summary>
    /// A simple MonoBehaviour to play a music at the start of the scene, if a music is already playing, it will be faded and stopped
    /// </summary>
    public class BackgroundMusic : PGMonoBehaviour
    {
        [SerializeField] private PgMusicTrack music;

        private void Start()
        {
            PgAudioManager.Music.Play(music);
        }
    }
}
