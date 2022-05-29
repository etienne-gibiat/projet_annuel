using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using PGSauce.Core.Utilities;

namespace PGSauce.Mobile
{
    public class JoystickManager : MonoSingleton<JoystickManager>
    {
        private HashSet<LeanFinger> fingers;
        public override void Init()
        {
            base.Init();
            fingers = new HashSet<LeanFinger>();
        }

        public bool IsFingerUsed(LeanFinger finger)
        {
            if (fingers == null)
            {
                return true;
            }
            return fingers.Contains(finger);
        }

        public void RegisterFinger(LeanFinger finger)
        {
            fingers?.Add(finger);
        }

        public void UnregisterFinger(LeanFinger finger)
        {
            fingers?.Remove(finger);
        }
    }
}
