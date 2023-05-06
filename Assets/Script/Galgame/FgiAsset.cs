using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 整个结构可能是这样的，Info信息才是真正的图纸
/// Info来决定在画面中组成哪些配件
/// </summary>

public class FgiAsset : SerializedScriptableObject
{
    public Vector2 headOffset;//头像偏移

    /// <summary>
    /// 服装或者其他插槽
    /// </summary>
    public List<FgiData> otherList = new List<FgiData>();
    /// <summary>
    /// 表情插槽
    /// </summary>
    public List<FgiData> faceList = new List<FgiData>();
    /// <summary>
    /// 衣物信息
    /// </summary>
    public List<FgiDressInfo> fgiDressInfoList;
    /// <summary>
    /// 表情信息
    /// </summary>
    public List<List<FgiFaceInfo>> fgiFaceInfoListGroup;

    private Dictionary<string, FgiData> otherDict;
    public Dictionary<string, FgiData> OtherDict
    {
        get
        {
            if (otherDict == null)
            {
                otherDict = otherList.ToDictionary(b => b.name);
            }
            return otherDict;
        }
    }
    private Dictionary<string, FgiData> faceDict;

    public Dictionary<string, FgiData> FaceDict
    {
        get
        {
            if (faceDict == null)
            {
              faceDict = faceList.ToDictionary(a => a.name);
            }
            return faceDict;
        }
    }


#if UNITY_EDITOR
    const int parameter = 13;
    public void Init(string configText, string InfoText, string fgiName, string folorPath)
    {
        AnalysisConfig(configText, fgiName, folorPath);
        AnalysisInfo(InfoText, fgiName, folorPath);

    }
    public void AnalysisConfig(string configText, string fgiName, string folorPath)
    {
        string[] spText = configText.Split('\t');
        ///需要跳过开头预设两行
        for (int i = parameter * 2; i < spText.Length - parameter + 1; i += parameter)
        {
            FgiData fgi = new FgiData();
            try
            {
                fgi.name = spText[i + 1];
                fgi.left = int.Parse(spText[i + 2]);
                fgi.top = -int.Parse(spText[i + 3]);
                fgi.width = int.Parse(spText[i + 4]);
                fgi.hegiht = int.Parse(spText[i + 5]);
                //配置文件中有一类全为0并且没图片,且与info挂钩的奇怪配置，这可能导致字典冲突，所以忽略。
                if (fgi.left == 0 && fgi.top == 0 && fgi.width == 0 && fgi.hegiht == 0)
                {
                    continue;
                }

                fgi.type = int.Parse(spText[i + 6]);
                byte.TryParse(spText[i + 7], out fgi.opacity);//茉子a_2595
                fgi.visible = int.Parse(spText[i + 8]);
                fgi.layerId = int.Parse(spText[i + 9]);
                if (!string.IsNullOrEmpty(spText[i + 10]))
                {
                    fgi.groupLayerId = int.Parse(spText[i + 10]);
                    faceList.Add(fgi);
                }
                else
                {
                    otherList.Add(fgi);
                }


                string path = Path.Combine(folorPath, $"{fgiName}_{fgi.layerId}.png");
                fgi.sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(path);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    public void AnalysisInfo(string InfoText, string fgiName, string folorPath)
    {
        string[] spText = InfoText.Split("\r\n", StringSplitOptions.None);
        fgiDressInfoList = new List<FgiDressInfo>();
        FgiDressInfo currentDress = new FgiDressInfo();

        fgiFaceInfoListGroup = new List<List<FgiFaceInfo>>();
        List<FgiFaceInfo> curretList = new List<FgiFaceInfo>();


        FgiFaceInfo currentFace = new FgiFaceInfo();
        int lastIndex = -1;
        for (int i = 0; i < spText.Length - 4; i++)
        {
            string[] paragraphInfo = spText[i].Split('\t');
            if (paragraphInfo.Length == 5)
            {
                if (paragraphInfo[0] == "dress")
                {

                    if (currentDress.dress != paragraphInfo[1])//如果表情不同
                    {
                        currentDress = new FgiDressInfo();
                        currentDress.dress = paragraphInfo[1];
                        fgiDressInfoList.Add(currentDress);
                        lastIndex = -1;
                    }
                }
                else
                {
                    Debug.Log("分割");
                }
                if (paragraphInfo[2] == "diff")
                {
                    int index = int.Parse(paragraphInfo[3]);
                    if (lastIndex != index)
                    {
                        currentDress.diff.Add(new FgiDressGroup());
                        lastIndex = index;
                    }
                    currentDress.diff[currentDress.diff.Count - 1].diffName.Add(paragraphInfo[4]);//添加实际面部拆分
                }
            }

            if (paragraphInfo.Length == 1)
            {
                if (curretList.Count != 0)
                {
                    fgiFaceInfoListGroup.Add(curretList);
                    curretList = new List<FgiFaceInfo>();
                }
            }


            if (paragraphInfo.Length == 4)
            {
                if (paragraphInfo[0] == "face")
                {

                    if (currentFace.face != paragraphInfo[1])
                    {
                        currentFace = new FgiFaceInfo();
                        currentFace.face = paragraphInfo[1];
                        curretList.Add(currentFace);
                        //fgiFaceInfoListGroup.Add(currentFace);
                    }
                }
                if (paragraphInfo[2] == "base")
                {
                    //这里的图片会有一个前缀，但前缀本身不是图片，作用未知，也无法作为表情分组
                    string[] spGroup = paragraphInfo[3].Split('/');//带一撇系列的都是表情

                    if (spGroup.Length == 2)
                    {
                        currentFace.faceGroup.Add(new FgiFaceGroup() { isFace = true, faceKey = spGroup[1] });
                        //currentFace.faceGroup.Add(spGroup[spGroup.Length - 1]);
                    }
                    else
                    {
                        currentFace.faceGroup.Add(new FgiFaceGroup() { faceKey = spGroup[0] });
                    }





                }
            }
        }

    }

#endif
}

