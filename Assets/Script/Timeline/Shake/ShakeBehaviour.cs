using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class ShakeBehaviour : PlayableBehaviour
{
    public Vector2 shakeRange;
    public RectTransform component;


    //public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    //{
    //    if (component == null)
    //    {
    //        component = playerData as RectTransform;
    //        if (component == null)
    //        {
    //            return;
    //        }
    //    }
    //    base.ProcessFrame(playable, info, playerData);


    //}
    //public override void OnPlayableDestroy(Playable playable)
    //{
    //    base.OnPlayableDestroy(playable);
    //}
}

