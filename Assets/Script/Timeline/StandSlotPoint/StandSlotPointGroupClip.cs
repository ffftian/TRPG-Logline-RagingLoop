using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


namespace RagingLoop
{
    public class StandSlotPointGroupClip : PlayableAsset, ITimelineClipAsset
    {
        public override double duration => 0.5;

        public StandSlotPointGroupBehaviour template;
        public ClipCaps clipCaps => ClipCaps.None;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<StandSlotPointGroupBehaviour>.Create(graph, template);
        }
    }
}

