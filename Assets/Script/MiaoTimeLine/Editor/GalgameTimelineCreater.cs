using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;
namespace Galgame
{
    public class GalgameTimelineCreater
    {
        public string saveDirectory;
        public string DialogueDirectiory;

        public GalgameTimelineCreater(string saveTimelineDirectory, string DialogueDirectiory = @"Assets\AssetSpeak")
        {
            this.saveDirectory = saveTimelineDirectory;
            this.DialogueDirectiory = DialogueDirectiory;
            if (!Directory.Exists(saveTimelineDirectory))
            {
                Directory.CreateDirectory(saveTimelineDirectory);
            }
        }

        public TimelineAsset CreateMessageTimeLine(string timelineName, string Dialogue, string SpeakAssetName)
        {
            if (File.Exists($@"{saveDirectory}\{timelineName}.playable"))
            {
                Debug.Log($@"{saveDirectory}\{timelineName}.playable拥有文件，跳过");
                return null;
            }

            TimelineAsset timelineAsset = ScriptableObject.CreateInstance<TimelineAsset>();//创建一个资源
            AssetDatabase.CreateAsset(timelineAsset, $@"{saveDirectory}\{timelineName}.playable");//先创建资源到本地，AssetDatabase源码里有检测资源函数，不这样他不会写到本地。

            #region 轨道资源创建
            FgiUnitTrack fgiUnitTrack = timelineAsset.CreateTrack<FgiUnitTrack>();
            FgiPointGroupTrack fgiPointGroupTrack = timelineAsset.CreateTrack<FgiPointGroupTrack>();
            DialogueControlTrack dialogueControlTrack = timelineAsset.CreateTrack<DialogueControlTrack>("Dialog");
            AudioTrack SpeakTrack = timelineAsset.CreateTrack<AudioTrack>("Speak");
            ImageBarTrack imageBarTrack = timelineAsset.CreateTrack<ImageBarTrack>();

            #endregion
            string audioPath = $@"{DialogueDirectiory}\{SpeakAssetName}.wav";
            AudioClip Speak = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath);
            float ClipLength = 0;
            if (Speak != null)
            {
                SpeakTrack.CreateClip(Speak);
                ClipLength = Speak.length;
            }


            TimelineClip clip = dialogueControlTrack.CreateClip<DialogueControlClip>();
            DialogueControlClip clipResource = (DialogueControlClip)clip.asset;
            clipResource.template.dialogue = Dialogue;
            clipResource.template._speed = 20;
            clip.displayName = timelineName;
            if (ClipLength != 0)
            {
                clip.duration = ClipLength;
            }
            else
            {
                ClipLength = Dialogue.Length * 0.08f;// 0.12f
                clip.duration = ClipLength;
            }

            clip = fgiUnitTrack.CreateClip<FgiUnitClip>();
            FgiUnitClip fgiUnitClip = (FgiUnitClip)(clip.asset);//这里要写一下默认着装配置什么的
            fgiUnitClip.dressIndex = 1;//跳过裸这个姿态


            clip = fgiPointGroupTrack.CreateClip<FgiPointGroupClip>();
            FgiPointGroupClip fgiPointGroupClip = (FgiPointGroupClip)(clip.asset);

            int index1 = timelineName.IndexOf("(");
            int index2 = timelineName.IndexOf(")");
            string index = timelineName.Substring(index1 + 1, index2 - index1 - 1);

            fgiPointGroupClip.template.fgiPtr = int.Parse(index) - 1;//因为序号从0开始，可以从下个版本改
            fgiPointGroupClip.template.pointsPtr = 1;//居中


            clip = imageBarTrack.CreateClip<ImageBarClip>();
            //ImageBarClip imageBarClip = (ImageBarClip)(clip.asset);
            clip.duration = ClipLength;
            return timelineAsset;
        }
    }
}

