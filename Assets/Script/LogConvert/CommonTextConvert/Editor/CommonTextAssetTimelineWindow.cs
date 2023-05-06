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
abstract public class CommonTextAssetTimelineWindow<TSettings> : OdinEditorWindow where TSettings : CommonTextSettings
{
    public CommonTextAsset messageAsset;
    protected TSettings settings;

    [PropertyOrder(1)]
    public List<CommonTextData> messageList;

    protected float MessageCount
    {
        get
        {
            return messageAsset.messageDataList.Count;
        }
    }

    [MinMaxSlider(0, "MessageCount", true)]
    public Vector2Int messageListRange;

    [PropertyOrder(-2)]
    [Button("刷新消息资源显示")]
    virtual public void RefreshMessageAssetPath()
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
        string timeLinePath = $@"Assets\Resources\{QQLogSettings.TimeLineDirectory}\{messageAsset.name}";
        CreateTimeline(timeLinePath);
    }
    protected abstract void CreateTimeline(string timeLinePath);

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Initialize()
    {
        base.Initialize();
        InitAsset();
        RefreshMessageAssetPath();
    }
    virtual protected void InitAsset()
    {
        settings = BaseSettings<TSettings>.LoadSettings();
        messageAsset = settings.commonTextAsset;
        messageListRange = settings.messageListRange;
    }

    virtual protected void OnDisable()
    {
        settings.commonTextAsset = messageAsset;
        settings.messageListRange = messageListRange;
        EditorUtility.SetDirty(settings);
    }
}

