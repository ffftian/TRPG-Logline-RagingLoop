using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;



public class RectTransformActClip : PlayableAsset, ITimelineClipAsset
{
    public RectTransformActBehaviour template;
    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<RectTransformActBehaviour>.Create(graph, template);
    }
}

