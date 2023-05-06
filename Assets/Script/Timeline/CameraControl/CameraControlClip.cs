using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


namespace Galgame
{
    public class CameraControlClip : PlayableAsset, ITimelineClipAsset
    {
        public CameraControlBehaviour template;
        public ClipCaps clipCaps => ClipCaps.Blending | ClipCaps.Extrapolation;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<CameraControlBehaviour>.Create(graph, template);
        }
    }
}

