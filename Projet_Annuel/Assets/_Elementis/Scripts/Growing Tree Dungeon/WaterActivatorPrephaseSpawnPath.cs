using System;
using System.Collections;
using _Elementis.Scripts.River_Editor;
using DG.Tweening;
using MonKey.Extensions;
using PGSauce.Animation;
using PGSauce.Core.Extensions;
using UnityEngine;

namespace _Elementis.Scripts.Growing_Tree_Dungeon
{
    public class WaterActivatorPrephaseSpawnPath : WaterPathActivatorPrePhaseBase
    {
        public GameObject waterPath;
        public Transform waterPointEndTarget;
        public float waterPointEndLerp;
        public Transform start, end;
        public DotweenProfile animationProfile;
        public RiverPath river;

        private void Awake()
        {
            waterPath.SetActive(false);
        }

        public override IEnumerator DoPrePhase(WaterPathActivator waterPathActivator, Action onPrephaseEnd)
        {
            waterPath.SetActive(true);
            waterPath.transform.position = start.position;
            yield return waterPath.transform.DOMove(end.position, animationProfile.Duration)
                .SetAs(animationProfile.Params)
                .OnUpdate((() =>
                {
                    var currentSplineLocalPos = river.controlPoints[river.controlPoints.Count - 1];
                    var targetWorldPos = waterPointEndTarget.position;
                    var targetLocalPos = river.transform.InverseTransformPoint(targetWorldPos);
                    var targetControlPoint = new Vector4(targetLocalPos.x, targetLocalPos.y, targetLocalPos.z,
                        currentSplineLocalPos.position.w);
                    var currentSplinePos = Vector4.Lerp(currentSplineLocalPos.position, targetControlPoint, waterPointEndLerp);
                    river.controlPoints[river.controlPoints.Count - 1].position = currentSplinePos;
                    river.GenerateRiver();
                    waterPathActivator.SetLookAtPosition(waterPath.transform.position);
                })).WaitForCompletion();
            
            
            
            var currentSplineLocalPos = river.controlPoints[river.controlPoints.Count - 1];
            var targetWorldPos = waterPointEndTarget.position;
            var targetLocalPos = river.transform.InverseTransformPoint(targetWorldPos);
            var targetControlPoint = new Vector4(targetLocalPos.x, targetLocalPos.y, targetLocalPos.z,
                currentSplineLocalPos.position.w);
            river.controlPoints[river.controlPoints.Count - 1].position = targetControlPoint;
            river.GenerateRiver();
            onPrephaseEnd();
        }
    }
}