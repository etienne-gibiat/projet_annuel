using UnityEngine;
using UnityEngine.Playables;

namespace _Elementis.Scripts.BezierLines.TimeLine
{
    public class BezierLineClip : PlayableAsset
    {
        public AnimationCurve evaluateCurve = AnimationCurve.Linear(0,0,1,1);
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<BezierLineTimeLineBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();

            //behaviour.t = 0f;
            behaviour.Curve = evaluateCurve;
            
            return playable;
        }
    }
}