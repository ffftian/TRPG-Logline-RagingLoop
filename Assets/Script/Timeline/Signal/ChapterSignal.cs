using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSignal : MonoBehaviour
{
    public Image X;
    public Image Y;

    public Sprite[] Index;
    public int XPtr = 0;
    public int YPtr = 0;

    private void Start()
    {
        X.sprite = Index[XPtr];
        Y.sprite = Index[YPtr];

    }


    public void ChapterAddX()
    {
        XPtr++;
        X.sprite = Index[XPtr];
    }
    public void ChapterAddY()
    {
        YPtr++;
        Y.sprite = Index[YPtr];
    }


}
