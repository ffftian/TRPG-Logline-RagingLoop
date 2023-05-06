using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
namespace Galgame
{
    /// <summary>
    /// 分组是没法分组的，本身Face位置就定型了，更可能/表示的是路径位置
    /// </summary>
    [Serializable]
    public class FgiFacePopup
    {
        //public int faceGroupIndex;
        //public int faceIndex;
        public Action<FgiData[]> OnChange;

        private string[] cashFace;
        private string[] casheFaceGroup;

        private FgiAsset fgiAsset;

        public FgiFacePopup(FgiAsset fgiAsset, int faceGroupIndex)
        {
            //this.faceGroupIndex = faceGroupIndex;
            //this.faceIndex = faceIndex;
            this.fgiAsset = fgiAsset;
            casheFaceGroup = new string[fgiAsset.fgiFaceInfoListGroup.Count];
            for (int i = 0; i < fgiAsset.fgiFaceInfoListGroup.Count; i++)
            {
                casheFaceGroup[i] = i.ToString();
            }
            cashFace = fgiAsset.fgiFaceInfoListGroup[faceGroupIndex].Select(f => $"{f.face}:{f.faceGroup[0].faceKey}").ToArray();
        }

        public void OnInspectorGUI(ref int faceGroupIndex, ref int faceIndex)
        {
            int casheGroupIndex = faceGroupIndex;
            faceGroupIndex = EditorGUILayout.Popup("面部组", faceGroupIndex, casheFaceGroup);

            if (casheGroupIndex != faceGroupIndex)
            {
                cashFace = fgiAsset.fgiFaceInfoListGroup[faceGroupIndex].Select(f => $"{f.face}:{f.faceGroup[0].faceKey}").ToArray();
                faceIndex = 0;
            }

            int casheIndex = faceIndex;
            faceIndex = EditorGUILayout.Popup("面部", faceIndex, cashFace);

            if (faceIndex != casheIndex)
            {
                //List<FgiFaceGroup> fgiFaceGroups = fgiAsset.fgiFaceInfoListGroup[faceGroupIndex][faceIndex].faceGroup;
                //FgiData[] useDatas = new FgiData[fgiFaceGroups.Count];
                //for (int i = 0; i < fgiFaceGroups.Count; i++)
                //{
                //    if (fgiFaceGroups[i].isFace)
                //    {
                //        useDatas[i] = fgiAsset.FaceDict[fgiFaceGroups[i].faceKey];
                //    }
                //    else
                //    {
                //        useDatas[i] = fgiAsset.OtherDict[fgiFaceGroups[i].faceKey];
                //    }
                //}
                FgiData[] data = GetData(faceGroupIndex, faceIndex);
                OnChange.Invoke(data);
            }
        }
        public FgiData[] GetData(int faceGroupIndex, int faceIndex)
        {
            List<FgiFaceGroup> fgiFaceGroups = fgiAsset.fgiFaceInfoListGroup[faceGroupIndex][faceIndex].faceGroup;
            FgiData[] useDatas = new FgiData[fgiFaceGroups.Count];
            for (int i = 0; i < fgiFaceGroups.Count; i++)
            {
                if (fgiFaceGroups[i].isFace)
                {
                    useDatas[i] = fgiAsset.FaceDict[fgiFaceGroups[i].faceKey];
                }
                else
                {
                    useDatas[i] = fgiAsset.OtherDict[fgiFaceGroups[i].faceKey];
                }
            }
            return useDatas;
        }
    }
}
