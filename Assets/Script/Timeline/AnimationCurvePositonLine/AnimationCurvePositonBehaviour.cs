using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Playables;
using UnityEngine;

namespace Miao
{
    [Serializable]
    public class AnimationCurvePositonBehaviour : PlayableBehaviour
    {
        public AnimationCurve curveX = AnimationCurve.EaseInOut(0, 0, 1, 1);
        public AnimationCurve curveY = AnimationCurve.EaseInOut(0, 0, 1, 1);
    }
}
