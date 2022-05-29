using System;
using UnityEngine;

namespace PGSauce.UI
{
    [Serializable]
    public class FloatAnimationSection : AnimationSection<float>
    {
        protected override float Lerp(float startVal, float endVal, float t)
        {
            return Mathf.LerpUnclamped(startVal, endVal, t);
        }
    }
}