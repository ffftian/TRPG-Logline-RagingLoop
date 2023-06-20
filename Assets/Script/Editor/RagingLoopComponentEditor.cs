using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

namespace RagingLoop
{
    [CustomEditor(typeof(RagingLoopComponent))]
    public class RagingLoopComponentEditor : Editor
    {
        private RagingLoopComponent component;

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
            if (!Application.isPlaying)
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
                CommonTextData checkedMessage = component.messageAsset.messageDataList[serialPtr];
                CommonTextData checkedMessageCN = component.messageAssetCN.messageDataList[serialPtr];

                EditorGUILayout.TextField(checkedMessage.textId);
                EditorGUILayout.TextField(checkedMessage.name);
                EditorGUILayout.TextArea(checkedMessage.log, GUI.skin.textArea);
                EditorGUILayout.TextArea(checkedMessageCN.log, GUI.skin.textArea);

                if (component.useTimeLineAsset == null)
                {
                    string timelinePath = $@"Assets\Resources\{QQLogSettings.TimeLineDirectory}\{component.messageAsset.name}\{checkedMessage.SaveID}.playable";
                    EditorGUILayout.LabelField($@"Timeline未能找到:{timelinePath}");
                }
                #endregion

                EditorGUILayout.LabelField("同步部分");
                #region 同步部分

                if (GUILayout.Button("读取IRecover数据"))
                {
                    IRecover[] rs = component.GetComponents<IRecover>();

                    foreach (IRecover r in rs)
                    {
                        r.RecoverData(component.messageAsset.name, component.serialPtr);
                    }
                }
                #endregion

                if (GUILayout.Button("调用 AssetDatabase.SaveAssets()"))
                {
                    AssetDatabase.SaveAssets();
                }
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
            component = (RagingLoopComponent)target;
            casheRecover = component.GetComponents<IRecord>();
        }
#if UNITY_EDITOR
        private void OnDisable()
        {
            //UnityEditor.EditorUtility.SetDirty(this.component);
            //AssetDatabase.SaveAssets();
        }
    }
#endif
}
