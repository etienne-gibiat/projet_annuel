using PGSauce.Unity;
using UnityEngine;

namespace PGSauce.Gameplay.MiniGames
{
    public abstract class MiniGameRewardBase : PGMonoBehaviour
    {
        [SerializeField, Tooltip("How likely is this reward to be chosen")] private float probability = 1;
        [SerializeField] private bool attachToCaller = true;
        
        public float Probability => probability;
        
        public void Show ( Transform parent )
        {
            gameObject.SetActive (true);
			
            if (attachToCaller && parent)
                transform.position = parent.position;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}