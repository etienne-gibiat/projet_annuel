using System;
using System.Collections;
using UnityEngine;

namespace _Elementis.Scripts.Growing_Tree_Dungeon
{
    public abstract class WaterPathActivatorPrePhaseBase : MonoBehaviour
    {
        public abstract IEnumerator DoPrePhase(WaterPathActivator waterPathActivator, Action onPrephaseEnd);
    }
}