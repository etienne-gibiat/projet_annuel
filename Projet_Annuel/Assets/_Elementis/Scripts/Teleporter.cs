using System;
using System.Collections.Generic;
using PGSauce.Core.FSM.Scripted.Example;
using PGSauce.Save;
using PGSauce.Unity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Elementis.Scripts
{
    public class Teleporter : PGMonoBehaviour
    {
        public Transform player;
        public List<TeleportPoint> tpPoints;

        private void Awake()
        {
            for (var index = 0; index < tpPoints.Count; index++)
            {
                var teleportPoint = tpPoints[index];
                if (teleportPoint.conditionToTeleport && teleportPoint.conditionToTeleport.SavedValue)
                {
                    Teleport(index);
                    teleportPoint.conditionToTeleport.SaveData(false);
                    return;
                }
            }
        }

        [Button]
        public void Teleport(int index)
        {
            player.position = tpPoints[index].point.position;
            player.forward = tpPoints[index].point.forward;
        }

        [Serializable]
        public struct TeleportPoint
        {
            public Transform point;
            public SavedDataBool conditionToTeleport;
        }
    }
}