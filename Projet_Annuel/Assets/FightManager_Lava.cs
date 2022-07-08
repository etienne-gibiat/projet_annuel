using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Elementis.Scripts.Lava_Dungeon {
    public class FightManager_Lava : MonoBehaviour
    {

        public GameObject fight1;
        public GameObject fight2;
        public GameObject dragon;
        private Animator anim;
        public LavaDungeonManager manager;
        // Start is called before the first frame update
        void Start()
        {
            anim = dragon.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (fight1.transform.childCount == 0 && fight1.active)
            {
                fight1.SetActive(false);
                fight2.SetActive(true);
            }
            if (fight2.transform.childCount == 0 && fight2.active)
            {
                fight2.SetActive(false);
                anim.SetBool("isReady", true);
            }
            if (dragon.GetComponent<BotControl>().EnnemyHealth <= 0)
            {
                manager.OnFightFinished();
            }
        }

        public void startFight()
        {
            fight1.SetActive(true);
        }
    }
}