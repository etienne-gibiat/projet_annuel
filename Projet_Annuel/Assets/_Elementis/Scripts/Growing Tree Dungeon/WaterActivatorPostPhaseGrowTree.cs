using System;
using System.Collections;
using _Elementis.Scripts.Procedural_Trees;
using UnityEngine;

namespace _Elementis.Scripts.Growing_Tree_Dungeon
{
    public class WaterActivatorPostPhaseGrowTree : WaterPathActivatorPostPhase
    {
        public TreeGenerator tree;
        
        public override IEnumerator DoPostPhase(WaterPathActivator waterPathActivator, Action onPostPhaseEnd)
        {
            tree.GrowNextThird();

            while (!tree.IsCurrentThirdFinished)
            {
                yield return new WaitForEndOfFrame();
            }
            
            onPostPhaseEnd();
        }
    }
}