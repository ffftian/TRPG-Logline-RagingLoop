using Spine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Timeline;
namespace RagingLoop
{
    public class RagingLoopComponent : LoglineComponent
    {
        public RectTransform NamePanel;
        public CommonTextAsset messageAsset;
        public List<StandSlot> characters = new List<StandSlot>();
        public StandSlotPointGroup StandSlotPointGroup;
        public TyperDialogue dialogueCN;

        public override string CurrentRoleName => messageAsset.messageDataList[serialPtr].name;

        public override int? messageAssetLength => messageAsset?.messageDataList.Count;

        public override void Start()
        {
            base.Start();
        }

        public override void SelectMessage(int Serial)
        {
            CommonTextData serialData = messageAsset.messageDataList[Serial];
            SetNameText(serialData.name);
            string timeLinePath = $@"{QQLogSettings.TimeLineDirectory}\{messageAsset.name}\{serialData.SaveID}";
            useTimeLineAsset = Resources.Load<TimelineAsset>(timeLinePath);
            playable.playableAsset = useTimeLineAsset;
#if UNITY_EDITOR//预赋值设置

            if(useTimeLineAsset.GetRootTrack(0) is MarkerTrack)
            {
                return;
            }
            if (playable.GetGenericBinding(useTimeLineAsset.GetRootTrack(0)) == null)
            {
                if (useTimeLineAsset.GetRootTrack(0) is StandSlotTrack)
                {
                   int groupID =  int.Parse(serialData.GroupID);
                    if (groupID < characters.Count && groupID > 0)
                    {
                        StandSlot standSlot = characters[groupID - 1];
                        playable.SetGenericBinding(useTimeLineAsset.GetRootTrack(0), standSlot);
                    }
                }
                if (useTimeLineAsset.GetRootTrack(1) is StandSlotPointGroupTrack)
                {
                    playable.SetGenericBinding(useTimeLineAsset.GetRootTrack(1), StandSlotPointGroup);
                }
                if (useTimeLineAsset.GetRootTrack(2) is DialogueControlTrack)
                {
                    playable.SetGenericBinding(useTimeLineAsset.GetRootTrack(2), dialogue);
                }
                if (useTimeLineAsset.GetRootTrack(3) is DialogueControlTrack)
                {
                    playable.SetGenericBinding(useTimeLineAsset.GetRootTrack(3), dialogueCN);
                }
            }
#endif
        }

        public override void SetNameText(string name)
        {
#if UNITY_EDITOR
            if (NameText == null) return;
#endif
            if(name == "旁白")
            {
                NamePanel.gameObject.SetActive(false);
                NameText.text = "";
            }
            else
            {

                NamePanel.gameObject.SetActive(true);
                NameText.text = $"{RagingLoopSetting.NameToJPNameText[name]}";
            }
        }

    }
}

