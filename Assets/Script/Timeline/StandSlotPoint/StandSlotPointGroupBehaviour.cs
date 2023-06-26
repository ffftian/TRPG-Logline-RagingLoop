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

        [HideInInspector]//临时的，设置占用格子
        public int[] occupyStandSlot = new int[7] {999,999,999,999,999,999,999 };

        public int[] slotIndexs = new int[7] { 0, 0, 0, 0, 0, 0, 0 };//每个格子附带的小序号

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
                        standSlotPointGroup.BindSlot(occupyStandSlot[i], i, slotIndexs[i]);
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
                        standSlotPointGroup.BindSlot(occupyStandSlot[i], i, slotIndexs[i]);
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