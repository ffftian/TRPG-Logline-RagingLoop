using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using System;

namespace Transition
{
    [TrackColor(0.8f,0.7f,0.3f)]
    [TrackBindingType(typeof(Image))]
    [TrackClipType(typeof(YuzuSoftTransitionClip))]
    public class YuzuSoftTransitionTrack : TrackAsset
    {


        /// <summary>
        /// 中间过渡图片
        /// </summary>
        //public ExposedReference<Sprite> transitionsSprite;
        protected Material TransitionMaterial
        {
            get
            {
                if (transitionMaterial == null)
                {
                    transitionMaterial = new Material(Shader.Find("Miao/RuleTransition"));
                }
                return transitionMaterial;
            }
        }
        //protected Material FlipMaterial
        //{
        //    get
        //    {
        //        if (flipMaterial == null)
        //        {
        //            flipMaterial = new Material(Shader.Find("Miao/CubeTransition"));
        //        }
        //        return flipMaterial;
        //    }
        //}
        private Material transitionMaterial;//反转是由一个特殊参数组成的
        public Material flipMaterial;
        //MiaoDefault

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            //var  TransitionMaterial = new Material(Shader.Find("Sprites/MiaoDefault"));
            // var t2 = new Material(Shader.Find("Miao/CubeTransition"));
            ScriptPlayable<YuzuSoftTransitionMixBehaviour> scriptPlayable = ScriptPlayable<YuzuSoftTransitionMixBehaviour>.Create(graph, inputCount);
            YuzuSoftTransitionMixBehaviour yuzuSoftTransitionMixBehaviour = scriptPlayable.GetBehaviour();
            yuzuSoftTransitionMixBehaviour.transitionMaterial = TransitionMaterial;
            yuzuSoftTransitionMixBehaviour.flipMaterial = flipMaterial;
            return scriptPlayable;
        }
    }
}

