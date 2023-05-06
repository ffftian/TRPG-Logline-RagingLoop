using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


namespace Miao
{
    /// <summary>
    /// 千恋万花转场的形式
    /// </summary>
    [System.Serializable]
    public class SpriteTransitionClip : PlayableAsset, ITimelineClipAsset
    {

        public SpriteTransitionBehaviour template;

        public ClipCaps clipCaps => ClipCaps.None;
        public ExposedReference<Sprite> transitionSprite;

        //public ExposedReference<Sprite> change;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<SpriteTransitionBehaviour>.Create(graph, template);
            SpriteTransitionBehaviour textureTransitionBehaviour =  playable.GetBehaviour();
            textureTransitionBehaviour.transitionSprite = transitionSprite.Resolve(graph.GetResolver());


            return playable;
        }
    }
}

