using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace RagingLoop
{
    [TrackBindingType(typeof(StandSlot))]
    [TrackClipType(typeof(StandSlotShowClip))]
    public class StandSlotTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            PlayableDirector playableDirector = go.GetComponent<PlayableDirector>();
            StandSlot slot = (StandSlot)playableDirector.GetGenericBinding(this);
            foreach (var clip in GetClips())
            {
                StandSlotShowClip slotClip = (clip.asset as StandSlotShowClip);
                if (slotClip != null)
                {
                    slotClip.template.StandSlot = slot;
                }
            }
            return base.CreateTrackMixer(graph, go, inputCount);
        }
    }
}

