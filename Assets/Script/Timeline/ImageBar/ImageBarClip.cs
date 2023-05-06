using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;



public class ImageBarClip : PlayableAsset, ITimelineClipAsset
{
    public ImageBarBehaviour template;
    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<ImageBarBehaviour>.Create(graph, template);
    }
}

