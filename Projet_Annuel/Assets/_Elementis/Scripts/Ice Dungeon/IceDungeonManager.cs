using _Elementis.Scripts.Character_Controller;
using _Elementis.Scripts.Spells;
using DG.Tweening;
using JetBrains.Annotations;
using PGSauce.Animation;
using PGSauce.AudioManagement;
using PGSauce.Save;
using UnityEngine;
using UnityEngine.Playables;

namespace _Elementis.Scripts.Ice_Dungeon
{
    public class IceDungeonManager : DungeonManager<IceDungeonManager>
    {
        [SerializeField] private PlayableDirector director;
        [SerializeField] private ElementisCharacterController player;
        [SerializeField] private Transform storm;
        [SerializeField] private PgMusicTrack avalancheSound;
        [SerializeField] private Transform stormGoal;
        [SerializeField] private DotweenProfile stormMove;
        [SerializeField] private GameObject terrainAfterAvalanche;
        [SerializeField] private GameObject terrainBeforeAvalanche;
        

        public override void Init()
        {
            base.Init();
            player.InSnow = true;
        }

        public void TriggerAvalanche()
        {
            player.TakeControlFromPlayer();
            director.Play();
        }
        
        [UsedImplicitly]
        public void OnTrackFinished()
        {
            PgAudioManager.Music.Play(avalancheSound, 0f, 1f);
            player.GiveControlToPlayer();
            storm.DOMove(stormGoal.position, stormMove.Duration).SetAs(stormMove.Params).OnComplete(() =>
            {
                PgAudioManager.Music.Stop(avalancheSound, 1.5f);
                terrainAfterAvalanche.SetActive(true); 
                terrainBeforeAvalanche.SetActive(false);
                storm.gameObject.SetActive(false);
            });
        }

        public void OnSpellCollected(SpellData spell)
        {
            OnDungeonFinished();
        }
    }
}