using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace RagingLoop
{
    [CustomEditor(typeof(StandSlotFace), true)]
    public class StandSlotFaceEditor : Editor
    {
        private StandSlotFace component;

        public SingleSlotPopup singleSlotPopup;


        private StandSlotSetting standSlotSetting;//管人物拆分修改名称的
        private BustAdvConfig bustAdvConfig;//管站位的


        private void OnEnable()
        {
            bustAdvConfig = BustAdvConfig.LoadSettings();
            component = (StandSlotFace)target;

            //string[] faces = component.casheFace.Select(a => a.name).ToArray();
            string[] faces = bustAdvConfig.AdvCasheName[component.id - 1].casheMouth;
            singleSlotPopup = new SingleSlotPopup(faces, "面部");

            standSlotSetting = StandSlotSetting.LoadSetting();
            try
            {
                standSlotSetting.GetSlotLabel(component.name, faces.Length - 1);
            }
            catch (Exception e)
            {

            }
        }
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("保存当前的Adv半身坐标"))
            {
                var slot = component.transform.parent;
                int slotIndex = int.Parse(slot.name);
                RagingLoopSetting.SaveBustAdv(slotIndex, component.id, component.transform as RectTransform);
            }
            base.OnInspectorGUI();
            int casheFace = component.faceIndex;
            if (component.casheFace.Count == 0) return;

            singleSlotPopup.OnInspectorGUI(ref component.faceIndex);
            if (casheFace != component.faceIndex)
            {
                component.ChangeFace(component.faceIndex);
            }
            int id = component.id - 1;
            bustAdvConfig.AdvCasheName[id].casheMouth[casheFace] = EditorGUILayout.TextField("Face可修改名称", bustAdvConfig.AdvCasheName[id].casheMouth[casheFace]);

        }

    }
}
