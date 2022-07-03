using System;
using _Elementis.Scripts.Character_Controller;
using PGSauce.Core.Extensions;
using PGSauce.Core.Utilities;
using PGSauce.Unity;
using UnityEngine;
using UnityEngine.Audio;

namespace _Elementis.Scripts.Village
{
    public class VillageSound : PGMonoBehaviour
    {
        public ElementisCharacterController player;
        public MinMax<float> distanceRange;
        public MinMax<float> volume;
        [SerializeField] private AudioMixerGroup villageAudio;

        private void Awake()
        {
            if (player == null)
            {
                player = FindObjectOfType<ElementisCharacterController>();
            }
        }

        private void Update()
        {
            if (Time.frameCount % 10 != 0)
            {
                // update every 10 frame
                return;
            }

            var distance = Vector3.Distance(transform.position, player.Position);
            if (distance > distanceRange.max)
            {
                villageAudio.audioMixer.SetFloat("VillageVolume", -80f);
                return;
            }

            var volume = distance.Remap(distanceRange, this.volume);
            villageAudio.audioMixer.SetFloat("VillageVolume", volume);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distanceRange.max);
        }
    }
}