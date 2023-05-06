using UnityEngine.Playables;
using UnityEngine;
using UnityEngine.Timeline;
namespace RagingLoop
{

    [TrackBindingType(typeof(StandSlotPointGroup))]
    [TrackClipType(typeof(StandSlotPointGroupClip))]
    public class StandSlotPointGroupTrack : TrackAsset
    {
        
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            PlayableDirector playableDirector = go.GetComponent<PlayableDirector>();
            StandSlotPointGroup slot = (StandSlotPointGroup)playableDirector.GetGenericBinding(this);
            foreach (var clip in GetClips())
            {
                StandSlotPointGroupClip slotClip = (clip.asset as StandSlotPointGroupClip);
                if (slotClip != null)
                {
                    slotClip.template.standSlotPointGroup = slot;
                }
            }
            return base.CreateTrackMixer(graph, go, inputCount);
        }
    }
}
