using PGSauce.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace PGSauce.Gameplay.MiniGames
{
    public abstract class MiniGameReward<T> : MiniGameRewardBase where T : MiniGameBase
    {
        [SerializeField] private UnityEvent<T> onRewarded;
        public void Execute(T game, Transform parent)
        {
            Show(parent);
            onRewarded.Invoke(game);
            CustomExecute(game, parent);
        }

        protected virtual void CustomExecute(T game, Transform parent)
        {
        }
    }
}