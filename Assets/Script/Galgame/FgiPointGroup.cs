using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class FgiPointGroup : MonoBehaviour
{
    private RectTransform m_RectTransform;
    public RectTransform rectTransform
    {
        get
        {
            if (ReferenceEquals(m_RectTransform, null))
            {
                m_RectTransform = transform as RectTransform;
            }
            return m_RectTransform;
        }
    }
    public FgiUnit[] fgiUnits;
    public RectTransform[] Points;



    public void TweenChangeFgiPoint(int fgiPtr, int pointPtr)//改变fgi
    {
        if (pointPtr == Points.Length)//证明是取消站位
        {
            fgiUnits[fgiPtr].rectTransform.SetParent(transform);
            fgiUnits[fgiPtr].canvasGroup.DOFade(0, 0.5f);
            return;
        }

        try
        {
            Transform equal1 = Points[pointPtr].GetChild(0);
            Transform equal2 = fgiUnits[fgiPtr].transform;
            if (equal1 == equal2)//角色位置相同不改变
            {
                return;
            }
        }
        catch { };
        if (Points[pointPtr].childCount != 0)
        {
            for (int i = 0; i < Points[pointPtr].childCount; i++)//隐藏角色在该位置的显示
            {
                Transform childFgi = Points[pointPtr].GetChild(i);
                childFgi.SetParent(transform);

                FgiUnit fgiUnit = childFgi.GetComponent<FgiUnit>();
                fgiUnit.canvasGroup.DOFade(0, 0.5f);
            }
        }
        fgiUnits[fgiPtr].rectTransform.SetParent(Points[pointPtr]); //切换父级到点
        fgiUnits[fgiPtr].rectTransform.anchoredPosition = Vector2.zero;
        fgiUnits[fgiPtr].canvasGroup.alpha = 0f;
        fgiUnits[fgiPtr].canvasGroup.DOFade(1, 0.5f);
        fgiUnits[fgiPtr].gameObject.SetActive(true);//绑定可视
    }


    public void ChangeFgiPoint(int fgiPtr, int pointPtr)
    {
        if (pointPtr == Points.Length)//证明是取消站位
        {
            fgiUnits[fgiPtr].rectTransform.SetParent(transform);
            fgiUnits[fgiPtr].gameObject.SetActive(false);
            return;
        }

        if (Points[pointPtr].childCount != 0)
        {
            for (int i = 0; i < Points[pointPtr].childCount; i++)//隐藏角色在该位置的显示
            {
                Transform childFgi = Points[pointPtr].GetChild(i);
                childFgi.SetParent(transform);

                FgiUnit fgiUnit = childFgi.GetComponent<FgiUnit>();
                fgiUnit.gameObject.SetActive(false);

            }
        }
        fgiUnits[fgiPtr].transform.SetParent(Points[pointPtr]); //切换父级到点
        fgiUnits[fgiPtr].rectTransform.anchoredPosition = Vector2.zero;
        fgiUnits[fgiPtr].gameObject.SetActive(true);
    }
}

