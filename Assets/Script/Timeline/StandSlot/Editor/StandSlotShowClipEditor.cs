using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
namespace RagingLoop
{
    [CustomEditor(typeof(StandSlotShowClip))]
    public class StandSlotShowClipEditor : Editor
    {

        private StandSlotShowClip component;
        private StandSlotShowBehaviour Behaviour
        {
            get
            {
                return component.template;
            }
        }
        private StandSlot StandSlot
        {
            get
            {
                return component.template.StandSlot;
            }
        }

        private StandSlotPopup slotPopup;

        private void OnEnable()
        {
            component = (StandSlotShowClip)target;
            BustAdvConfig bustAdvConfig = BustAdvConfig.LoadSettings();
            string[] brows = bustAdvConfig.AdvCasheName[StandSlot.id - 1].casheBrow;
            string[] eyes = bustAdvConfig.AdvCasheName[StandSlot.id - 1].casheEye;
            string[] mouths = bustAdvConfig.AdvCasheName[StandSlot.id - 1].casheMouth;
            //string[] brows = StandSlot.casheBrow.Select(a => a.name).ToArray();
            //string[] eyes = StandSlot.casheEye.Select(a => a.name).ToArray();
            //string[] mouths = StandSlot.casheMouth.Select(a => a.name).ToArray();

            slotPopup = new StandSlotPopup(brows, eyes, mouths);
        }

        public override void OnInspectorGUI()
        {
            if (slotPopup == null)
            {
                OnEnable();
            }
            base.OnInspectorGUI();
            int casheOverallIndex = Behaviour.overallIndex;
            bool dirty =  slotPopup.OnInspectorGUI(ref Behaviour.browIndex, ref Behaviour.eyeIndex, ref Behaviour.mouthIndex, ref Behaviour.overallIndex);
            if (casheOverallIndex != Behaviour.overallIndex)
            {
                Behaviour.browIndex = Behaviour.overallIndex;
                Behaviour.eyeIndex = Behaviour.overallIndex;
                Behaviour.overallIndex = Behaviour.overallIndex;
               // 
              //  AssetDatabase.SaveAssets();
            }
            if(dirty)
            {
                EditorUtility.SetDirty(this.component);
                AssetDatabase.SaveAssetIfDirty(this.component);
            }
        }
    }
}

