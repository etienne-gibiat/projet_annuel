using UnityEngine;
using System.Collections;

namespace PGSauce.AudioManagement.External
{
    public class DemoScript : MonoBehaviour
    {
        public void PlayLoop(AudioClip clip) { SFXLoopManager.Main.Play(clip); }

        public void Play2(Track DesiredTrack) { MusicManager.Main.Play(DesiredTrack, 2, 2); }
        public void SetMusicVolume2(float DesiredVolume) { MusicManager.Main.SetVolume(DesiredVolume, 2); }
        public void SetSFXVolume2(float DesiredVolume) { SFXManager.Main.SetVolume(DesiredVolume, 2); }
        public void Play(Playlist DesiredPlaylist) { PlaylistManager.Main.Play(DesiredPlaylist); }
        public void Play(SFXObject DesiredSFX) { SFXManager.Main.Play(DesiredSFX); }
        public void Play(SFXGroup DesiredSFX) { SFXManager.Main.Play(DesiredSFX); }
        public void PlayExplosions(SFXGroup DesiredSFX) { SFXManager.Main.Play(DesiredSFX, 0.5f, 0.2f); }
    }
}

