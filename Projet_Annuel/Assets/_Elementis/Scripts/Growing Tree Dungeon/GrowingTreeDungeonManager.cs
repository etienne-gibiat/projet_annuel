using System;
using _Elementis.Scripts.Character_Controller;
using _Elementis.Scripts.Spells;
using Cinemachine;
using JetBrains.Annotations;
using PGSauce.AudioManagement;
using PGSauce.Save;
using UnityEngine;
using UnityEngine.Playables;

namespace _Elementis.Scripts.Growing_Tree_Dungeon
{
    public class GrowingTreeDungeonManager : DungeonManager<GrowingTreeDungeonManager>
    {
        [SerializeField] private PgMusicTrack ambient;
        [SerializeField] private PlayableDirector director;
        [SerializeField] private DungeonDoor door;

        
        private Action _onTrackFinished;

        private void Start()
        {
            PgAudioManager.Music.Play(ambient, 2f, 0.5f);
        }
        
        public void OnTreeGrown(Action onTreeGrownFinished)
        {
            PgAudioManager.Music.Stop(ambient, 1.5f);
            director.Play();
            _onTrackFinished = onTreeGrownFinished;
        }

        [UsedImplicitly]
        public void OnTrackFinished()
        {
            PgAudioManager.Music.Play(ambient, 2f, 0.5f);
            _onTrackFinished?.Invoke();
        }

        public void OnSpellCollected(SpellData spell)
        {
            var player = FindObjectOfType<ElementisCharacterController>();
            player.TakeControlFromPlayer();
            door.Open(() =>
            {
                player.GiveControlToPlayer();
            });
        }
    }
}