using Dice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

public class ShakeMixBehaviour : PlayableBehaviour
{
    RectTransform component;

    Vector2 baseAnchoredPosition;

    float shakeCilp = 0.02f;
    float timer;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        bool active = false;
        int inputCount = playable.GetInputCount();
        if (component == null)
        {
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                if (inputWeight > 0)
                {
                    active = true;
                }
            }
        }
        else
        {
            active = true;
        }
        if (active)
        {
            if (component == null)
            {
                component = playerData as RectTransform;
                baseAnchoredPosition = component.anchoredPosition;
            }
            timer += Time.deltaTime;
            if(timer >= shakeCilp)
            {
                timer = 0;
                Vector2 AnchoredPositon = Vector3.zero;
                for (int i = 0; i < inputCount; i++)
                {
                    float inputWeight = playable.GetInputWeight(i);
                    var inputPlayable = (ScriptPlayable<ShakeBehaviour>)playable.GetInput(i);
                    ShakeBehaviour diceProBehaviour = inputPlayable.GetBehaviour();

                    float x = UnityEngine.Random.Range(-diceProBehaviour.shakeRange.x, diceProBehaviour.shakeRange.x);
                    float y = UnityEngine.Random.Range(-diceProBehaviour.shakeRange.y, diceProBehaviour.shakeRange.y);
                    AnchoredPositon += inputWeight * new Vector2(x, y);
                }
                component.anchoredPosition = baseAnchoredPosition + AnchoredPositon;
                component.ForceUpdateRectTransforms();
            }
           
        }
    }
    public override void OnPlayableDestroy(Playable playable)
    {
        base.OnPlayableDestroy(playable);
        if (component != null)
        {
            component.anchoredPosition = baseAnchoredPosition;
        }
    }


}

