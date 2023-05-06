using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;

public class BaseOdinSettings<TSettings> : SerializedScriptableObject where TSettings : SerializedScriptableObject
{
    public const string SettingPath = @"Assets\LoglineEditorSetting";
    public const string SolderPath = "LoglineEditorSetting";
    public const string TimeLineDirectory = "TimeLine";

#if UNITY_EDITOR
    static TSettings settings;
    public static TSettings LoadSettings()
    {
        if (settings) return settings;
        SettingsNameAttribute settingsNameAttribute = typeof(TSettings).GetCustomAttribute<SettingsNameAttribute>();
        if (settingsNameAttribute == null)
        {
            Debug.LogError("未设置保存名称");
        }
        string path = @$"{SettingPath}\{settingsNameAttribute.settingsName}";
        var g2 = AssetDatabase.LoadAssetAtPath<QQLogSettings>(path);
        settings = AssetDatabase.LoadAssetAtPath<TSettings>(path);
        if (settings == null)
        {
            settings = ScriptableObject.CreateInstance<TSettings>();
            AssetDatabase.CreateAsset(settings, @$"{SettingPath}\{settingsNameAttribute.settingsName}");
        }
        return settings;
    }

#endif
}

