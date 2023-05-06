using Spine.Unity;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[SettingsName("QQLogEditorSetting.asset")]
public class QQLogSettings : BaseSettings<QQLogSettings>
{
    public QQTextAsset QQMessageAsset;
    public Vector2Int messageListRange;
    public bool UseDefaultName;
    public string SpineAssetName;
    [Tooltip("����ʱ���������ɶ������û���")]
    public string[] IgnoreName;
    public List<bool> extendMethodInfos = new List<bool>();

}

