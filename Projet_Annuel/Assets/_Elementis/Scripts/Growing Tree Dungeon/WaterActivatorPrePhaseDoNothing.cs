using System;
using System.Collections;

namespace _Elementis.Scripts.Growing_Tree_Dungeon
{
    public class WaterActivatorPrePhaseDoNothing : WaterPathActivatorPrePhaseBase
    {
        public override IEnumerator DoPrePhase(WaterPathActivator waterPathActivator, Action onPrephaseEnd)
        {
            if (false)
            {
                yield return null;
            }

            onPrephaseEnd();
        }
    }
}