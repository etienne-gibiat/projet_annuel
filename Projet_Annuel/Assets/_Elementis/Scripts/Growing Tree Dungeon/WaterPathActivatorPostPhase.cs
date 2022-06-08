using System;
using System.Collections;
using UnityEngine;

namespace _Elementis.Scripts.Growing_Tree_Dungeon
{
    public abstract class WaterPathActivatorPostPhase : MonoBehaviour
    {
        public abstract IEnumerator DoPostPhase(WaterPathActivator waterPathActivator, Action onPostPhaseEnd);
    }
}