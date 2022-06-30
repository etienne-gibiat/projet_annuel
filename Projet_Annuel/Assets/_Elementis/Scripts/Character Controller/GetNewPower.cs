using System;
using _Elementis.Scripts.Spells;
using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Elementis.Scripts.Character_Controller
{
    public class GetNewPower : MonoBehaviour
    {
        [AssetsOnly, SerializeField] private ParticleSystem onCollectParticleSystem;
        [SerializeField] private SpellData spell;
        [SerializeField] private PGEventSpellData onCollected;
        [SerializeField] private CinemachineVirtualCamera cam;
        [SerializeField] private GameObject toHide;

        private bool _activated;

        private void OnTriggerEnter(Collider other)
        {
            if (_activated)
            {
                return;
            }
            
            if (other.gameObject.layer == Layers.PLAYER)
            {
                var player = other.gameObject.GetComponentInParent<ElementisCharacterController>();
                if (player)
                {
                    CollectNewSpell(player);
                }
            }
        }

        private void CollectNewSpell(ElementisCharacterController player)
        {
            _activated = true;
            player.TakeControlFromPlayer();
            cam.Priority = 100;
            cam.LookAt = player.transform;
            toHide.SetActive(false);

            ParticleSystem ps = null;

            var seq = DOTween.Sequence();
            seq.AppendInterval(0.75f)
                .AppendCallback(() =>
                {
                    ps = Instantiate(onCollectParticleSystem, player.transform.position, Quaternion.identity);
                })
                .AppendInterval(onCollectParticleSystem.main.duration)
                .AppendCallback(() =>
                {
                    cam.Priority = 0;
                    player.GiveControlToPlayer();
                    //Destroy(gameObject);
                    onCollected.Raise(spell);
                });
        }
    }
}