using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using PGSauce.AudioManagement.External.EventBinding;
using PGSauce.Core.PGDebugging;

namespace PGSauce.AudioManagement.External
{
    /// <summary>Base class for all sound managers.</summary>
    public abstract class SoundManager : MonoBehaviour
    {
        /// <summary>(Optional) Audio mixer to set as the output of all audio handled by the SoundManager.</summary>
        [Tooltip("(Optional) Audio mixer to set as the output of all audio handled by the SoundManager.")]
        public AudioMixerGroup Mixer;

        /// <summary>Marks the GameObject as DontDestroyOnLoad to provide the manager with scene persistence (Note: manager must be present on a root GameObject for this functionality to work).</summary>
        [Tooltip("Marks the GameObject as DontDestroyOnLoad to provide the manager with scene persistence (Note: manager must be present on a root GameObject for this functionality to work).")]
        [SerializeField]
        private bool ScenePersistent;

        /// <summary>The type of audio this sound manager handles.</summary>
        public abstract AudioObject.SoundType ManagerType { get; }

        /// <summary>(Read Only) The parent empty to store all of the AudioObjects under.</summary>
        public Transform Parent { get; protected set; }

        /// <summary>If the SoundManager has been paused.</summary>
        public bool Paused
        {
            get { return _Paused; }
            set
            {
                if (value != _Paused)
                {
                    if (_Paused && !value) { UnPause(); }
                    else if (!_Paused && value) { Pause(); }
                }
            }
        }
        private bool _Paused;

        /// <summary>If the SoundManager has been muted.</summary>
        public bool Muted
        {
            get { return _Muted; }
            set
            {
                if (value != _Muted)
                {
                    if (_Muted && !value) { UnMute(); }
                    else if (!_Muted && value) { Mute(); }
                }
            }
        }
        private bool _Muted;

        /// <summary>The global volume of the SoundManager.</summary>
        public float MasterVolume
        {
            get { return Volume; }
            set
            {
                //Sets the Volume
                if (Volume != value) { OnStateChange(); }
                Volume = value;

                //Forces update on active audio
                FlushVolumeChange();
            }
        }
        private void FlushVolumeChange()
        {
            for (int i = 0; i < ActiveAudio.Count; i++) { ActiveAudio[i].volume = ActiveAudio[i].volume; }
            OnVolumeChange(Volume);
        }

        /// <summary>The global volume of the SoundManager.</summary>
        protected float Volume = 1f;

        /// <summary>Current number of playing audio sources in this sound manager.</summary>
        public int CurrentAudioSourceCount { get { return ActiveAudio.Count; } }

        /// <summary>Callback for various state changes.</summary>
        public System.Action OnStateChange = () => { };

        /// <summary>Callback for the volume of the SoundManager changing.</summary>
        public System.Action<float> OnVolumeChange = (float x) => { };

        /// <summary>Callback for on pause.</summary>
        public System.Action OnPause = () => { };

        /// <summary>Callback for on pause.</summary>
        public System.Action OnUnPause = () => { };

        /// <summary>Callback for mute state change.</summary>
        public System.Action<bool> OnMuteChange = (bool x) => { };

        /// <summary>All of the active AudioObjects owned by the SoundManager.</summary>
        protected List<AudioObject> ActiveAudio = new List<AudioObject>();

        /// <summary>All of the inactive AudioObjects owned by the SoundManager</summary>
        protected List<AudioObject> InactiveAudio = new List<AudioObject>();

        [BindableMethod]
        /// <summary>Removes and destroys any excess inactive AudioObjects to free up memory.</summary>
        public void RemoveInactive()
        {
            for (int i = 0; i < InactiveAudio.Count; i++) { Destroy(InactiveAudio[i].Source); }
            InactiveAudio.Clear();
        }

        /// <summary>The current Coroutine for fading the volume.</summary>
        protected Coroutine FadeJob;

        protected virtual void Awake()
        {
            if (ScenePersistent) { DontDestroyOnLoad(this); }
            Parent = new GameObject("Sound Objects").transform;
            Parent.parent = transform;
            _Paused = false;
        }

        
        /// <summary>Sets the global volume of the SoundManager with a fade effect, stopping any current fade effects.</summary>
        /// <param name="Volume">The desired volume.</param>
        /// <param name="FadeDuration">The duration of the fade effect.</param>
        [BindableMethod]
        public void SetVolume(float Volume, float FadeDuration)
        {
            //Begins fade
            if (FadeDuration > 0)
            {
                StopVolumeFade();
                FadeJob = StartCoroutine(VolumeFade(Volume, FadeDuration));
            }

            //Sets volume
            else { SetVolume(Volume); }
        }
        
        /// <summary>Sets the global volume of the SoundManager, stopping any current fade effects.</summary>
        /// <param name="Volume">The desired volume.</param>
        [BindableMethod]
        public void SetVolume(float Volume)
        {
            const bool debug = true;
            PGDebug.SetCondition(debug).Message($"Volume is {Volume}, current {this.Volume}").Log();
            if (Volume != this.Volume)
            {
                PGDebug.SetCondition(debug).Message($"DIFFERENT New Volume is {Volume}").Log();
                //Stops fade
                StopVolumeFade();

                //Sets Volume
                MasterVolume = Volume;
            }
        }

        /// <summary>Sets the global volume of the SoundManager with a fade effect.</summary>
        /// <param name="Volume">The desired volume.</param>
        /// <param name="FadeDuration">The duration of the fade effect.</param>
        protected abstract IEnumerator VolumeFade(float Volume, float FadeDuration);

        /// <summary>Stops the current Coroutine for fading the volume.</summary>
        protected void StopVolumeFade()
        {
            if (FadeJob != null)
            {
                StopCoroutine(FadeJob);
                FadeJob = null;
            }
        }

        /// <summary>Stops an AudioObject.</summary>
        /// <param name="Audio">The AudioObject to be stopped.</param>
        public void StopAudio(AudioObject Audio)
        {
            Audio.Stop();
            ActiveAudio.Remove(Audio);
            InactiveAudio.Add(Audio);
            Audio.Source.name = "Inactive AudioObject";
            OnStateChange();
        }

        [BindableMethod]
        /// <summary>Pauses all audio, fades and delays on the SoundManager.</summary>
        public void Pause()
        {
            for (int i = 0; i < ActiveAudio.Count; i++) { ActiveAudio[i].Pause(); }
            _Paused = true;
            OnStateChange();
            OnPause();
        }

        [BindableMethod]
        /// <summary>UnPauses all audio, fades and delays on the SoundManager.</summary>
        public void UnPause()
        {
            for (int i = 0; i < ActiveAudio.Count; i++) { ActiveAudio[i].UnPause(); }
            _Paused = false;
            OnStateChange();
            OnUnPause();
        }

        [BindableMethod]
        /// <summary>Mutes all audio on the SoundManager.</summary>
        public void Mute()
        {
            for (int i = 0; i < ActiveAudio.Count; i++) { ActiveAudio[i].Source.mute = true; }
            _Muted = true;
            OnStateChange();
            OnMuteChange(_Muted);
        }

        [BindableMethod]
        /// <summary>UnMutes all audio on the SoundManager.</summary>
        public void UnMute()
        {
            for (int i = 0; i < ActiveAudio.Count; i++) { ActiveAudio[i].Source.mute = false; }
            _Muted = false;
            OnStateChange();
            OnMuteChange(_Muted);
        }

        /// <summary>Starts an AudioClip in the scene.</summary>
        /// <param name="Audio">The desired AudioClip to be played.</param>
        /// <param name="Looping">Whether to loop the audio or not.</param>
        /// <param name="AutoPlay">If the audio should automatically play.</param>
        /// <returns>The AudioObject containing the audio.</returns>
        public AudioObject StartAudio(AudioClip Audio, bool Looping, bool AutoPlay = true) 
        {
            AudioObject ReturnedAudio = new AudioObject(ManagerType);

            //If inactive AudioObjects are available
            if (InactiveAudio.Count > 0)
            {
                //Reuses AudioObject
                ReturnedAudio = InactiveAudio[InactiveAudio.Count - 1];
                InactiveAudio.RemoveAt(InactiveAudio.Count - 1);
                ReturnedAudio.StopFade();
            }
            //If none are available
            else
            {
                //Creates a new AudioObject
                GameObject AudioObject = new GameObject("Audio Object");
                AudioObject.transform.parent = Parent;
                ReturnedAudio.Source = AudioObject.AddComponent<AudioSource>();
            }

            //Adds to ActiveAudio list
            ActiveAudio.Add(ReturnedAudio);

            //Sets up properties of the AudioSource
            ReturnedAudio.SourceVolume = 1f;
            ReturnedAudio.Source.name = Audio.name;
            ReturnedAudio.Source.time = 0;
            ReturnedAudio.Source.loop = Looping;
            ReturnedAudio.Source.clip = Audio;
            ReturnedAudio.Source.priority = 128;
            ReturnedAudio.volume = 1f;
            ReturnedAudio.Source.mute = _Muted;
            ReturnedAudio.Source.outputAudioMixerGroup = Mixer;

            //Plays the AudioObject
            if (AutoPlay) { ReturnedAudio.Play(); }

            OnStateChange();
            return ReturnedAudio;
        }
    }
}
