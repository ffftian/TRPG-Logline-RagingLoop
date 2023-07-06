using RagingLoop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StandSlotPointGroupClip))]
public class StandSlotPointGroupClipEditor : Editor
{

    private StandSlotPointGroupClip component;
    private string[] popup;
    private StandSlotPointGroup StandSlotPointGroup
    {
        get
        {
            return component.template.standSlotPointGroup;
        }
    }
    private StandSlotPointGroupBehaviour Behaviour
    {
        get
        {
            return component.template;
        }
    }
    public PointGroupPop pop;

    private void OnEnable()
    {
        component = (StandSlotPointGroupClip)target;
        if (Behaviour.occupyStandSlot == null || Behaviour.occupyStandSlot.Length < StandSlotPointGroup.standPoints.Length)
        {
            Behaviour.occupyStandSlot = new int[StandSlotPointGroup.standPoints.Length];
            for (int i = 0; i < Behaviour.occupyStandSlot.Length; i++)
            {
                Behaviour.occupyStandSlot[i] = 999;
            }
        }
        pop = new PointGroupPop(StandSlotPointGroup);

    }
    private void OnDisable()
    {
        UnityEditor.EditorUtility.SetDirty(this.component);
        AssetDatabase.SaveAssets();
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        if (StandSlotPointGroup == null) return;
        pop.OnInspectorGUINoChange(ref Behaviour.occupyStandSlot,ref Behaviour.slotIndexs);

    }
}

