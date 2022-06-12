﻿using System;
using System.Collections;
using _Elementis.Scripts.Procedural_Trees;
using Cinemachine;
using UnityEngine;

namespace _Elementis.Scripts.Growing_Tree_Dungeon
{
    public class WaterActivatorPostPhaseGrowTree : WaterPathActivatorPostPhase
    {
        public float waitAfterGrowth = 0.75f;
        public TreeGenerator tree;
        public CinemachineVirtualCamera cam;
        
        public override IEnumerator DoPostPhase(WaterPathActivator waterPathActivator, Action onPostPhaseEnd)
        {
            cam.Priority = 10;
            tree.GrowNextThird();

            while (!tree.IsCurrentThirdFinished)
            {
                cam.LookAt.position = tree.PointOfInterest;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(waitAfterGrowth);

            cam.Priority = 0;
            onPostPhaseEnd();
        }
    }
}