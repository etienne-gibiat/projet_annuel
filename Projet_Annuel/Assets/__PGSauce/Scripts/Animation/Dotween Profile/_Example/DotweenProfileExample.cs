using System;
using DG.Tweening;
using PGSauce.Unity;
using UnityEngine;

namespace PGSauce.Animation.Example
{
    /// <summary>
    /// A test class to test dotween profiles
    /// We have 3 objects
    /// The 1st one is scaled up and down
    /// The 2nd is rotating
    /// The last one is moving
    /// </summary>
    public class DotweenProfileExample : PGMonoBehaviour
    {
        [SerializeField] private Transform scaled;
        [SerializeField] private Transform rotated;
        [SerializeField] private Transform moved;
        [SerializeField] private DotweenProfile scaleProfile;
        [SerializeField] private DotweenProfile rotatedProfile;
        [SerializeField] private DotweenProfile movedProfile;

        private void Awake()
        {
            scaled.DOScale(Vector3.one * 2, scaleProfile.Duration).SetAs(scaleProfile.Params);
            rotated.DOLocalRotate(new Vector3(360f, 0, 0), rotatedProfile.Duration, RotateMode.FastBeyond360).SetAs(rotatedProfile.Params);
            moved.DOMoveY(5, movedProfile.Duration).SetAs(movedProfile.Params);
        }
    }
}