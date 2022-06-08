using System;
using System.Collections;
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
        public RamSpline spline;

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
                    var currentSplineLocalPos = spline.controlPoints[spline.controlPoints.Count - 1];
                    var targetWorldPos = waterPointEndTarget.position;
                    var targetLocalPos = spline.transform.InverseTransformPoint(targetWorldPos);
                    var targetControlPoint = new Vector4(targetLocalPos.x, targetLocalPos.y, targetLocalPos.z,
                        currentSplineLocalPos.w);
                    var currentSplinePos = Vector4.Lerp(currentSplineLocalPos, targetControlPoint, waterPointEndLerp);
                    spline.controlPoints[spline.controlPoints.Count - 1] = currentSplinePos;
                    spline.GenerateSpline();
                    waterPathActivator.SetLookAtPosition(waterPath.transform.position);
                })).WaitForCompletion();
            
            
            
            var currentSplineLocalPos = spline.controlPoints[spline.controlPoints.Count - 1];
            var targetWorldPos = waterPointEndTarget.position;
            var targetLocalPos = spline.transform.InverseTransformPoint(targetWorldPos);
            var targetControlPoint = new Vector4(targetLocalPos.x, targetLocalPos.y, targetLocalPos.z,
                currentSplineLocalPos.w);
            spline.controlPoints[spline.controlPoints.Count - 1] = targetControlPoint;
            spline.GenerateSpline();
            onPrephaseEnd();
        }
    }
}