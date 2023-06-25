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
        private StandSlotFace StandSlotFace
        {
            get
            {
                return component.template.StandSlotFace;
            }
        }

        private StandSlotPopup slotPopup;
        private SingleSlotPopup singlePopup;

        private void OnEnable()
        {
            component = (StandSlotShowClip)target;

            BustAdvConfig bustAdvConfig = BustAdvConfig.LoadSettings();

            if (StandSlot != null)
            {
                string[] brows = bustAdvConfig.AdvCasheName[StandSlot.id - 1].casheBrow;
                string[] eyes = bustAdvConfig.AdvCasheName[StandSlot.id - 1].casheEye;
                string[] mouths = bustAdvConfig.AdvCasheName[StandSlot.id - 1].casheMouth;

                slotPopup = new StandSlotPopup(brows, eyes, mouths);
            }
            else if (StandSlotFace != null)
            {
                string[] mouths = bustAdvConfig.AdvCasheName[StandSlotFace.id - 1].casheMouth;
                singlePopup = new SingleSlotPopup(mouths, "面部");
            }
        }



        public override void OnInspectorGUI()
        {
            if (slotPopup == null && singlePopup == null)
            {
                OnEnable();
            }
            base.OnInspectorGUI();
            bool dirty = false;
            if (slotPopup != null)
            {
                int casheOverallIndex = Behaviour.overallIndex;
                dirty = slotPopup.OnInspectorGUI(ref Behaviour.browIndex, ref Behaviour.eyeIndex, ref Behaviour.mouthIndex, ref Behaviour.overallIndex);
                if (casheOverallIndex != Behaviour.overallIndex)
                {
                    Behaviour.browIndex = Behaviour.overallIndex;
                    Behaviour.eyeIndex = Behaviour.overallIndex;
                    Behaviour.overallIndex = Behaviour.overallIndex;
                }
            }
            else if (singlePopup != null)
            {
                dirty = singlePopup.OnInspectorGUI(ref Behaviour.faceIndex);
            }
            if (dirty)
            {
                EditorUtility.SetDirty(this.component);
                AssetDatabase.SaveAssetIfDirty(this.component);
            }
        }
    }
}

