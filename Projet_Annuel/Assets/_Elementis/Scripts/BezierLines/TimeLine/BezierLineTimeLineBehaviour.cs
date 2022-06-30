using UnityEngine;
using UnityEngine.Playables;

namespace _Elementis.Scripts.BezierLines.TimeLine
{
    public class BezierLineTimeLineBehaviour : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var bezier = playerData as BezierLine;

            if (!bezier)
            {
                return;
            }

            var t = 0f;

            if (info.weight > 0f)
            {
                //var bezierBehaviour = (ScriptPlayable<BezierLineTimeLineBehaviour>) playable.GetInput(i);
                t = (float)(playable.GetTime() / playable.GetDuration());
            }
            bezier.UpdateBezier(Curve.Evaluate(t));
        }

        public AnimationCurve Curve { get; set; }
    }
}