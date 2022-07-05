using _Elementis.Scripts.Character_Controller;
using PGSauce.Core.Extensions;
using PGSauce.Core.Utilities;
using PGSauce.Unity;
using UnityEngine;
using UnityEngine.Audio;

namespace _Elementis.Scripts
{
    public class BiomeMusic : PGMonoBehaviour
    {
        public ElementisCharacterController player;
        public MinMax<float> distanceRange;
        public MinMax<float> volume;
        public string param = "VillageBiomeVolume";
        [SerializeField] private AudioMixerGroup outputAudio;

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
                outputAudio.audioMixer.SetFloat(param, -80f);
                return;
            }

            var v = distance.Remap(distanceRange, this.volume);
            outputAudio.audioMixer.SetFloat(param, v);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distanceRange.max);
        }
    }
}