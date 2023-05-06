using Spine.Unity.Playables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
namespace Galgame
{
    /// <summary>
    /// 我觉得支持，本身组件自带复位功能会比较好
    /// </summary>
    [DisallowMultipleComponent]
    public class GalgameComponent : LoglineComponent
    {
        public CommonTextAsset messageAsset;
        public List<FgiUnit> characters = new List<FgiUnit>();
        public FgiPointGroup fgiPointGroup;
        public Image barImage;

        public override string CurrentRoleName => messageAsset.messageDataList[serialPtr].name;

        public override int? messageAssetLength => messageAsset?.messageDataList.Count;
#if UNITY_EDITOR
        public bool 自动同步角色位置信息 = false;
#endif
        public override void SelectMessage(int Serial)
        {
            CommonTextData serialData = messageAsset.messageDataList[Serial];
            SetNameText(serialData.name);
            string timeLinePath = $@"{QQLogSettings.TimeLineDirectory}\{messageAsset.name}\{serialData.SaveID}";
            useTimeLineAsset = Resources.Load<TimelineAsset>(timeLinePath);
            playable.playableAsset = useTimeLineAsset;
#if UNITY_EDITOR//预赋值设置

            if (playable.GetGenericBinding(useTimeLineAsset.GetRootTrack(0)) == null)
            {
                if (useTimeLineAsset.GetRootTrack(0) is FgiUnitTrack)
                {
                    for (int i = 0; i < characters.Count; i++)
                    {
                        if (characters[i] != null)
                        {
                            if (characters[i].name == serialData.name)
                            {
                                playable.SetGenericBinding(useTimeLineAsset.GetRootTrack(0), characters[i]);
                                break;
                            }
                        }
                    }
                }
                if (useTimeLineAsset.GetRootTrack(1) is FgiPointGroupTrack)
                {
                    playable.SetGenericBinding(useTimeLineAsset.GetRootTrack(1), fgiPointGroup);
                }
                if (useTimeLineAsset.GetRootTrack(2) is DialogueControlTrack)
                {
                    playable.SetGenericBinding(useTimeLineAsset.GetRootTrack(2), dialogue);
                }
                if (useTimeLineAsset.GetRootTrack(4) is ImageBarTrack)
                {
                    playable.SetGenericBinding(useTimeLineAsset.GetRootTrack(4), barImage);
                }

            }
#endif
        }

        public override void SetNameText(string name)
        {
#if UNITY_EDITOR
            if (NameText == null) return;
#endif

            NameText.text = $"【{name}】";
        }
    }
}

