using System;
using UnityEngine.Playables;
using UnityEngine;
namespace Galgame
{
    public class FgiUnitMixBehaviour : PlayableBehaviour
    {
        public FgiUnit fgiUnit { get; set; }
        float casheWeight;

        public override void OnGraphStart(Playable playable)
        {
            base.OnGraphStart(playable);
            if (!Application.isPlaying)
            {
                fgiUnit.RefreshDress();
                fgiUnit.RefreshFace();
            }
        }

#if UNITY_EDITOR
        public override void PrepareFrame(Playable playable, FrameData info)
        {
            base.PrepareFrame(playable, info);
            //这玩意有bug
            //if (!Application.isPlaying && fgiUnits != null)
            //{
            //    int inputCount = playable.GetInputCount();
            //    float weight = 0;

            //    for (int i = 0; i < inputCount; i++)
            //    {
            //        if (playable.GetInputWeight(i) > 0)
            //        {
            //            weight = playable.GetInputWeight(i);
            //        }
            //        //Debug.Log($"权重{weight}");
            //    }
            //    if (weight == 0 && casheWeight > 0)
            //    {
            //        fgiUnits.RefreshDress();
            //        fgiUnits.RefreshFace();
            //    }
            //    casheWeight = weight;
            //}
        }
        public override void OnPlayableDestroy(Playable playable)
        {
            if (!Application.isPlaying && fgiUnit)
            {
                fgiUnit.RefreshDress();
                fgiUnit.RefreshFace();
            }
        }
#endif
    }
}
