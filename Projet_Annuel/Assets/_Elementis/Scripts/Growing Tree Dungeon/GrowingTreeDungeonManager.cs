using System;
using JetBrains.Annotations;
using PGSauce.AudioManagement;
using UnityEngine;
using UnityEngine.Playables;

namespace _Elementis.Scripts.Growing_Tree_Dungeon
{
    public class GrowingTreeDungeonManager : DungeonManager<GrowingTreeDungeonManager>
    {
        [SerializeField] private PgMusicTrack ambient;
        [SerializeField] private PlayableDirector director;
        private Action _onTrackFinished;

        private void Start()
        {
            PgAudioManager.Music.Play(ambient, 2f);
        }
        
        public void OnTreeGrown(Action onTreeGrownFinished)
        {
            PgAudioManager.Music.Stop(ambient);
            director.Play();
            _onTrackFinished = onTreeGrownFinished;
        }

        [UsedImplicitly]
        public void OnTrackFinished()
        {
            PgAudioManager.Music.Play(ambient, 2f);
            _onTrackFinished?.Invoke();
        }
    }
}