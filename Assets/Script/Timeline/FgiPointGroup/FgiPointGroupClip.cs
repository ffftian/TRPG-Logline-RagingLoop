using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


namespace Galgame
{
    public class FgiPointGroupClip : PlayableAsset, ITimelineClipAsset
    {
        public override double duration => 0.5f;

        public FgiPointGroupBehaviour template = new FgiPointGroupBehaviour();
        public ClipCaps clipCaps => ClipCaps.None;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<FgiPointGroupBehaviour>.Create(graph, template);
        }
    }
}

