using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PGSauce.PGEvents.Example
{
    public class Enemy : MonoBehaviour
    {
        public Player firstPlayer;
        public Player secondPlayer;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            var player = Random.Range(0, 2) == 1 ? firstPlayer : secondPlayer;
                
            player.TakeDamage(Random.Range(5, 50));
        }
    }
}


