using System;
using System.Collections;
using _Elementis.Scripts.Procedural_Trees;
using UnityEngine;

namespace _Elementis.Scripts.Growing_Tree_Dungeon
{
    public abstract class WaterPathActivatorPostPhase : MonoBehaviour
    {
        public abstract IEnumerator DoPostPhase(WaterPathActivator waterPathActivator, Action<TreeGenerator> onPostPhaseEnd);
    }
}