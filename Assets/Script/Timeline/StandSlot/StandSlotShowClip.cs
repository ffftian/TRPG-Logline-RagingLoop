using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


namespace RagingLoop
{
    public class StandSlotShowClip : PlayableAsset, ITimelineClipAsset
    {
        public override double duration => 0.5f;
        public StandSlotShowBehaviour template;
        public ClipCaps clipCaps => ClipCaps.Extrapolation;//外推启用

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<StandSlotShowBehaviour>.Create(graph, template);
        }
    }
}

