using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
namespace Galgame
{
    //[Serializable]
    //public class RecordFgiPackage//在galgame中，只有对话的角色会改变立绘状态
    //{
    //    public string name;//fgiUnit所属
    //    public FgiUnitClip fgiUnitData;
    //    public List<FgiPointGroupClip> pointData = new List<FgiPointGroupClip>();//可能存在两个角色消失再一个角色出现的情况，这种情况肯定是多轨道的
    //}

    public class FgiUnitSaveData
    {
        public FgiIndex fgiIndex;
        public FgiUnitBehaviour fgiUnitBehaviour;
    }

    [Serializable]
    public class RecordFgiPackage//存不了对方ScriptObject的引用啊！！
    {
        public string name;//fgiUnit所属
        public FgiUnitSaveData fgiUnitData;
        //public FgiUnitBehaviour fgiUnitData;
        public List<FgiPointGroupBehaviour> pointData = new List<FgiPointGroupBehaviour>();
    }


    public class RecordFgiAsset : SerializedScriptableObject
    {
        [SerializeField]
        protected RecordFgiPackage[] recordFgiPackages;
#if UNITY_EDITOR
        public static RecordFgiAsset GetAsset(string assetName, int length)
        {
            RecordFgiAsset recordObjectAsset = AssetDatabase.LoadAssetAtPath<RecordFgiAsset>($"{CommonTextSettings.SettingPath}\\{assetName}.Asset");

            if (recordObjectAsset == null)
            {
                recordObjectAsset = SerializedScriptableObject.CreateInstance<RecordFgiAsset>();
                recordObjectAsset.recordFgiPackages = new RecordFgiPackage[length];
                AssetDatabase.CreateAsset(recordObjectAsset, $"{CommonTextSettings.SettingPath}\\{assetName}.Asset");
            }
            if (length > recordObjectAsset.recordFgiPackages.Length)
            {
                RecordFgiPackage[] newRecord = new RecordFgiPackage[length];
                recordObjectAsset.recordFgiPackages.CopyTo(newRecord, length);
                recordObjectAsset.recordFgiPackages = newRecord;

            }
            return recordObjectAsset;
        }
#endif

        public RecordFgiPackage GetRecordFgiPackage(int ptr)
        {
            if (recordFgiPackages[ptr] == null)
            {
                recordFgiPackages[ptr] = new RecordFgiPackage();
            }
            return recordFgiPackages[ptr];
        }

        public FgiUnitSaveData GetLastCharacter(string characterName, int currentIndex)
        {
            for (int i = currentIndex; i >= 0; i--)
            {
                RecordFgiPackage recordFgiPackage = this.GetRecordFgiPackage(i);//逆向赋值
                if (characterName == recordFgiPackage.name)
                {
                    return recordFgiPackage.fgiUnitData;
                }
            }
            return null;
        }
    }
}

