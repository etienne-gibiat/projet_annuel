using System;
using PGSauce.Core.Utilities;
using PGSauce.Save;
using UnityEngine;

namespace _Elementis.Scripts
{
    public abstract class DungeonManager<T> : MonoSingleton<T> where T : MonoSingletonBase
    {
        [SerializeField]
        private SavedDataBool dungeonFinished;
        [SerializeField]
        private SavedDataBool dungeonJustFinished;

        public void OnDungeonFinished()
        {
            dungeonFinished.SaveData(true);
            dungeonJustFinished.SaveData(true);
        }
    }
}