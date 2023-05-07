using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;



public class ShakeClip : PlayableAsset, ITimelineClipAsset
{
    public ShakeBehaviour template;
    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<ShakeBehaviour>.Create(graph, template);
    }
}

