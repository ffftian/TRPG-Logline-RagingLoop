using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Sirenix.OdinInspector;
using System;

namespace Transition
{
    public class YuzuSoftTransitionClip : PlayableAsset, ITimelineClipAsset
    {
        //public YuzuSoftTransitionBehaviour template = new YuzuSoftTransitionBehaviour();
        public ClipCaps clipCaps => ClipCaps.None;
        public Sprite mainTexture;
        /// <summary>
        /// 图片改变蒙版
        /// </summary>
        public Texture2D transitionRuleTexture;
        public TransitionType transitionType;
        public bool reverse;
        [Range(0,1)]
        public float FadeBar = 0;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
           var scriptPlayable =  ScriptPlayable<YuzuSoftTransitionBehaviour>.Create(graph);
            YuzuSoftTransitionBehaviour yuzuSoftTransitionBehaviour = scriptPlayable.GetBehaviour();
            yuzuSoftTransitionBehaviour.transitionRule = transitionRuleTexture;
            yuzuSoftTransitionBehaviour.transitionType = transitionType;
            yuzuSoftTransitionBehaviour.mainTexture = mainTexture;
            yuzuSoftTransitionBehaviour.reverse = reverse;
            yuzuSoftTransitionBehaviour.fadeBar = FadeBar;
            return scriptPlayable;
        }

    }
}

