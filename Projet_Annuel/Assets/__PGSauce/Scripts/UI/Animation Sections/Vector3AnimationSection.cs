using System;
using UnityEngine;

namespace PGSauce.UI
{
    [Serializable]
    public class Vector3AnimationSection : AnimationSection<Vector3>
    {
        protected override Vector3 Lerp(Vector3 startVal, Vector3 endVal, float t)
        {
            return Vector3.LerpUnclamped(startVal, endVal, t);
        }
    }
}