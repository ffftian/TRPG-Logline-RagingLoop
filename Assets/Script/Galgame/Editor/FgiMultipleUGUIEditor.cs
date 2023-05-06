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
    [CustomEditor(typeof(FgiMultipleUGUI), true)]
    public class FgiMultipleUGUIEditor : Editor
    {
        private FgiMultipleUGUI component;

        public FgiDressPopup DessPopup;
        public FgiFacePopup FacePopup;

        public FgiAsset currentFgiAsset
        {
            get
            {
                return component.currentFgiAsset;
            }
        }
        public string[] casheAssets;
        //FgiPopup faceFront;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (!Application.isPlaying)
            {

                if (currentFgiAsset != null && casheAssets != null)
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
                        DessPopup = new FgiDressPopup(currentFgiAsset);
                        DessPopup.OnChange += component.ChangeDress;
                        //DessPopup.OnChange += (c) =>
                        //{
                        //    component.dressIndex = DessPopup.dressIndex;
                        //    component.dressTypeIndex = DessPopup.dressTypeIndex;
                        //};

                    }
                    if (FacePopup == null)
                    {
                        FacePopup = new FgiFacePopup(currentFgiAsset, component.fgiIndex.faceGroupIndex);
                        FacePopup.OnChange += component.ChangeFace;
                        //FacePopup.OnChange += (c) =>
                        //{
                        //    component.faceGroupIndex = FacePopup.faceGroupIndex;
                        //    component.faceIndex = FacePopup.faceIndex;
                        //};
                    }
                    if (casheAssetInxdex != component.assetIndex)
                    {
                        component.RefreshDress();
                        component.RefreshFace();
                    }
                    DessPopup.OnInspectorGUI(ref component.fgiIndex.dressIndex, ref component.fgiIndex.dressTypeIndex);
                    FacePopup.OnInspectorGUI(ref component.fgiIndex.faceGroupIndex, ref component.fgiIndex.faceIndex);
                }
                else
                {
                    Clear();
                }
            }
        }

        private void OnEnable()
        {
            if (!Application.isPlaying)
            {
                component = (FgiMultipleUGUI)target;
                casheAssets = component.fgiAssets.Select(a => a.name).ToArray();
            }
        }

        private void Clear()
        {
            //if (DessPopup != null)
            //{
            //    if (DessPopup.dressIndex != 0)
            //    {
            //        component.dressIndex = DessPopup.dressIndex;
            //        component.dressTypeIndex = DessPopup.dressTypeIndex;
            //    }
            //}
            //if (FacePopup != null)
            //{
            //    component.faceGroupIndex = FacePopup.faceGroupIndex;
            //    component.faceIndex = FacePopup.faceIndex;
            //}
            //EditorUtility.SetDirty(component);
            DessPopup = null;
            FacePopup = null;
        }


        public void OnDisable()
        {
            if (!Application.isPlaying)
            {
                if (component.gameObject != null)
                {
                    //指示记录对预制件实例所做的修改。
                    EditorUtility.SetDirty(component);
                    PrefabUtility.RecordPrefabInstancePropertyModifications(component);

                    for (int i = 0; i < this.component.face.Length; i++)
                    {
                        PrefabUtility.RecordPrefabInstancePropertyModifications(this.component.face[i]);
                    }
                    for (int i = 0; i < this.component.dress.Length; i++)
                    {
                        PrefabUtility.RecordPrefabInstancePropertyModifications(this.component.dress[i]);
                    }



                    //EditorUtility.SetDirty(component.gameObject);
                    //if (PrefabUtility.IsPartOfAnyPrefab(component.gameObject))
                    //{
                    //    PrefabUtility.ApplyPrefabInstance(component.gameObject, InteractionMode.AutomatedAction);
                    //}
                    //Clear();
                }
            }
        }
    }
}

