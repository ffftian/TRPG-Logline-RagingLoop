using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Miao;
using Sirenix.Utilities.Editor;
using UnityEngine.Timeline;
using System.Collections;
using System.Reflection;
using System;
namespace Galgame
{
    public class GalgameTimelineWindow : OdinEditorWindow
    {
        public CommonTextAsset messageAsset;
        protected CommonTextSettings settings;

        [PropertyOrder(1)]
        public List<CommonTextData> messageList;

        [MinMaxSlider(0, "MessageCount", true)]
        public Vector2Int messageListRange;

        [PropertyOrder(-2)]
        [Button("刷新消息资源显示")]
        public void RefreshMessageAssetPath()
        {
            if (messageAsset == null)
            {
                messageList.Clear();
                messageListRange = Vector2Int.zero;

            }
            else
            {
                //linq函数处理过的数组是浅拷贝，数组的更改将同步到messageAsset中。
                messageList = messageAsset.messageDataList.Skip(messageListRange.x).Take(messageListRange.y - messageListRange.x).ToList();
            }
        }


        [PropertyOrder(-1)]
        [Button("根据范围创建TimeLine")]
        public void CreateTimeline()
        {
            AssetDatabase.CreateFolder(CommonTextSettings.TimeLineDirectory, messageAsset.name);
            string TimeLinePath = $@"Assets\Resources\{QQLogSettings.TimeLineDirectory}\{messageAsset.name}";
            GalgameTimelineCreater timelineCreater = new GalgameTimelineCreater(TimeLinePath);
            for (int i = 0; i < messageList.Count; i++)
            {
                TimelineAsset timelineAsset = timelineCreater.CreateMessageTimeLine(messageList[i].SaveID, messageList[i].log, $@"{messageList[i].name}\{messageList[i].SaveID}");
            }
        }

        [MenuItem("文本编辑器/GalgameTimeLine生成器", priority = 100)]
        private static void OpenWindow()
        {
            GalgameTimelineWindow instance = GetWindow<GalgameTimelineWindow>("GalgameTimeLine生成器");
        }


        protected override void Initialize()
        {
            base.Initialize();
            settings = CommonTextSettings.LoadSettings();
            messageAsset = settings.commonTextAsset;
            messageListRange = settings.messageListRange;
            RefreshMessageAssetPath();
        }
        protected void OnDisable()
        {
            settings.commonTextAsset = messageAsset;
            settings.messageListRange = messageListRange;
            EditorUtility.SetDirty(settings);
        }
    }
}

