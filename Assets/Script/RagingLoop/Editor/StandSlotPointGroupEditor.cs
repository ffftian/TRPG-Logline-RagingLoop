using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RagingLoop
{
    [CustomEditor(typeof(StandSlotPointGroup), true)]
    public class StandSlotPointGroupEditor : Editor
    {
        private StandSlotPointGroup component;

        //private string[] pointChoosePopup;

        public PointGroupPop pop;

        private void OnEnable()
        {
            component = (StandSlotPointGroup)target;

            //更新可选Slots和表格
            if (component.occupyStandSlot.Length < component.standPoints.Length)
            {
                component.occupyStandSlot = new int[component.standPoints.Length];
            }

            pop = new PointGroupPop(component);
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //if (GUILayout.Button("重置已有slot对象到组并隐藏"))
            //{
            //    component.standSlotGroup = new RectTransform[component.transform.childCount];
            //    for (int i=0; i<component.transform.childCount; i++)
            //    {
            //        component.standSlotGroup[i] = component.transform.GetChild(i) as RectTransform;
            //        component.standSlotGroup[i].gameObject.SetActive(false);
            //    }
            //}
            if (GUILayout.Button("赋值序号"))
            {
                for(int i=0; i < component.standSlotGroup.Length; i++)
                {
                    component.standSlotGroup[i].GetComponent<StandSlotBase>().id = i+1;
                }
            }
            if (GUILayout.Button("重新排序"))
            {
                for(int i= component.standSlotGroup.Length - 1 ;i>=0;i--)
                {
                    component.standSlotGroup[i].SetAsFirstSibling();
                }
            }

            if (GUILayout.Button("重置绑定对象的坐标"))
            {
                for (int i = 0; i < component.standSlotGroup.Length; i++)
                {
                    component.standSlotGroup[i].SetParent(component.transform);
                    component.standSlotGroup[i].gameObject.SetActive(false);
                }
            }

            if (component.standPoints == null|| pop==null) return;
            pop.OnInspectorGUI(ref component.occupyStandSlot);
        }
    }
}
