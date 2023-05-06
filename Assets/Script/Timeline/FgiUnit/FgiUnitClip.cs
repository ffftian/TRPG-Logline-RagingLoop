using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


namespace Galgame
{
    public class FgiUnitClip : PlayableAsset, ITimelineClipAsset
    {
        public override double duration => 0.5f;
        public FgiIndex fgiIndex;
        public FgiUnitBehaviour template = new FgiUnitBehaviour();
        #region FgiIndexGroup
        public int assetIndex
        {
            get
            {
                return fgiIndex.assetIndex;
            }
            set
            {
                fgiIndex.assetIndex = value;
            }
        }
        public int dressIndex
        {
            get
            {
                return fgiIndex.dressIndex;
            }
            set
            {
                fgiIndex.dressIndex = value;
            }
        }
        public int dressTypeIndex
        {
            get
            {
                return fgiIndex.dressTypeIndex;
            }
            set
            {
                fgiIndex.dressTypeIndex = value;
            }
        }
        public int faceGroupIndex
        {
            get
            {
                return fgiIndex.faceGroupIndex;
            }
            set
            {
                fgiIndex.faceGroupIndex = value;
            }
        }
        public int faceIndex
        {
            get
            {
                return fgiIndex.faceIndex;
            }
            set
            {
                fgiIndex.faceIndex = value;
            }
        }
        #endregion

        public ClipCaps clipCaps => ClipCaps.None;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<FgiUnitBehaviour>.Create(graph, template);
            //FgiUnitBehaviour fgiUnitBehaviour =  playable.GetBehaviour();
            //fgiUnitBehaviour.fgiUnits = fgiUnits;

            return playable;
        }
    }
}
