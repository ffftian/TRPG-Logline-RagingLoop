using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
/// <summary>
/// timeline实际使用的立绘显示切换器
/// </summary>
[DisallowMultipleComponent]
public class FgiUnit : FgiMultipleUGUI
{
    private bool dressUse;
    private bool dressFace;

    public FgiHead fgiHead;
    public RectTransform dressGroup;
    public RectTransform faceGroup;

    public CanvasGroup canvasGroup;

    //List<Tween> handleDress;


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
    public bool CheckFace(FgiData[] fgi)
    {
        for (int i = 0; i < fgi.Length; i++)
        {
            if (faceGroup.GetChild(i).GetComponent<Image>().sprite != fgi[i].sprite)
            {
                return true;
            }
        }
        return false;
    }

    public override void ChangeFace(FgiData[] fgi)
    {
        base.ChangeFace(fgi);
        fgiHead.ChangeFace(this, fgi);

    }
    public override void ChangeDress(FgiData[] fgi)
    {
        base.ChangeDress(fgi);
        fgiHead.ChangeDress(this, fgi);
    }

    public void TweenChangeDress(FgiData[] newfgi, float time = 0.25f)
    {
        fgiHead.TweenChangeDress(this, newfgi, time);

        if (CheckDress(newfgi))//是否更换躯干
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
    public void TweenChangeFace(FgiData[] newfgi, float time = 0.25f)
    {
        fgiHead.TweenChangeFace(this, newfgi, time);
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
    public void TweenImage(Image[] images, float Value, float time)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].DOFade(Value, time);
        }
    }

}

