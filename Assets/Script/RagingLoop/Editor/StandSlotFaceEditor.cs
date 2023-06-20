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

        private StandSlotSetting standSlotSetting;
        private BustAdvConfig bustAdvConfig;

        private void OnEnable()
        {


            string[] faces = component.casheFace.Select(a => a.name).ToArray();
            //string[] faces = bustAdvConfig.AdvCasheName[component.id - 1].casheFace;


            standSlotSetting = StandSlotSetting.LoadSetting();
            component = (StandSlotFace)target;

            singleSlotPopup = new SingleSlotPopup(faces, "面部");
            standSlotSetting = StandSlotSetting.LoadSetting();

            standSlotSetting.GetSlotLabel(component.name, faces.Length - 1);
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

            singleSlotPopup.OnInspectorGUI(ref component.faceIndex);
            if (casheFace != component.faceIndex)
            {
                component.ChangeBody(component.faceIndex);
            }

        }

    }
}
