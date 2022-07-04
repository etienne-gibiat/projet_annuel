using System;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Utilities;
using PGSauce.Unity;
using UnityEngine;

namespace _Elementis.Scripts.Ice_Dungeon
{
    public class AvalancheTrigger : PGMonoBehaviour
    {
        private bool _triggered;

        private void Awake()
        {
            _triggered = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            PGDebug.Message($"ON trigger").Log();
            if (other.gameObject.layer == Layers.PLAYER)
            {
                if (!_triggered)
                {
                    _triggered = true;
                    IceDungeonManager.Instance.TriggerAvalanche();
                }
            }
        }
    }
}