using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using System.ComponentModel;
namespace Galgame
{
    [CustomEditor(typeof(FgiUGUI))]
    public class FgiUGUIEditor : Editor
    {
        private FgiUGUI component;

        public FgiDressPopup DessPopup;
        public FgiFacePopup FacePopup;
        //FgiPopup faceFront;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (component.fgiAsset != null)
            {
                if (DessPopup == null)
                {
                    DessPopup = new FgiDressPopup(component.fgiAsset);
                    DessPopup.OnChange += component.ChangeDress;
                    DessPopup.OnChange += (c) => EditorUtility.SetDirty(component);

                    FacePopup = new FgiFacePopup(component.fgiAsset, component.fgiIndex.faceGroupIndex);
                    FacePopup.OnChange += component.ChangeFace;
                    FacePopup.OnChange += (c) => EditorUtility.SetDirty(component);
                }
                DessPopup.OnInspectorGUI(ref component.fgiIndex.dressIndex, ref component.fgiIndex.dressTypeIndex);
                FacePopup.OnInspectorGUI(ref component.fgiIndex.faceGroupIndex, ref component.fgiIndex.faceIndex);
            }
            else
            {
                DessPopup = null;
                FacePopup = null;
            }
        }
        private void OnEnable()
        {
            component = (FgiUGUI)target;
        }
        public void OnDisable()
        {
            //if (DessPopup != null)
            //{
            //    component.dressIndex = DessPopup.dressIndex;
            //    component.dressTypeIndex = DessPopup.dressTypeIndex;
            //    component.faceGroupIndex = FacePopup.faceGroupIndex;
            //    component.faceIndex = FacePopup.faceIndex;
            //}
        }
    }
}

