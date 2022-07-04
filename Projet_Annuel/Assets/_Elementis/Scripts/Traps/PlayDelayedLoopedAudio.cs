using System;
using DG.Tweening;
using PGSauce.Unity;
using UnityEngine;

namespace _Elementis.Scripts.Traps
{
    public class PlayDelayedLoopedAudio : PGMonoBehaviour
    {
        public new AudioSource audio;
        public float delay;
        public float loopTime = 1f;

        private Tween _audioTween;
        
        private void Awake()
        {
            DOVirtual.DelayedCall(delay, () =>
            {
                Play();
            });
        }

        private void Play()
        {
            _audioTween = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    audio.Play();
                }).AppendInterval(loopTime)
                .AppendCallback(() =>
                {
                    Play();
                });
        }
    }
}