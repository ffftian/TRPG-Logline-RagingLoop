using System;
using UnityEngine;
using UnityEngine.Playables;

namespace RagingLoop
{
    /// <summary>
    /// 绑定立绘
    /// </summary>
    [Serializable]
    public class StandSlotPointGroupBehaviour : PlayableBehaviour
    {
        public StandSlotPointGroup standSlotPointGroup { get; set; }

        [HideInInspector]
        public int[] occupyStandSlot = new int[4] {999,999,999,999 };//临时的，这样没准有用


        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);
            if (standSlotPointGroup == null)
            {
                standSlotPointGroup = (StandSlotPointGroup)playerData;
            }
            if (occupyStandSlot.Length == 0) return;

            if (Application.isPlaying)
            {
                for (int i = 0; i < standSlotPointGroup.standSlotGroup.Length; i++)
                {
                    standSlotPointGroup.FreeSlot(i);
                }
                for (int i = 0; i < standSlotPointGroup.occupyStandSlot.Length; i++)
                {
                    if (occupyStandSlot[i] < standSlotPointGroup.standSlotGroup.Length)
                    {
                        standSlotPointGroup.BindSlot(occupyStandSlot[i], i);
                    }
                    //else
                    //{
                    //    standSlotPointGroup.FreeSlot(i);
                    //}
                }
            }
            else
            {
                for (int i = 0; i < standSlotPointGroup.standSlotGroup.Length; i++)
                {
                    standSlotPointGroup.FreeSlot(i);
                }

                for (int i = 0; i < standSlotPointGroup.occupyStandSlot.Length; i++)
                {
                    if (occupyStandSlot.Length==0) return;

                    if (occupyStandSlot[i] < standSlotPointGroup.standSlotGroup.Length)
                    {
                        standSlotPointGroup.BindSlot(occupyStandSlot[i], i);
                    }
                }
            }
        }
        public override void OnPlayableDestroy(Playable playable)
        {
            base.OnPlayableDestroy(playable);

        }
    }

}