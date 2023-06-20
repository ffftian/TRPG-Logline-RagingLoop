using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace RagingLoop
{
    [TrackBindingType(typeof(StandSlotBase))]
    [TrackClipType(typeof(StandSlotShowClip))]
    public class StandSlotTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            PlayableDirector playableDirector = go.GetComponent<PlayableDirector>();
            StandSlotBase slot = (StandSlotBase)playableDirector.GetGenericBinding(this);
            foreach (var clip in GetClips())
            {
                StandSlotShowClip slotClip = (clip.asset as StandSlotShowClip);
                if (slotClip != null)
                {
                    if (slot is StandSlot)
                    {
                        slotClip.template.StandSlot = slot as StandSlot;
                    }
                    else if (slot is StandSlotFace)
                    {
                        slotClip.template.StandSlotFace = slot as StandSlotFace;
                    }
                }
            }
            return base.CreateTrackMixer(graph, go, inputCount);
        }
    }
}

