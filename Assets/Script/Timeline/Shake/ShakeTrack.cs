using Dice;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackBindingType(typeof(RectTransform))]
[TrackClipType(typeof(ShakeClip))]
public class ShakeTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        ScriptPlayable<ShakeMixBehaviour> scriptPlayable = ScriptPlayable<ShakeMixBehaviour>.Create(graph, inputCount);
        return scriptPlayable;
    }

}

