using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public abstract class FgiUGUIBase : MonoBehaviour
{
    private RectTransform m_RectTransform;
    public RectTransform rectTransform
    {
        get
        {
            if (m_RectTransform == null)
            {
                m_RectTransform = transform as RectTransform;
            }
            return m_RectTransform;
        }
    }

    abstract public FgiAsset currentFgiAsset { get; }


    public Image[] dress;
    public Image[] face;

    public FgiData[] GetIndexFace()
    {
        List<FgiFaceGroup> fgiFaceGroups = currentFgiAsset.fgiFaceInfoListGroup[faceGroupIndex][faceIndex].faceGroup;
        FgiData[] useDatas = new FgiData[fgiFaceGroups.Count];
        for (int i = 0; i < fgiFaceGroups.Count; i++)
        {
            if (fgiFaceGroups[i].isFace)
            {
                useDatas[i] = currentFgiAsset.FaceDict[fgiFaceGroups[i].faceKey];
            }
            else
            {
                useDatas[i] = currentFgiAsset.OtherDict[fgiFaceGroups[i].faceKey];
            }
        }
        return useDatas;
    }
    public FgiData[] GetIndexDress()
    {
        List<string> groups = currentFgiAsset.fgiDressInfoList[dressIndex].diff[dressTypeIndex].diffName;
        int length = groups.Count;
        FgiData[] fgiDatas = new FgiData[length];

        for (int i = 0; i < length; i++)
        {
            fgiDatas[i] = currentFgiAsset.OtherDict[groups[i]];
        }
        return fgiDatas;
    }

    public void RefreshFace()
    {
        ChangeFace(GetIndexFace());
    }
    public void RefreshDress()
    {
        ChangeDress(GetIndexDress());
    }

    virtual public void ChangeFace(FgiData[] fgi)
    {
        for (int i = 0; i < face.Length; i++)
        {
            if (i < fgi.Length)
            {
                face[i].gameObject.SetActive(true);
                ChangeImage(fgi[i], face[i]);

            }
            else
            {
                face[i].gameObject.SetActive(false);
            }
        }
    }
    virtual public void ChangeDress(FgiData[] fgi)
    {
        for (int i = 0; i < dress.Length; i++)
        {
            if (i < fgi.Length)
            {
                dress[i].gameObject.SetActive(true);
                ChangeImage(fgi[i], dress[i]);

            }
            else
            {
                dress[i].gameObject.SetActive(false);
            }
        }
    }

    public void ChangeImage(FgiData fgi, Image image)
    {
        image.rectTransform.anchoredPosition = new Vector2(fgi.left, fgi.top);
        image.rectTransform.sizeDelta = new Vector2(fgi.width, fgi.hegiht);
        image.sprite = fgi.sprite;
    }

    #region FgiIndexGroup
    public FgiIndex fgiIndex;
    public int dressIndex
    {
        get
        {
            return fgiIndex.dressIndex;
        }
        set
        {
            fgiIndex.dressIndex = value;
        }
    }
    public int dressTypeIndex
    {
        get
        {
            return fgiIndex.dressTypeIndex;
        }
        set
        {
            fgiIndex.dressTypeIndex = value;
        }
    }
    public int faceGroupIndex
    {
        get
        {
            return fgiIndex.faceGroupIndex;
        }
        set
        {
            fgiIndex.faceGroupIndex = value;
        }
    }
    public int faceIndex
    {
        get
        {
            return fgiIndex.faceIndex;
        }
        set
        {
            fgiIndex.faceIndex = value;
        }
    }
    #endregion

}

