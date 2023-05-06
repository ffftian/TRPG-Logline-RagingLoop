using Spine.Unity.Playables;
using Spine.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.Timeline;

using UnityEngine;
using Unity.VisualScripting;
namespace Galgame
{
    [CustomEditor(typeof(GalgameComponent))]
    public class GalgameComponentEditor : Editor
    {
        private GalgameComponent component;

        //public Action OnSelectLeaveMessage;

        private IRecord[] casheRecover;


        private int serialPtr
        {
            get { return component.serialPtr; }
            set
            {
                if (value != component.serialPtr)
                {
                    Record(component.useTimeLineAsset);
                    component.SelectMessage(value);
                }
                component.serialPtr = value;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
                DrawExtra();
        }

        public void DrawExtra()
        {
            if (component.messageAssetLength != null)
            {
                #region 文本部分
                int length = (int)component.messageAssetLength;

                serialPtr = (int)EditorGUILayout.Slider("文本条", serialPtr, 0, length - 1);
                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginDisabledGroup(serialPtr == 0 ? true : false);
                if (GUILayout.Button("文本向左", EditorStyles.miniButtonLeft))
                    serialPtr--;
                EditorGUI.EndDisabledGroup();
                EditorGUI.BeginDisabledGroup(serialPtr == length - 1 ? true : false);
                if (GUILayout.Button("文本向右", EditorStyles.miniButtonRight))
                    serialPtr++;
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndHorizontal();
                CommonTextData checkedMeesage = component.messageAsset.messageDataList[serialPtr];
                EditorGUILayout.TextField(checkedMeesage.textId);
                EditorGUILayout.TextField(checkedMeesage.name);
                EditorGUILayout.TextArea(checkedMeesage.log, GUI.skin.textArea);

                if (component.useTimeLineAsset == null)
                {
                    string timelinePath = $@"Assets\Resources\{QQLogSettings.TimeLineDirectory}\{component.messageAsset.name}\{checkedMeesage.SaveID}.playable";
                    EditorGUILayout.LabelField($@"Timeline未能找到:{timelinePath}");
                }
                #endregion
                #region 同步部分
                if (!Application.isPlaying)
                {
                    EditorGUILayout.LabelField("同步部分");
                    if (GUILayout.Button("同步角色位置和站姿"))
                    {
                        IRecover[] rs = component.GetComponents<IRecover>();

                        foreach (IRecover r in rs)
                        {
                            r.RecoverData(component.messageAsset.name, component.serialPtr);
                        }
                    }
                }


                #endregion

            }
        }
        /// <summary>
        /// 记录当前轨道所需信息
        /// </summary>
        /// <param name="timelineAsset"></param>
        public void Record(TimelineAsset timelineAsset)
        {
            if (timelineAsset == null) return;

            for (int i = 0; i < casheRecover.Length; i++)
            {
                casheRecover[i].StartRecord(component.messageAsset.name, serialPtr);

                foreach (TrackAsset track in timelineAsset.GetOutputTracks())
                {
                    IRecordTack recordTack = track as IRecordTack;
                    if (recordTack != null)
                    {

                        if (component.playable.GetGenericBinding(track) != null)
                        {
                            casheRecover[i].RecordData(component.messageAsset.name, component.playable.GetGenericBinding(track).name, serialPtr, recordTack.SaveValue);
                        }
                    }
                }
            }
        }



        private void OnEnable()
        {
            component = (GalgameComponent)target;
            casheRecover = component.GetComponents<IRecord>();
        }
    }
}

