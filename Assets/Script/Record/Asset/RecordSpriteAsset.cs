#if UNITY_EDITOR
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class RecordSpriteAsset : SerializedScriptableObject
{
    public Sprite[] RecordSprite;
    public static RecordSpriteAsset GetAsset(string assetName, int size)
    {
        RecordSpriteAsset recordObjectAsset = AssetDatabase.LoadAssetAtPath<RecordSpriteAsset>($"{QQLogSettings.SettingPath}\\{assetName}_BG.Asset");
        if (recordObjectAsset == null)
        {
            recordObjectAsset = new RecordSpriteAsset();
            recordObjectAsset.RecordSprite = new Sprite[size];
            AssetDatabase.CreateAsset(recordObjectAsset, $"{QQLogSettings.SettingPath}\\{assetName}_BG.Asset");
        }
        return recordObjectAsset;
    }
    public Sprite GetNearSprite(int currentIndex)
    {
        for (int i = currentIndex; i > 0; i--)
        {
            if (RecordSprite[i] != null)
            {
                return RecordSprite[i];
            }
        }
        return null;
    }
}
#endif

