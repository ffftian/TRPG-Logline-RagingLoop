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

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (component == null)
        {
            component = playerData as RectTransform;
            baseAnchoredPosition = component.anchoredPosition;
        }

        int inputCount = playable.GetInputCount();


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

