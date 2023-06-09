﻿using RagingLoop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
[SettingsName("BustAdvConfig.asset")]
public class BustAdvConfig : BaseOdinSettings<BustAdvConfig>
{
    public Dictionary<string, ImgBustAdv> BustAdv = new Dictionary<string, ImgBustAdv>();
    public SaveCasheName[] AdvCasheName;

#if UNITY_EDITOR
    [MenuItem("文本编辑器/创建角色部件名称缓存")]
    static void Create()
    {
        BustAdvConfig bustAdvConfig = BustAdvConfig.LoadSettings();

        StandSlotPointGroup standSlotPointGroup = GameObject.FindObjectOfType<StandSlotPointGroup>();

        bustAdvConfig.AdvCasheName = new SaveCasheName[standSlotPointGroup.standSlotGroup.Length];

        for (int i = 0; i < standSlotPointGroup.standSlotGroup.Length; i++)
        {
            StandSlotBase standSlotBase = standSlotPointGroup.standSlotGroup[i].GetComponent<StandSlotBase>();

            StandSlot standSlot = standSlotBase as StandSlot;
            SaveCasheName saveCasheName = new SaveCasheName();
            saveCasheName.name = standSlotPointGroup.standSlotGroup[i].name;

            if (standSlot != null)
            {
                int count = standSlot.casheBrow.Count;
                saveCasheName.casheBrow = new string[count];
                for (int j = 0; j < count; j++)
                {
                    saveCasheName.casheBrow[j] = standSlot.casheBrow[j].name;
                }
                count = standSlot.casheEye.Count;
                saveCasheName.casheEye = new string[count];
                for (int j = 0; j < count; j++)
                {
                    saveCasheName.casheEye[j] = standSlot.casheEye[j].name;
                }
                count = standSlot.casheMouth.Count;
                saveCasheName.casheMouth = new string[count];
                for (int j = 0; j < count; j++)
                {
                    saveCasheName.casheMouth[j] = standSlot.casheMouth[j].name;
                }
            }
            bustAdvConfig.AdvCasheName[i] = saveCasheName;
        }
        Debug.Log("保存成功");
        EditorUtility.SetDirty(bustAdvConfig);
    }


    [MenuItem("文本编辑器/创建半身立绘设置")]
    static void BustAdvConfigCreater()
    {
        BustAdvConfig bustAdvConfig = LoadSettings();
        RagingLoopSetting.Init(bustAdvConfig.BustAdv);
        EditorUtility.SetDirty(bustAdvConfig);
    }
#endif
}

