﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Miao
{
    public class AnimationCurvePositonClip : PlayableAsset, ITimelineClipAsset
    {
        public AnimationCurvePositonBehaviour template = new AnimationCurvePositonBehaviour();
        public bool looping = true;
        //public bool extrapolation = false;
        public override double duration
        {
            get
            {
                if(template.curveX[1].time ==0)
                {
                    return template.curveY[1].time;
                }
                else if (template.curveY[1].time == 0)
                {
                    return template.curveX[1].time;
                }
                return template.curveX[1].time < template.curveY[1].time ? template.curveX[1].time : template.curveY[1].time;
            }
        }

        public ClipCaps clipCaps
        {
            get
            {
                return ClipCaps.Blending | ClipCaps.AutoScale | (looping ? ClipCaps.Looping : 0)/*|(extrapolation ? ClipCaps.Extrapolation:0)*/;
            }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<AnimationCurvePositonBehaviour>.Create(graph, template);
            return playable;
        }
    }
}
