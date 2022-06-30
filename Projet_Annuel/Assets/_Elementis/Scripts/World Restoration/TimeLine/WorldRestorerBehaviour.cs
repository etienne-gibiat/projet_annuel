using UnityEngine;
using UnityEngine.Playables;

namespace _Elementis.Scripts.World_Restoration.TimeLine
{
    public class WorldRestorerBehaviour  : PlayableBehaviour
    {
        public AnimationCurve Curve { get; set; }
        
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var bezier = playerData as WorldRestorationZone;

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
            bezier.UpdateRadius(Curve.Evaluate(t));
        }
    }
}