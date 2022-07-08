using System;
using _Elementis.Scripts.Character_Controller;
using _Elementis.Scripts.Spells;
using JetBrains.Annotations;
using PGSauce.AudioManagement;
using PGSauce.Core.PGDebugging;
using PGSauce.Save;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace _Elementis.Scripts.Lava_Dungeon
{
    public class LavaDungeonManager : DungeonManager<LavaDungeonManager>
    {
        public PgMusicTrack bossMusic;
        public ElementisCharacterController player;
        [SerializeField] private PlayableDirector director;
        public FightManager_Lava fightmanager;
        
        public void TriggerFight()
        {
            
            fightmanager.startFight();
            PGDebug.Message($"CEDRIC : mettre le boss / les vagues  comme tu veux ici").LogTodo();
            PgAudioManager.Music.Play(bossMusic, 0, 1);
        }
        
        [Button]
        public void OnFightFinished()
        {
            PGDebug.Message($"CEDRIC : appelle cette méthode que tout le fight est fini").LogTodo();
            
            
            player.TakeControlFromPlayer();
            director.Play();
            PgAudioManager.Music.Stop(bossMusic, 1.5f);
        }
        
        [UsedImplicitly]
        public void OnTrackFinished()
        {
            player.GiveControlToPlayer();
            OnDungeonFinished();
        }
    }
}