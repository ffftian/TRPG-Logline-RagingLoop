using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class StandPictureCreaterSettings : ScriptableObject
{

    public GameObject StandPrefab;
    public GameObject FacePrefab;

    public RectTransform StandPictureRoot;
    public string StandPicturePath = "Assets/Sources/RagingLoop/StandPicture";

    static StandPictureCreaterSettings settings;
    public static StandPictureCreaterSettings LoadSettings()
    {
        if (settings) return settings;
        settings = AssetDatabase.LoadAssetAtPath<StandPictureCreaterSettings>(Path.Combine(QQLogSettings.SettingPath,$"{nameof(StandPictureCreaterSettings)}.asset"));
        if (settings == null)
        {
            settings = CreateInstance<StandPictureCreaterSettings>();
            AssetDatabase.CreateAsset(settings, Path.Combine(QQLogSettings.SettingPath, $"{nameof(StandPictureCreaterSettings)}.asset"));
        }
        return settings;
    }
}

