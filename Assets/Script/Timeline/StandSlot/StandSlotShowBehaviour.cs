using System;
using UnityEngine;
using UnityEngine.Playables;
namespace RagingLoop
{
    [Serializable]
    public class StandSlotShowBehaviour : PlayableBehaviour
    {
        public Vector3 position;
        public int overallIndex;
        public int browIndex;
        public int eyeIndex;
        public int mouthIndex;

        public StandSlot StandSlot { get; set; }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (StandSlot == null)
            {
                StandSlot = (StandSlot)playerData;
                if (StandSlot == null)
                {
                    return;
                }
            }
            StandSlot.ChangeBrow(browIndex);

            if(Application.isPlaying)
            {
                StandSlot.TickEye(eyeIndex);
            }
            else
            {
                StandSlot.ChangeEye(eyeIndex);
            }

            StandSlot.ChangeMouth(mouthIndex);
        }
        /// <summary>
        /// 多调几次无所谓，反正人狼村效果是瞬间切换
        /// </summary>
        /// <param name="playable"></param>
        public override void OnPlayableDestroy(Playable playable)
        {
#if UNITY_EDITOR
            if (StandSlot != null)
            {
                if (!Application.isPlaying)
                {
                    StandSlot.ChangeBrow(StandSlot.browIndex);
                    StandSlot.ChangeEye(StandSlot.eyeIndex);
                    StandSlot.ChangeMouth(StandSlot.mouthIndex);
                }
            }
#endif
        }
    }
}

