using System;
using System.Collections.Generic;
using PGSauce.Unity;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public class WfcChest : PGMonoBehaviour
    {
        public List<WfcTileBase> tilesUnlocked;
        public SavedDataWfcChest data;
        public new AudioSource audio;

        public GameObject opened;
        public GameObject closed;
        private void Start()
        {
            if (Opened)
            {
                opened.SetActive(true);
                closed.SetActive(false);
                WfcCastle.Instance.OnChestOpen(tilesUnlocked);
            }
            else
            {
                opened.SetActive(false);
                closed.SetActive(true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Opened)
            {
                return;
            }
            
            if (other.gameObject.layer == Layers.PLAYER)
            {
                Open();
            }
        }

        private void Open()
        {
            opened.SetActive(true);
            closed.SetActive(false);
            audio.Play();
            data.SaveData(new WfcChestData(){opened = true});
            WfcCastle.Instance.OnChestOpen(tilesUnlocked);
        }

        private bool Opened => data.SavedValue.opened;
    }
}