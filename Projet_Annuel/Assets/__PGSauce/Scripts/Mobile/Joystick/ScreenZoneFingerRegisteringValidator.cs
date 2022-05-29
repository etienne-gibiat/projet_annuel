using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Mobile;
using System;
using PGSauce.Core.Strings;

namespace PGSauce.Mobile
{
    [CreateAssetMenu(menuName = MenuPaths.MenuBase + "Mobile/Finger Registering Validator/Screen Zone")]
    public class ScreenZoneFingerRegisteringValidator : FingerRegisteringValidator
    {
        private enum CompareMode { GREATER, LESS }
        [SerializeField, Range(0, 1)] private float xPercentage;
        [SerializeField, Range(0, 1)] private float yPercentage;
        [SerializeField] private CompareMode xPercentageCompareMode = CompareMode.GREATER;
        [SerializeField] private CompareMode yPercentageCompareMode = CompareMode.GREATER;
        public override bool Validate(Lean.Touch.LeanFinger finger)
        {
            float w = Screen.width;
            float h = Screen.height;
            Vector2 screenPos = finger.ScreenPosition;
            Vector2 normalizedScreenPos = new Vector2(screenPos.x / w, screenPos.y / h);

            bool xOk = false;
            bool yOk = false;

            xOk = CompareValue(xPercentageCompareMode, xPercentage, normalizedScreenPos.x);
            yOk = CompareValue(yPercentageCompareMode, yPercentage, normalizedScreenPos.y);

            return xOk && yOk;
        }

        private bool CompareValue(CompareMode compareMode, float threshold, float val)
        {
            switch (compareMode)
            {
                case CompareMode.GREATER:
                    return threshold < val;
                case CompareMode.LESS:
                    return threshold > val;
            }

            throw new UnityException("The compare mode is invalid " + compareMode);
        }
    }
}
