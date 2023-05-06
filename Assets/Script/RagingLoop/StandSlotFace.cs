using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StandSlotFace : StandSlotBase
{
    public List<Sprite> casheFace;

    public Image face;

    public void ChangeFace(int browIndex)
    {
        face.sprite = casheFace[browIndex];
    }

    [HideInInspector]
    public int faceIndex;
}

