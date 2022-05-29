using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;

namespace PGSauce.Mobile
{
    public abstract class FingerRegisteringValidator : ScriptableObject
    {
        public abstract bool Validate(LeanFinger finger);
    }
}
