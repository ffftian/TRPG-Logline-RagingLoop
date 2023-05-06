using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;



public class ImageChangeClip : PlayableAsset, ITimelineClipAsset
{
    public override double duration => 0.5;

    public ImageChangeBehaviour template;
    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<ImageChangeBehaviour>.Create(graph, template);
    }
}

