using DG.Tweening;
using MiaoTween;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
namespace Galgame
{
    public class CameraControlMixBehaviour : PlayableBehaviour
    {
        private Camera component;
        public float baseSize;
        public Vector3 basePosition;
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (component == null)
            {
                component = playerData as Camera;
                if(component == null)
                {
                    return;
                }
                baseSize = component.orthographicSize;
                basePosition = component.transform.position;
            }
            float size = 0;
            Vector3 position = Vector3.zero;
            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                if (inputWeight > 0)
                {
                    var inputPlayable = (ScriptPlayable<CameraControlBehaviour>)playable.GetInput(i);
                    CameraControlBehaviour colorImageBehaviour = inputPlayable.GetBehaviour();

                    size += colorImageBehaviour.orthographicSize * inputWeight;
                    position += colorImageBehaviour.position * inputWeight;
                }
            }
            if (size != 0)
            {
                component.orthographicSize = size;
                component.transform.position = position;
            }
            else
            {
                component.orthographicSize = baseSize;
                component.transform.position = basePosition;
            }
        }


        public override void OnPlayableDestroy(Playable playable)
        {
            base.OnPlayableDestroy(playable);
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                if (component != null)
                {
                    component.orthographicSize = baseSize;
                    component.transform.position = basePosition;
                }
            }
#endif
        }
    }
}

