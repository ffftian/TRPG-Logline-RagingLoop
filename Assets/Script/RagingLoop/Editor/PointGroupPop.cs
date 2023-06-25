
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RagingLoop
{
    public class PointGroupPop
    {
        private StandSlotPointGroup component;
        private string[] pointChoosePopup;

        public PointGroupPop(StandSlotPointGroup component)
        {
            this.component = component;

            pointChoosePopup = new string[component.standSlotGroup.Length+1];
            //pointChoosePopup[0] = "null";
            for (int i = 0; i < component.standSlotGroup.Length; i++)
            {
                pointChoosePopup[i] = component.standSlotGroup[i].name;//这个改成名字就可以实时读取了
            }
            pointChoosePopup[component.standSlotGroup.Length] = "null";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cashePoint=已在使用中的点上的对象"></param>
         public void OnInspectorGUI(ref int[] occupyStandSlot)
        {
            EditorGUILayout.LabelField("选择角色站位");
            for (int i = 0; i < component.standPoints.Length; i++)
            {

                EditorGUILayout.LabelField(component.GetIndexName(i));
                int lastOccupy = occupyStandSlot[i];
                occupyStandSlot[i] = EditorGUILayout.Popup(occupyStandSlot[i], pointChoosePopup);
                occupyStandSlot[i] = occupyStandSlot[i];

                if (occupyStandSlot[i] != lastOccupy)
                {
                    for (int x = 0; x < occupyStandSlot.Length; x++)
                    {
                        if (x != i)
                        {
                            if (occupyStandSlot[x] == occupyStandSlot[i])
                            {
                                occupyStandSlot[x] = pointChoosePopup.Length;//清空其他的该角色的绑点数据
                            }
                        }
                    }
                    if (occupyStandSlot[i] != component.standSlotGroup.Length)
                    {
                        if (lastOccupy < component.standSlotGroup.Length)
                        {
                            component.FreeSlot(lastOccupy);
                        }
                        component.BindSlot(occupyStandSlot[i], i);
                    }
                    ////Transform slot = component.standSlotGroup[occupyStandSlot[i]];
                    //component.FreeSlot(slotIndex);
                    //component.BindSlot(int slotindex,int index);
                }
                if (GUILayout.Button("清空"))
                {
                    if (occupyStandSlot[i] < component.standSlotGroup.Length)
                    {
                        component.FreeSlot(occupyStandSlot[i]);
                        occupyStandSlot[i] = component.standSlotGroup.Length;
                    }
                }
            }
        }

        public void OnInspectorGUINoChange(ref int[] occupyStandSlot)
        {
            EditorGUILayout.LabelField("选择角色站位");
            for (int i = 0; i < component.standPoints.Length; i++)
            {
                EditorGUILayout.LabelField(component.GetIndexName(i));
                int lastOccupy = occupyStandSlot[i];
                occupyStandSlot[i] = EditorGUILayout.Popup(occupyStandSlot[i], pointChoosePopup);
                occupyStandSlot[i] = occupyStandSlot[i];

                if (occupyStandSlot[i] != lastOccupy)
                {
                    for (int x = 0; x < occupyStandSlot.Length; x++)
                    {
                        if (x != i)
                        {
                            if (occupyStandSlot[x] == occupyStandSlot[i])
                            {
                                occupyStandSlot[x] = pointChoosePopup.Length;//清空其他的该角色的绑点数据
                            }
                        }
                    }
                    //if (occupyStandSlot[i] != component.standSlotGroup.Length)
                    //{
                    //    if (lastOccupy < component.standSlotGroup.Length)
                    //    {
                    //        component.FreeSlot(lastOccupy);
                    //    }
                    //    component.BindSlot(occupyStandSlot[i], i);
                    //}
                }
                if (GUILayout.Button("清空"))
                {
                    if (occupyStandSlot[i] < component.standSlotGroup.Length)
                    {
                        //component.FreeSlot(occupyStandSlot[i]);
                        occupyStandSlot[i] = component.standSlotGroup.Length;
                    }
                }
            }
        }
    }

}