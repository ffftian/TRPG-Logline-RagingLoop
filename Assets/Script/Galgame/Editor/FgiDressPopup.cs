using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
namespace Galgame
{
    [Serializable]
    public class FgiDressPopup
    {
        //public int dressIndex;
        //public int dressTypeIndex;
        public Action<FgiData[]> OnChange;

        /// <summary>
        /// 缓存衣物名称
        /// </summary>
        private string[] casheDress;
        private FgiAsset fgiAsset;

        public FgiDressPopup(FgiAsset fgiAsset)
        {
            //this.dressIndex = dressIndex;
            //this.dressTypeIndex = dressTypeIndex;
            this.fgiAsset = fgiAsset;
            casheDress = fgiAsset.fgiDressInfoList.Select(a => a.dress).ToArray();
        }

        public void OnInspectorGUI(ref int dressIndex, ref int dressTypeIndex)
        {
            int casheIndex = dressIndex;
            dressIndex = EditorGUILayout.Popup("服装", dressIndex, casheDress);

            string[] dressType = new string[fgiAsset.fgiDressInfoList[dressIndex].diff.Count];
            for (int i = 0; i < fgiAsset.fgiDressInfoList[dressIndex].diff.Count; i++)
            {
                dressType[i] = i.ToString();
            }
            int chasDressTypeIndex = dressTypeIndex;
            dressTypeIndex = EditorGUILayout.Popup("种类", dressTypeIndex, dressType);
            if (casheIndex != dressIndex || chasDressTypeIndex != dressTypeIndex)
            {
                //List<string> group = fgiAsset.fgiDressInfoList[dressIndex].diff[dressTypeIndex].diffName;
                //int length = group.Count;
                //FgiData[] fgiDatas = new FgiData[length];

                //for (int i = 0; i < length; i++)
                //{
                //    fgiDatas[i] = fgiAsset.OtherDict[group[i]];
                //}
                FgiData[] data = GetData(dressIndex, dressTypeIndex);
                OnChange.Invoke(data);
            }
        }
        public FgiData[] GetData(int dressIndex, int dressTypeIndex)
        {
            List<string> group = fgiAsset.fgiDressInfoList[dressIndex].diff[dressTypeIndex].diffName;
            int length = group.Count;
            FgiData[] fgiDatas = new FgiData[length];

            for (int i = 0; i < length; i++)
            {
                fgiDatas[i] = fgiAsset.OtherDict[group[i]];
            }
            return fgiDatas;
        }
    }
}

