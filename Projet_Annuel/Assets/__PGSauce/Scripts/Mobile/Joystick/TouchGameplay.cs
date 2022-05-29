using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using PGSauce.Unity;

namespace PGSauce.Mobile
{
    public abstract class TouchGameplay : PGMonoBehaviour
    {
        public abstract void OnMoveStart(LeanFinger leanFinger);
        public abstract void OnMove(LeanFinger leanFinger, Vector2 delta);
        public abstract void OnMoveEnd(LeanFinger leanFinger);
        public abstract void OnShortTap(LeanFinger finger);
    }
}
