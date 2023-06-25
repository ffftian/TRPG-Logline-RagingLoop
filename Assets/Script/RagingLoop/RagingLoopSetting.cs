using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Text.RegularExpressions;
using Spine;
using Unity.VisualScripting;
using static UnityEngine.UI.CanvasScaler;
using UnityEditor;

public struct SaveCasheName
{
    public string name;
    public string[] casheBrow;
    public string[] casheEye;
    public string[] casheMouth;
}

public static class RagingLoopSetting
{
    public static Dictionary<string, string> NameToJPNameText = new Dictionary<string, string>();
    public static Dictionary<string, string> NameToCHNameText = new Dictionary<string, string>();


    const string bustAdvPath = "Setting/bustAdv";
    //static Dictionary<string, List<ImgBustAdv>> BustAdv = new Dictionary<string, List<ImgBustAdv>>();
    static RagingLoopSetting()
    {
        NameToJPNameText.Add("房石阳明", "房石　陽明");
        NameToJPNameText.Add("咩子", "めー子");
        NameToJPNameText.Add("美佐峰美辻", "美佐峰　美辻");
        NameToJPNameText.Add("芹泽千枝实", "芹沢　千枝実");
        NameToJPNameText.Add("回末李花子", "回末　李花子");
        NameToJPNameText.Add("卷岛春", "巻島　春");
        NameToJPNameText.Add("织部泰长", "織部　泰長");
        NameToJPNameText.Add("酿田近望", "醸田　近望");
        NameToJPNameText.Add("织部义次", "織部　義次");
        NameToJPNameText.Add("织部香织", "織部　かおり");
        NameToJPNameText.Add("能里清之介", "能里　清之介");
        NameToJPNameText.Add("室匠", "室　匠");
        NameToJPNameText.Add("卷岛宽造", "巻島　寛造");
        NameToJPNameText.Add("山胁多惠", "山脇　多恵");
        NameToJPNameText.Add("狼老头", "狼老头");
        NameToJPNameText.Add("马宫久子", "馬宮　久子");
        NameToJPNameText.Add("桥本雄大", "橋本　雄大");

        NameToCHNameText.Add("房石阳明", "房石　阳明");
        NameToCHNameText.Add("咩子", "咩子");
        NameToCHNameText.Add("美佐峰美辻", "美佐峰　美辻");
        NameToCHNameText.Add("芹泽千枝实", "芹泽　千枝实");
        NameToCHNameText.Add("回末李花子", "回末　李花子");
        NameToCHNameText.Add("卷岛春", "卷岛　春");
        NameToCHNameText.Add("织部泰长", "织部　泰长");
        NameToCHNameText.Add("酿田近望", "酿田　近望");
        NameToCHNameText.Add("织部义次", "织部　义次");
        NameToCHNameText.Add("织部香织", "织部　香织");
        NameToCHNameText.Add("能里清之介", "能里　清之介");
        NameToCHNameText.Add("室匠", "室　匠");
        NameToCHNameText.Add("卷岛宽造", "卷岛　宽造");
        NameToCHNameText.Add("山胁多惠", "山胁　多惠");
        NameToCHNameText.Add("狼爷爷", "狼爷爷");
        NameToCHNameText.Add("马宫久子", "马宫　久子");
        NameToCHNameText.Add("桥本雄大", "桥本　雄大");
    }
    public static void Init(Dictionary<string, ImgBustAdv> bustAdv)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(bustAdvPath);
        InputBustAdv(bustAdv, textAsset.text);
    }

    //public static string GetAdvBrow(int id,int index)
    //{
    //    BustAdvConfig bustAdvConfig =  BustAdvConfig.LoadSettings();
    //    return  bustAdvConfig.AdvCasheName[id - 1].casheBrow[index];
    //}

    //public static string GetAdvEye(int id, int index)
    //{
    //    BustAdvConfig bustAdvConfig = BustAdvConfig.LoadSettings();
    //    return bustAdvConfig.AdvCasheName[id - 1].casheEye[index];
    //}

    //public static string GetAdvMouth(int id, int index)
    //{
    //    BustAdvConfig bustAdvConfig = BustAdvConfig.LoadSettings();
    //    return bustAdvConfig.AdvCasheName[id - 1].casheMouth[index];
    //}


    private static void InputBustAdv(Dictionary<string, ImgBustAdv> BustAdv, string analysis)
    {
        string[] sp = analysis.Split('\n');

        BustAdv.Clear();
        for (int i = 0; i < sp.Length; i++)
        {
            MatchCollection match = Regex.Matches(sp[i], "[0-9\\-]+");
            //try
            if (match.Count >= 8)
            {
                ImgBustAdv imgBustAdv = new ImgBustAdv();
                try
                {
                    imgBustAdv.slot = int.Parse(match[0].Value);
                    imgBustAdv.number = int.Parse(match[1].Value);
                    imgBustAdv.xy.x = int.Parse(match[2].Value);
                    imgBustAdv.xy.y = int.Parse(match[3].Value);
                    imgBustAdv.s = int.Parse(match[8].Value);//#ImgScale最后一位参数
                    string key = $"{imgBustAdv.slot}_{imgBustAdv.number}";

                    if (!BustAdv.ContainsKey(key))
                    {
                        BustAdv.Add(key, imgBustAdv);
                    }
                    //if (BustAdv.TryGetValue(key, out List<ImgBustAdv> imgBuestAdvList))
                    //{
                    //    imgBuestAdvList.Add(imgBustAdv);
                    //}
                    //else
                    //{
                    //    BustAdv.Add(key, imgBuestAdvList = new List<ImgBustAdv>());
                    //    imgBuestAdvList.Add(imgBustAdv);
                    //}
                }
                catch (Exception e)
                {
                    Debug.LogError($"错误的读取序号{i}内容{match}");
                }
            }
        }
    }
    public static ImgBustAdv GetBustAdv(int slot, int number)
    {
#if UNITY_EDITOR
        BustAdvConfig bustAdvConfig = BustAdvConfig.LoadSettings();
#else
        BustAdvConfig bustAdvConfig = RagingLoopSettingMono.Instance.bustAdvConfig;
#endif
        if (bustAdvConfig.BustAdv.TryGetValue($"{slot}_{number}", out var imgBustAdv))
        {
            return imgBustAdv;
        }
        Debug.LogError($"未识别到的坐标{slot}_{number}");
        return new ImgBustAdv();
    }
    public static void SaveBustAdv(int slot, int number, RectTransform rectTransform)
    {
#if UNITY_EDITOR
        BustAdvConfig bustAdvConfig = BustAdvConfig.LoadSettings();
#else
        BustAdvConfig bustAdvConfig = RagingLoopSettingMono.Instance.bustAdvConfig;
#endif
        if (bustAdvConfig.BustAdv.TryGetValue($"{slot}_{number}", out var imgBustAdv))
        {
            imgBustAdv.xy = rectTransform.anchoredPosition;
            imgBustAdv.s = rectTransform.localScale.x * 100;
            imgBustAdv.anchorMax = rectTransform.anchorMax;
            imgBustAdv.anchorMin = rectTransform.anchorMin;
            imgBustAdv.pivot = rectTransform.pivot;
            bustAdvConfig.BustAdv[$"{slot}_{number}"] = imgBustAdv;

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(bustAdvConfig);
            AssetDatabase.SaveAssetIfDirty(bustAdvConfig);
            Debug.Log($"成功覆盖到BustAdv配置{slot}_{number}中");
#endif
        }
        else
        {
            Debug.LogError($"未能覆盖到BustAdv配置中{slot}_{number}");
        }
    }
}

