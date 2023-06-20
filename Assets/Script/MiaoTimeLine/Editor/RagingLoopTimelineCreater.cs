using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;
namespace RagingLoop
{
    public class RagingLoopTimelineCreater : MonoBehaviour
    {
        public string saveDirectory;
        public string DialogueDirectiory;

        public RagingLoopTimelineCreater(string saveTimelineDirectory, string DialogueDirectiory = @"Assets\AssetSpeak")
        {
            this.saveDirectory = saveTimelineDirectory;
            this.DialogueDirectiory = DialogueDirectiory;
            if (!Directory.Exists(saveTimelineDirectory))
            {
                Directory.CreateDirectory(saveTimelineDirectory);
            }
        }
        public TimelineAsset CreateMessageTimeLine(string timelineName, string dialogue,string translateDialogue, string SpeakAssetName)
        {
            if (File.Exists($@"{saveDirectory}\{timelineName}.playable"))
            {
                Debug.Log($@"{saveDirectory}\{timelineName}.playable拥有文件，跳过");
                return null;
            }
            TimelineAsset timelineAsset = ScriptableObject.CreateInstance<TimelineAsset>();//创建一个资源
            AssetDatabase.CreateAsset(timelineAsset, $@"{saveDirectory}\{timelineName}.playable");//先创建资源到本地，AssetDatabase源码里有检测资源函数，不这样他不会写到本地。

            ////轨道资源创建
            StandSlotTrack standSlotTrack = timelineAsset.CreateTrack<StandSlotTrack>();

            if (SpeakAssetName != "旁白")
            {
                StandSlotPointGroupTrack standSlotPointGroup = timelineAsset.CreateTrack<StandSlotPointGroupTrack>();
                standSlotPointGroup.CreateClip<StandSlotPointGroupClip>();
            }
            DialogueControlTrack dialogueControlTrack = timelineAsset.CreateTrack<DialogueControlTrack>("Dialog");
            DialogueControlTrack translateDialogueControlTrack = timelineAsset.CreateTrack<DialogueControlTrack>("DialogTrans");
            AudioTrack SpeakTrack = timelineAsset.CreateTrack<AudioTrack>("Speak");
            string audioPath = $@"{DialogueDirectiory}\{SpeakAssetName}.wav";

            


            ////赋值
            AudioClip Speak = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath);
            float ClipLength = 0;
            if (Speak != null)
            {
                SpeakTrack.CreateClip(Speak);
                ClipLength = Speak.length;
            }

            
            TimelineClip clip = dialogueControlTrack.CreateClip<DialogueControlClip>();
            DialogueControlClip clipResource = (DialogueControlClip)clip.asset;
            clipResource.template.dialogue = dialogue;
            clipResource.template._speed = 20;
            clip.displayName = timelineName;
            if (ClipLength != 0)
            {
                clip.duration = ClipLength;
            }
            else
            {
                ClipLength = dialogue.Length * 0.10f;
                clip.duration = ClipLength;
            }
            TimelineClip transClip = translateDialogueControlTrack.CreateClip<DialogueControlClip>();
            DialogueControlClip transClipResource = (DialogueControlClip)transClip.asset;
            transClipResource.template.dialogue = translateDialogue;
            transClipResource.template._speed = 20;
            transClip.displayName = timelineName;
            if (ClipLength != 0)
            {
                transClip.duration = ClipLength;
            }
            else
            {
                ClipLength = dialogue.Length * 0.10f;
                transClip.duration = ClipLength;
            }

            //clip = dialogueControlTrack.CreateClip<DialogueControlClip>();
            //clipResource = (DialogueControlClip)clip.asset;
            //clipResource.template.dialogue = translateDialogue;
            //clipResource.template._speed = 100;
            //clip.displayName = timelineName;
            //if (ClipLength != 0)
            //{
            //    clip.duration = ClipLength;
            //}
            //else
            //{
            //    ClipLength = dialogue.Length * 0.12f;
            //    clip.duration = ClipLength;
            //}


            return timelineAsset;
        }
    }
}
