using System;
using PGSauce.Core.Extensions;
using PGSauce.Core.PGDebugging;
using UnityEngine;

namespace PGSauce.Core.Utilities
{
    [Serializable]
    public struct Float01
    {
        [SerializeField, Range(0, 1)] private float value;
        public float Value => value;

        public Float01(float val)
        {
            if (!val.IsBetween(0, 1))
            {
                PGDebug.Message($"{val} is not between 0 and 1, clamping float").LogWarning();
            }
            value = Mathf.Clamp01(val);
        }
        
        public static implicit operator Float01(float d) => new Float01(d);
        public static implicit operator float(Float01 d) => d.Value;
    }
}