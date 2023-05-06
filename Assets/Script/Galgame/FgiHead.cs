using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FgiHead : MonoBehaviour//头像也必须要两个交错替换了
{
    /// <summary>
    /// 当前所属
    /// </summary>
    [SerializeField]
    protected FgiUnit fgiUnit;


    public Image[] dress;
    public Image[] face;
    public RectTransform dressGroup;
    public CanvasGroup canvasGroup;
    private Tween handle;

    public void TweenChangeDress(FgiUnit fgiUnit,FgiData[] newfgi, float time)
    {
        this.fgiUnit = fgiUnit;
        ChangeOffset();
        if(CheckDress(newfgi))
        { 
        int newI = 0;
            for (int i = 0; i < dress.Length; i++)
            {
                GameObject go = dress[i].gameObject;
                if (go.activeSelf)//是否已激活
                {
                    Tween handle = dress[i].DOFade(0, time);
                    handle.OnComplete(() => go.SetActive(false));
                }
                else
                {
                    if (newI == newfgi.Length)
                    {
                        continue;
                    }
                    ChangeImage(newfgi[newI], dress[i]);
                    go.transform.SetSiblingIndex(newI);
                    go.SetActive(true);
                    Tween handle = dress[i].DOFade(1, time);
                    newI++;
                }
            }
        }
    }
    public void TweenChangeFace(FgiUnit fgiUnit,FgiData[] newfgi, float time)
    {
        this.fgiUnit = fgiUnit;
        ChangeOffset();
        int newI = 0;
        for (int i = 0; i < face.Length; i++)
        {
            GameObject go = face[i].gameObject;
            if (go.activeSelf)
            {
                Tween handle = face[i].DOFade(0, time);
                handle.OnComplete(() => go.SetActive(false));
            }
            else
            {
                if (newI == newfgi.Length)
                {
                    continue;
                }
                ChangeImage(newfgi[newI], face[i]);
                go.transform.SetSiblingIndex(newI);
                go.SetActive(true);
                Tween handle = face[i].DOFade(1, time);
                newI++;
            }
        }
    }

    public void ChangeFace(FgiUnit fgiUnit,FgiData[] fgi)
    {
        this.fgiUnit = fgiUnit;
        ChangeOffset();
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
    public void ChangeDress(FgiUnit fgiUnit,FgiData[] fgi)
    {
        this.fgiUnit = fgiUnit;
        ChangeOffset();
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
    public bool CheckDress(FgiData[] fgi)
    {
        for (int i = 0; i < fgi.Length; i++)
        {
            if (dressGroup.GetChild(i).GetComponent<Image>().sprite != fgi[i].sprite)
            {
                return true;
            }
        }
        return false;
    }
    public void ChangeOffset()
    {
        (this.transform as RectTransform).anchoredPosition =  fgiUnit.currentFgiAsset.headOffset;

        if(fgiUnit.transform.localScale.x < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }


    public void ChangeImage(FgiData fgi, Image image)
    {
        image.rectTransform.anchoredPosition = new Vector2(fgi.left, fgi.top);
        image.rectTransform.sizeDelta = new Vector2(fgi.width, fgi.hegiht);
        image.sprite = fgi.sprite;
    }
#if UNITY_EDITOR
    /// <summary>
    /// 保存当前面部位置偏移
    /// </summary>
    [Button("保存位置偏移")]
    public void SaveOffset()
    {
        for(int i=0;i< fgiUnit.fgiAssets.Length;i++)
        {
            fgiUnit.fgiAssets[i].headOffset = (this.transform as RectTransform).anchoredPosition;
            EditorUtility.SetDirty(fgiUnit.fgiAssets[i]);
        }
        //fgiUnits.currentFgiAsset.reversal = this.transform.localScale.x > 0 ? false : true;
    }
#endif

}

