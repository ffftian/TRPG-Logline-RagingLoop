using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RagingLoop
{
    [CustomEditor(typeof(StandSlotFace), true)]
    public class StandSlotFaceEditor : Editor
    {
        private StandSlotFace component;

        public SingleSlotPopup singleSlotPopup;

        private void OnEnable()
        {
            component = (StandSlotFace)target;

            string[] faces = component.casheFace.Select(a => a.name).ToArray();

            singleSlotPopup = new SingleSlotPopup(faces, "面部");
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
