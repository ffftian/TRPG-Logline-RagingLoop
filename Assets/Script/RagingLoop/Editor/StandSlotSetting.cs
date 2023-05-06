using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class StandSlotSetting : SerializedScriptableObject
{

    public Dictionary<string, string[]> SlotSetting = new Dictionary<string, string[]>();




    public string GetSlotLabel(string name, int index)
    {
        string[] data;
        if (SlotSetting.TryGetValue(name, out data))
        {
            if (index >= data.Length)
            {
                var newData = new string[index + 1];
                for (int i = 0; i < newData.Length; i++)
                {
                    newData[i] = i.ToString();
                }
                data.CopyTo(newData, 0);
                SlotSetting[name] = newData;
            }
            else
            {
                return data[index];
            }
        }
        else
        {
            data = new string[index + 1];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = i.ToString();
            }
            SlotSetting.Add(name, data);
        }

        return data[index];
    }
    public void SetSlotLabel(string name, int index, string label)
    {
        SlotSetting[name][index] = index + label;

    }


#if UNITY_EDITOR
    static StandSlotSetting settings;
    public static StandSlotSetting LoadSetting()
    {
        if (settings) return settings;
        settings = AssetDatabase.LoadAssetAtPath<StandSlotSetting>(Path.Combine(QQLogSettings.SettingPath, $"{nameof(StandSlotSetting)}.asset"));
        if (settings == null)
        {
            settings = CreateInstance<StandSlotSetting>();
            AssetDatabase.CreateAsset(settings, Path.Combine(QQLogSettings.SettingPath, $"{nameof(StandSlotSetting)}.asset"));
        }
        return settings;
    }
#endif

}

