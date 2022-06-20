using System;
using PGSauce.AudioManagement;
using UnityEngine;

namespace _Elementis.Scripts.Growing_Tree_Dungeon
{
    public class GrowingTreeDungeonManager : DungeonManager
    {
        [SerializeField] private PgMusicTrack ambient;

        private void Start()
        {
            PgAudioManager.Music.Play(ambient, 2f);
        }
    }
}