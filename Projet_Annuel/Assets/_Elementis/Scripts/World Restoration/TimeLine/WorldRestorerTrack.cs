using UnityEngine.Timeline;

namespace _Elementis.Scripts.World_Restoration.TimeLine
{
    [TrackBindingType(typeof(WorldRestorationZone))]
    [TrackClipType(typeof(WorldRestorerClip))]
    public class WorldRestorerTrack : TrackAsset
    {
    }
}