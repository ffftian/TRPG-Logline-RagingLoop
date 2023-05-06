using MiaoTween;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace Galgame
{
    [TrackBindingType(typeof(Camera))]
    [TrackClipType(typeof(CameraControlClip))]
    public class CameraControlTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<CameraControlMixBehaviour>.Create(graph, inputCount);
        }
    }
}

