using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

namespace RagingLoop
{
    public class RagingLoopTimelineWindow : CommonTextAssetTimelineWindow<BilingualTextSettings>
    {
        /// <summary>
        /// 中文验证配置
        /// </summary>
        public CommonTextAsset messageAssetCN;
        protected List<CommonTextData> messageListCN;

        public bool 启用预设置头像;
        public override void RefreshMessageAssetPath()
        {
            base.RefreshMessageAssetPath();
            if (messageAsset == null)
            {
                messageListCN.Clear();
            }
            else
            {
                messageListCN = messageAssetCN.messageDataList.Skip(messageListRange.x).Take(messageListRange.y - messageListRange.x).ToList();
            }
        }

        protected override void CreateTimeline(string timeLinePath)
        {
            RagingLoopTimelineCreater timelineCreater = new RagingLoopTimelineCreater(启用预设置头像,timeLinePath);
            for (int i = 0; i < messageList.Count; i++)
            {
                timelineCreater.CreateMessageTimeLine(messageList[i].SaveID, messageList[i].log, messageListCN[i].log, $@"{messageList[i].name}\{messageList[i].SaveID}");
            }
        }
        [MenuItem("文本编辑器/RagingLoopTimeTimeLine生成器", priority = 100)]
        static void OpenWindow()
        {
            RagingLoopTimelineWindow instance = GetWindow<RagingLoopTimelineWindow>("RagingLoopTimeline生成器");
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void InitAsset()
        {
            base.InitAsset();
            messageAssetCN = settings.SecondTextAsset;
            启用预设置头像 = settings.启用预设置头像;
        }

        protected override void OnDisable()
        {
            settings.SecondTextAsset = messageAssetCN;
            settings.启用预设置头像 = 启用预设置头像;
            base.OnDisable();
        }

    }
}
