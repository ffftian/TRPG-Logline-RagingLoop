using System;
using UnityEngine;
using UnityEngine.Playables;
namespace RagingLoop
{
    [Serializable]
    public class StandSlotShowBehaviour : PlayableBehaviour
    {
        public Vector3 position;

        public int faceIndex;

        public int overallIndex;
        public int browIndex;
        public int eyeIndex;
        public int mouthIndex;

        public StandSlot StandSlot { get; set; }
        public StandSlotFace StandSlotFace { get; set; }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            StandSlotUpdate(playerData);
            StandSlotFaceUpdate(playerData);
        }

        public void StandSlotUpdate(object playerData)
        {
            if (StandSlot == null)
            {
                StandSlot = playerData as StandSlot;
                if (StandSlot == null)
                {
                    return;
                }
            }
            StandSlot.ChangeBrow(browIndex);

            if (Application.isPlaying)
            {
                StandSlot.TickEye(eyeIndex);
            }
            else
            {
                StandSlot.ChangeEye(eyeIndex);
            }

            StandSlot.ChangeMouth(mouthIndex);
        }

        public void StandSlotFaceUpdate(object playerData)
        {
            if (StandSlotFace == null)
            {
                StandSlotFace = playerData as StandSlotFace;
                if (StandSlotFace == null)
                {
                    return;
                }
            }
            StandSlotFace.ChangeFace(faceIndex);

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

