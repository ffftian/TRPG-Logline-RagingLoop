using Spine.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Timeline;

namespace Miao
{


    [TrackColor(0.8f, 0.8f, 0.8f)]
    [TrackBindingType(typeof(SpriteRenderer))]
    [TrackClipType(typeof(SpriteTransitionClip))]
    public class SpriteTransitionTrack : TrackAsset
    {
        /// <summary>
        /// 要转换成的图片
        /// </summary>
        public ExposedReference<Sprite> transition;

    }
}
