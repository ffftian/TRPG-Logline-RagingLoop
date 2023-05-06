using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace Galgame
{
    [TrackBindingType(typeof(FgiUnit))]
    [TrackClipType(typeof(FgiUnitClip))]
    public class FgiUnitTrack : TrackAsset, IRecordTack
    {
        public object SaveValue
        {
            get
            {
                object lastFgi = null;
                double endTime = 0;
                foreach (var clip in GetClips())
                {

                    if (endTime < clip.end)
                    {
                        endTime = clip.end;

                        lastFgi = clip.asset;
                    }
                }
                return lastFgi;
            }
        }

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            //FgiUnit fgiUnits = FgiUnit.Resolve(graph.GetResolver());
            //foreach (var c in GetClips())
            //{
            //    (c.asset as FgiUnitClip).fgiUnits = fgiUnits;
            //}
            PlayableDirector playableDirector = go.GetComponent<PlayableDirector>();
            FgiUnit fgiUnit = (FgiUnit)playableDirector.GetGenericBinding(this);
            foreach (var clip in GetClips())
            {
                (clip.asset as FgiUnitClip).template.fgiUnit = fgiUnit;
            }
            ScriptPlayable<FgiUnitMixBehaviour> mixer = ScriptPlayable<FgiUnitMixBehaviour>.Create(graph, inputCount);
            mixer.GetBehaviour().fgiUnit = fgiUnit;
            return mixer;
        }
        protected override void OnCreateClip(TimelineClip clip)//这会导致创建的时候根本没绑定啊
        {
            base.OnCreateClip(clip);

        }

        //protected override Playable CreatePlayable(PlayableGraph graph, GameObject gameObject, TimelineClip clip)
        //{
        //    ScriptPlayable<FgiUnitBehaviour> playable = (ScriptPlayable<FgiUnitBehaviour>)base.CreatePlayable(graph, gameObject, clip);
        //    return playable;
        //}
    }
}

