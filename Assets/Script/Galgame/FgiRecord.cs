#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
namespace Galgame
{
    public class FgiRecord : MonoBehaviour, IRecord, IRecover
    {
        public FgiPointGroup pointGroup;
        private GalgameComponent m_Component;
        public GalgameComponent galgameComponent
        {
            get
            {
                if (m_Component == null)
                {
                    m_Component = GetComponent<GalgameComponent>();
                }
                return m_Component;
            }
        }

        private RecordFgiAsset recordAnmationAsset;

        protected int? length
        {
            get
            {
                return galgameComponent.messageAssetLength;
            }
        }
        protected List<FgiUnit> characters
        {
            get
            {
                return galgameComponent.characters;
            }
        }
        public void StartRecord(string messageAssetName, int ptr)
        {
            if (recordAnmationAsset == null)
            {
                recordAnmationAsset = RecordFgiAsset.GetAsset(messageAssetName, (int)length);
            }
            RecordFgiPackage recordFgiPackage = recordAnmationAsset.GetRecordFgiPackage(ptr);
            recordFgiPackage.pointData.Clear();
        }

        public void RecordData(string messageAssetName, string targetName, int ptr, object value)
        {

            RecordFgiPackage recordFgiPackage = recordAnmationAsset.GetRecordFgiPackage(ptr);

            if (value is FgiUnitClip)
            {
                recordFgiPackage.name = targetName;//当前轨道的名字
                  FgiUnitClip fgi = (value as FgiUnitClip);
                FgiUnitSaveData fgiUnitData = new FgiUnitSaveData();
                fgiUnitData.fgiUnitBehaviour = fgi.template;
                fgiUnitData.fgiIndex = fgi.fgiIndex;
                recordFgiPackage.fgiUnitData = fgiUnitData;

            }
            if (value is FgiPointGroupClip)
            {
                var data = (value as FgiPointGroupClip);
                recordFgiPackage.pointData.Add(data.template);
            }

        }

        public void RecoverData(string messageAssetName, int currentIndex)
        {
            if (recordAnmationAsset == null)
            {
                recordAnmationAsset = RecordFgiAsset.GetAsset(messageAssetName, (int)length);
            }
            List<FgiUnit> useCharacters = new List<FgiUnit>(characters);

            //反过来写，通过遍历单位最近的站位来确定屏幕显示单位
            List<int> characterPtr = new List<int>();
            List<int> occupyPointsPtr = new List<int>();

            //List<int> standPoints = new List<int>(3);//已有站位点
            //List<int> findFgiPtr = new List<int>();


            for (int i = 0; i < characters.Count; i++)
            {
                characterPtr.Add(i);
            }
            for (int i = currentIndex - 1; i >= 0; i--)
            {
                RecordFgiPackage recordFgiPackage = recordAnmationAsset.GetRecordFgiPackage(i);//逆向赋值
                if (useCharacters.Count == 0 && characterPtr.Count == 0)
                {
                    break;
                }
                #region 角色立绘状态设置

                for (int j = 0; j < useCharacters.Count; j++)
                {
                    if (useCharacters[j].name == recordFgiPackage.name)
                    {
                        useCharacters[j].ChangeDress(recordFgiPackage.fgiUnitData.fgiUnitBehaviour.fgiDress);
                        useCharacters[j].ChangeFace(recordFgiPackage.fgiUnitData.fgiUnitBehaviour.fgiFace);
                        useCharacters.Remove(useCharacters[j]);
                        break;
                    }
                }
                #endregion

                #region 找坐标
                for (int j = 0; j < recordFgiPackage.pointData.Count; j++)
                {
                    int fgiPtr = recordFgiPackage.pointData[j].fgiPtr;
                    int pointsPtr = recordFgiPackage.pointData[j].pointsPtr;

                    if (pointsPtr == pointGroup.Points.Length)//证明是隐藏站位
                    {
                        if (characterPtr.Contains(fgiPtr))//并且角色仍在列表内
                        {
                            pointGroup.ChangeFgiPoint(fgiPtr, pointsPtr);
                            characterPtr.Remove(fgiPtr);
                        }
                    }
                    else
                    {
                        for (int k = 0; k < characterPtr.Count; k++)
                        {
                            if (characterPtr[k] == fgiPtr)
                            {
                                if(occupyPointsPtr.Contains(pointsPtr))//这个位置已经被占据了
                                {
                                    characterPtr.Remove(fgiPtr);
                                }
                                else if (characterPtr.Contains(fgiPtr))
                                {
                                    pointGroup.ChangeFgiPoint(fgiPtr, pointsPtr);
                                    characterPtr.Remove(fgiPtr);
                                    occupyPointsPtr.Add(pointsPtr);
                                }
                            }
                        }
                    }

                    
                    //for (int k = 0; k < standPoints.Count; k++)
                    //{
                    //    if (pointsPtr == pointGroup.Points.Length)//证明是隐藏站位
                    //    {
                    //        pointGroup.ChangeFgiPoint(fgiPtr, pointsPtr);
                    //        findFgiPtr.Add(fgiPtr);
                    //    }
                    //    if (standPoints[k] == pointsPtr)
                    //    {
                    //        if (!findFgiPtr.Contains(fgiPtr))//如果一个单位占了两个位置，则取消这个位置站位的资格
                    //        {
                    //            pointGroup.ChangeFgiPoint(fgiPtr, pointsPtr);
                    //            findFgiPtr.Add(fgiPtr);
                    //        }
                    //        standPoints.Remove(standPoints[k]);
                    //        break;
                    //    }
                    //}
                }
                #endregion
            }
        }
    }
}

#endif