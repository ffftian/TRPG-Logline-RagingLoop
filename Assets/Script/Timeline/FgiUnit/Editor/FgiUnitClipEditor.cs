using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
namespace Galgame
{
    [CustomEditor(typeof(FgiUnitClip))]
    public class FgiUnitClipEditor : Editor
    {
        private FgiUnitClip component;
        private FgiUnit fgiUnit
        {
            get
            {
                return component.template.fgiUnit;
            }
        }
        private FgiAsset currentFgiAsset
        {
            get
            {
                return fgiUnit.fgiAssets[component.assetIndex];
            }
        }


        public FgiDressPopup DessPopup;
        public FgiFacePopup FacePopup;
        private string[] casheAssets = new string[2] { "1", "2" };
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (fgiUnit != null)
            {
                PopupInspector();
                if (GUILayout.Button("使用上一项服装姿势"))
                {
                    GalgameComponent galgameComponent = GameObject.FindObjectOfType<GalgameComponent>();
                    var recordAnmationAsset = RecordFgiAsset.GetAsset(galgameComponent.messageAsset.name, (int)galgameComponent.messageAssetLength);

                    for (int i = galgameComponent.serialPtr - 1; i > 0; i--)
                    {
                        RecordFgiPackage recordFgiPackage = recordAnmationAsset.GetRecordFgiPackage(i);
                        if (recordFgiPackage.name == fgiUnit.name)//一会改，先看看能不能存上
                        {
                            component.fgiIndex = recordFgiPackage.fgiUnitData.fgiIndex;

                            //InitDressPopup();
                            //InitFacePopup();
                            component.template.fgiDress = DessPopup.GetData(component.fgiIndex.dressIndex, component.fgiIndex.dressTypeIndex);
                            component.template.fgiFace = FacePopup.GetData(component.fgiIndex.faceGroupIndex, component.fgiIndex.faceIndex);
                            break;
                        }
                    }

                }

                if (GUILayout.Button("使用默认服装姿势"))
                {
                    component.fgiIndex = fgiUnit.fgiIndex;
                    InitDressPopup();
                    InitFacePopup();
                    component.template.fgiDress = DessPopup.GetData(fgiUnit.dressIndex, fgiUnit.dressTypeIndex);
                    component.template.fgiFace = FacePopup.GetData(fgiUnit.faceGroupIndex, fgiUnit.faceIndex);
                    //FacePopup = null;
                }
            }
        }

        private void PopupInspector()
        {
            int casheAssetInxdex = component.assetIndex;
            component.assetIndex = EditorGUILayout.Popup("站姿", component.assetIndex, casheAssets);
            if (casheAssetInxdex != component.assetIndex)
            {
                Clear();
                //排除数组超界
                if (currentFgiAsset.fgiDressInfoList.Count <= component.dressIndex)
                {
                    component.dressIndex = currentFgiAsset.fgiDressInfoList.Count - 1;
                }
                if (currentFgiAsset.fgiDressInfoList[component.dressIndex].diff.Count <= component.dressTypeIndex)
                {
                    component.dressTypeIndex = currentFgiAsset.fgiDressInfoList[component.dressIndex].diff.Count - 1;
                }
                if (currentFgiAsset.fgiFaceInfoListGroup.Count <= component.faceGroupIndex)
                {
                    component.faceGroupIndex = currentFgiAsset.fgiFaceInfoListGroup.Count;
                }
                if (currentFgiAsset.fgiFaceInfoListGroup[component.faceGroupIndex].Count <= component.faceIndex)
                {
                    component.faceIndex = currentFgiAsset.fgiFaceInfoListGroup[component.faceGroupIndex].Count - 1;
                }
            }

            if (DessPopup == null)
            {
                InitDressPopup();
            }
            if (FacePopup == null)
            {
                InitFacePopup();
            }
            if (casheAssetInxdex != component.assetIndex)
            {
                component.template.fgiDress = DessPopup.GetData(component.fgiIndex.dressIndex, component.fgiIndex.dressTypeIndex);
                component.template.fgiFace = FacePopup.GetData(component.fgiIndex.faceGroupIndex, component.fgiIndex.faceIndex);

            }
            DessPopup.OnInspectorGUI(ref component.fgiIndex.dressIndex, ref component.fgiIndex.dressTypeIndex);
            FacePopup.OnInspectorGUI(ref component.fgiIndex.faceGroupIndex, ref component.fgiIndex.faceIndex);
        }
        public void InitDressPopup()
        {
            DessPopup = new FgiDressPopup(currentFgiAsset);
            DessPopup.OnChange = (v) =>
            {
                component.template.fgiDress = v;
            };
        }
        public void InitFacePopup()
        {
            FacePopup = new FgiFacePopup(currentFgiAsset, component.fgiIndex.faceGroupIndex);
            FacePopup.OnChange = (v) =>
            {
                component.template.fgiFace = v;
            };
        }


        private void Clear()
        {
            //if (DessPopup != null)
            //{
            //    component.dressIndex = DessPopup.dressIndex;
            //    component.dressTypeIndex = DessPopup.dressTypeIndex;
            //}
            //if (FacePopup != null)
            //{
            //    component.faceGroupIndex = FacePopup.faceGroupIndex;
            //    component.faceIndex = FacePopup.faceIndex;
            //}
            EditorUtility.SetDirty(component);
            DessPopup = null;
            FacePopup = null;
        }

        private void OnEnable()
        {

            component = (FgiUnitClip)target;
            if (fgiUnit != null)
            {
                casheAssets = fgiUnit.fgiAssets.Select(a => a.name).ToArray();
            }
        }
        private void OnDisable()
        {
            //if (DessPopup != null)
            //{
            //    component.dressIndex = DessPopup.dressIndex;
            //    component.dressTypeIndex = DessPopup.dressTypeIndex;
            //}
            //if (FacePopup != null)
            //{
            //    component.faceIndex = FacePopup.faceIndex;
            //    component.faceGroupIndex = FacePopup.faceGroupIndex;
            //}
            EditorUtility.SetDirty(component);
        }
    }
}

