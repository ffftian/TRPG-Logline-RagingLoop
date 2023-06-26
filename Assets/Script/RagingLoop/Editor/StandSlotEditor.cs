using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RagingLoop
{
    [CustomEditor(typeof(StandSlot), true)]
    public class StandSlotEditor : Editor
    {
        private StandSlot component;
        private StandSlotPopup slotPopup;
        //public string currentName;
        private StandSlotSetting standSlotSetting;
        private BustAdvConfig bustAdvConfig;

        private void OnEnable()
        {
            bustAdvConfig = BustAdvConfig.LoadSettings();
            component = (StandSlot)target;
            //string[] body = component.casheBody.Select(c => c.name).ToArray();
            //int length = component.casheBrow.Count;
            string[] brows = bustAdvConfig.AdvCasheName[component.id - 1].casheBrow;
            string[] eyes = bustAdvConfig.AdvCasheName[component.id - 1].casheEye;
            string[] mouths = bustAdvConfig.AdvCasheName[component.id - 1].casheMouth;

            slotPopup = new StandSlotPopup(brows, eyes, mouths);
            try
            {
                standSlotSetting = StandSlotSetting.LoadSetting();
                standSlotSetting.GetSlotLabel(component.name, eyes.Length - 1);
                slotPopup.RefreshOveralls(Slot);
            }
            catch (Exception e)
            {

            }
        }
        private void OnDisable()
        {
            UnityEditor.EditorUtility.SetDirty(bustAdvConfig);
            AssetDatabase.SaveAssets();
        }
        public override void OnInspectorGUI()
        {
            try
            {
                var slot = component.transform.parent;
                int slotIndex = int.Parse(slot.name);
                component.index = EditorGUILayout.IntSlider(component.index, 0, component.GetSlotImgBustAdv().Count);
                if (GUILayout.Button("保存当前的Adv半身坐标(需要在已在编辑时使用)"))
                {
                    RagingLoopSetting.SaveBustAdv(slotIndex, component.id, component.index, component.transform as RectTransform);
                }
            }
            catch
            {

            }

            base.OnInspectorGUI();
            int casheBrow = component.browIndex;
            int casheEye = component.eyeIndex;
            int casheMouth = component.mouthIndex;
            int overallIndex = component.overallIndex;
            slotPopup.OnInspectorGUI(ref component.browIndex, ref component.eyeIndex, ref component.mouthIndex, ref component.overallIndex);
            if (overallIndex != component.overallIndex)
            {
                component.browIndex = component.overallIndex < component.casheBrow.Count ? component.overallIndex : component.browIndex;
                component.eyeIndex = component.overallIndex;
                component.mouthIndex = component.overallIndex < component.casheMouth.Count ? component.overallIndex : component.mouthIndex;
                Debug.Log($"全局标签名{component.overallIndex}");
            }
            if (casheBrow != component.browIndex)
            {
                component.ChangeBrow(component.browIndex);
            }
            if (casheEye != component.eyeIndex)
            {
                component.ChangeEye(component.eyeIndex);
            }
            if (casheMouth != component.mouthIndex)
            {
                component.ChangeMouth(component.mouthIndex);
            }


            int id = component.id - 1;
            bustAdvConfig.AdvCasheName[id].casheBrow[casheBrow] = EditorGUILayout.TextField("Brow可修改名称", bustAdvConfig.AdvCasheName[id].casheBrow[casheBrow]);
            bustAdvConfig.AdvCasheName[id].casheEye[casheEye] = EditorGUILayout.TextField("Eye可修改名称", bustAdvConfig.AdvCasheName[id].casheEye[casheEye]);
            bustAdvConfig.AdvCasheName[id].casheMouth[casheMouth] = EditorGUILayout.TextField("Mouth可修改名称", bustAdvConfig.AdvCasheName[id].casheMouth[casheMouth]);
            //RefreshName(this.component.name, overallIndex);

            EditorGUILayout.LabelField("acce");
            for (int i = 0; i < component.acce.Length; i++)
            {
                if (component.acce[i].sprite != null)
                {
                    EditorGUILayout.BeginHorizontal();
                    component.acce[i].gameObject.SetActive(EditorGUILayout.Toggle(component.acce[i].sprite.name, component.acce[i].gameObject.activeSelf));
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
        public string[] Slot
        {
            get
            {
                return standSlotSetting.SlotSetting[component.name];
            }
        }
        [Obsolete("废弃不使用这个来重命名")]
        private void RefreshName(string name, int index)
        {
            string label = standSlotSetting.GetSlotLabel(name, index);
            label = EditorGUILayout.TextField("标签名称", label);
            if (label != standSlotSetting.GetSlotLabel(name, index))
            {
                standSlotSetting.SetSlotLabel(name, index, label);
                slotPopup.RefreshOveralls(Slot);
            }
        }
    }
}

