using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PGSauce.PGEvents.Example
{
    public class UI : MonoBehaviour
    {
        public Text hpText;

        private void Awake()
        {
            hpText.text = "";
        }

        public void OnPlayerDamageTaken(int damage, Player player)
        {
            hpText.text = $"{damage} DMG Taken by player {player.name}";
        }
    }
}


