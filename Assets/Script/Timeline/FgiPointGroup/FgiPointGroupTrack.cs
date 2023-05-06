using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace Galgame
{
    [TrackBindingType(typeof(FgiPointGroup))]
    [TrackClipType(typeof(FgiPointGroupClip))]
    public class FgiPointGroupTrack : TrackAsset, IRecordTack
    {
        public object SaveValue
        {
            get
            {
                object lastClip = null;
                double endTime = 0;
                foreach (var clip in GetClips())
                {

                    if (endTime < clip.end)
                    {
                        endTime = clip.end;

                        lastClip = clip.asset;
                    }
                }
                return lastClip;
            }
        }

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            PlayableDirector playableDirector = go.GetComponent<PlayableDirector>();
            FgiPointGroup fgi = (FgiPointGroup)playableDirector.GetGenericBinding(this);
            foreach (var clip in GetClips())
            {
                (clip.asset as FgiPointGroupClip).template.fgiPointGroup = fgi;
            }

            return base.CreateTrackMixer(graph, go, inputCount);
        }
    }
}

