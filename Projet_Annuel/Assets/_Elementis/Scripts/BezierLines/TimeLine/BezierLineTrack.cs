using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace _Elementis.Scripts.BezierLines.TimeLine
{
    [TrackBindingType(typeof(BezierLine))]
    [TrackClipType(typeof(BezierLineClip))]
    public class BezierLineTrack : TrackAsset
    {
    }
}