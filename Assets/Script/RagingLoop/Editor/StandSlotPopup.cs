using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class StandSlotPopup
{
    string[] brows;
    string[] eyes;
    string[] mouths;
    string[] overalls= new string[0];

     //Action<int> ChangeBrow;
     //Action<int> ChangeEye;
     //Action<int> ChangeMouth;

    public StandSlotPopup(string[] brows, string[] eyes, string[] mouths)
    {
        this.brows = brows;
        this.eyes = eyes;
        this.mouths = mouths;
        //together = brows.Length;

        //overalls = new string[brows.Length];
        //for(int i=0; i < overalls.Length; i++)
        //{
        //    overalls[i] = i.ToString();
        //}

        //this.ChangeBrow = ChangeBrow;
        //this.ChangeEye = ChangeEye;
        //this.ChangeMouth = ChangeMouth;
    }

    public void RefreshOveralls(string[] overalls)
    {
        this.overalls = overalls;
    }

    public bool OnInspectorGUI(ref int browIndex, ref int eyeIndex, ref int mouthIndex,ref int overallIndex)
    {
        bool dirty = false;
        int cashe = browIndex;
        browIndex = EditorGUILayout.Popup("额头", browIndex, brows);
        if(cashe != browIndex)
        {
            dirty = true;
        }
        cashe = eyeIndex;
        eyeIndex = EditorGUILayout.Popup("眼睛", eyeIndex, eyes);
        if (cashe != eyeIndex)
        {
            dirty = true;
        }
        cashe = mouthIndex;
        mouthIndex = EditorGUILayout.Popup("嘴巴", mouthIndex, mouths);
        if (cashe != mouthIndex)
        {
            dirty = true;
        }
        overallIndex = EditorGUILayout.Popup("全局", overallIndex, overalls);
        return dirty;
    }
}

