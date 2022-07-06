using System;
using System.Collections.Generic;
using System.Linq;
using _Elementis.Scripts.Character_Controller;
using DG.Tweening;
using JetBrains.Annotations;
using PGSauce.AudioManagement;
using PGSauce.Save;
using PGSauce.Unity;
using UnityEngine;
using UnityEngine.Playables;

namespace _Elementis
{
    public class LastDungeonSpawner : PGMonoBehaviour
    {
        public List<SavedDataBool> conditions;
        public SavedDataBool alreadyShownCinematic;
        [SerializeField] private PlayableDirector director;
        [SerializeField] private GameObject dungeon;
        [SerializeField] private ElementisCharacterController player;

        private void Awake()
        {
            var canShow = conditions.All(c => c.SavedValue);
            if (canShow)
            {
                if (alreadyShownCinematic.SavedValue)
                {
                    dungeon.SetActive(true);
                }
                else
                {
                    DOVirtual.DelayedCall(5, () =>
                    {
                        player.TakeControlFromPlayer();
                        director.Play();
                    }, false);
                }
            }
            else
            {
                dungeon.SetActive(false);
            }
        }
        
        [UsedImplicitly]
        public void OnTrackFinished()
        {
            alreadyShownCinematic.SaveData(true);
            player.GiveControlToPlayer();
        }
    }
}