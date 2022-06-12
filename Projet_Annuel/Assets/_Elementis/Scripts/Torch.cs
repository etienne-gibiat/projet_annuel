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

        private void Awake()
        {
            fire.SetActive(false);
        }

        [UsedImplicitly]
        public void TurnOn()
        {
            fire.SetActive(true);
            if (fireTurnOnSfx)
            {
                PgAudioManager.Sfx.Play(fireTurnOnSfx, fire.transform.position);
            }
            else
            {
                PGDebug.Message($"FIRE SFX Torch").LogTodo();
            }
        }
    }
}