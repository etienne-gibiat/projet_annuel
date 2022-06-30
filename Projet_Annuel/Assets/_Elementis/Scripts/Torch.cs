using System;
using JetBrains.Annotations;
using PGSauce.AudioManagement;
using PGSauce.Core.PGDebugging;
using UnityEngine;

namespace _Elementis.Scripts
{
    public class Torch : MonoBehaviour
    {
        public GameObject fire;
        public PgSfxObject fireTurnOnSfx;
        public AudioSource fireLoopSound;
        public bool turnOnAtStart;
        public bool doTurnOnSound;
        private bool _on;

        private void Awake()
        {
            if (turnOnAtStart)
            {
                TurnOn();
            }
            else
            {
                fire.SetActive(false);
                _on = false;
            }
        }

        [UsedImplicitly]
        public void TurnOn()
        {
            if (_on)
            {
                return;
            }

            _on = true;
            fire.SetActive(true);
            if (fireTurnOnSfx && doTurnOnSound)
            {
                PgAudioManager.Sfx.Play(fireTurnOnSfx, fire.transform.position);
            }
            fireLoopSound.Play();
        }
    }
}