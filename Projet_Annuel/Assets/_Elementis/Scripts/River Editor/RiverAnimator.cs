using System;
using DG.Tweening;
using UnityEngine;

namespace _Elementis.Scripts.River_Editor
{
    public class RiverAnimator : MonoBehaviour
    {
        public RiverPath river;
        public float duration;

        private void Start()
        {
            DOVirtual.Float(0, 1, duration, value =>
            {
                river.UpdateRiver(value);
            }).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }
    }
}