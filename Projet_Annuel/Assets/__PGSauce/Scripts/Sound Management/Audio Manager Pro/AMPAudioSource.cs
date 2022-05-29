using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PGSauce.AudioManagement.External
{
    [RequireComponent(typeof(AudioSource))]
public class AMPAudioSource : MonoBehaviour
{
    public float Volume
    {
        get { return _volume; }
        set
        {
            _volume = Mathf.Clamp01(value);
            OnVolumeChange(SFXManager.Main.MasterVolume);
        }
    }
    [Range(0, 1)]
    [SerializeField] private float _volume = 1;

    public AudioClip Clip
    {
        get { return _source.clip; }
        set { _source.clip = value; }
    }

    public bool Loop
    {
        get { return _source.loop; }
        set { _source.loop = value; }
    }

    private AudioSource _source;

    private void OnVolumeChange(float masterVolume) { _source.volume = Volume * masterVolume; }
    private void OnPause() { _source.Pause(); }
    private void OnUnPause() { _source.UnPause(); }
    private void OnMuteChange(bool mute) { _source.mute = mute; }
    private void OnStopAll() { _source.Stop(); }

    public void Play()
    {
        bool isMuted = _source.mute;
        _source.Play();
        _source.mute = isMuted;
    }

    public void Stop()
    {
        _source.Stop();
    }

    private void FlushAllCallbacks()
    {
        OnVolumeChange(SFXManager.Main.MasterVolume);
        OnMuteChange(SFXManager.Main.Muted);
    }

    private void OnEnable()
    {
        if (!_source) { _source = GetComponent<AudioSource>(); }
        _source.outputAudioMixerGroup = SFXManager.Main.Mixer;
        RegisterAllCallbacks();
    }

    private void OnDisable()
    {
        DeregisterAllCallbacks();
    }

    private void RegisterAllCallbacks()
    {
        SFXManager.Main.OnVolumeChange += OnVolumeChange;
        SFXManager.Main.OnMuteChange += OnMuteChange;
        SFXManager.Main.OnPause += OnPause;
        SFXManager.Main.OnUnPause += OnUnPause;
        SFXManager.Main.OnStopAll += OnStopAll;
    }

    private void DeregisterAllCallbacks()
    {
        SFXManager.Main.OnVolumeChange -= OnVolumeChange;
        SFXManager.Main.OnMuteChange -= OnMuteChange;
        SFXManager.Main.OnPause -= OnPause;
        SFXManager.Main.OnUnPause -= OnUnPause;
        SFXManager.Main.OnStopAll -= OnStopAll;
    }
}

}

