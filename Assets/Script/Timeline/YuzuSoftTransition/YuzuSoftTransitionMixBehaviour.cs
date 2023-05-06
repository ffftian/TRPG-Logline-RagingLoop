using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Transition
{
    public class YuzuSoftTransitionMixBehaviour : PlayableBehaviour
    {
        public Image background;
        public Sprite baseSprite;
        public Material baseMaterial;

        public Material transitionMaterial;
        public Material flipMaterial;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);
            if (background == null)
            {
                background = playerData as Image;
                if (background == null)
                {
                    return;
                }
                baseSprite = background.sprite;
                baseMaterial = background.material;
            }
            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                if (inputWeight == 1)
                {
                    var inputPlayable = (ScriptPlayable<YuzuSoftTransitionBehaviour>)playable.GetInput(i);
                    YuzuSoftTransitionBehaviour behaviour = inputPlayable.GetBehaviour();
                    ///子项从0到1
                    float Progress;
                    if (behaviour.reverse)
                    {
                        Progress = 1 - (float)(inputPlayable.GetTime() / inputPlayable.GetDuration());
                    }
                    else
                    {
                        Progress = (float)(inputPlayable.GetTime() / inputPlayable.GetDuration());
                    }
                    if (background.sprite != behaviour.mainTexture&& behaviour.mainTexture!=null)
                    {
                        background.sprite = behaviour.mainTexture;
                    }

                    switch (behaviour.transitionType)
                    {
                        case TransitionType.flipGround:
                            if (background.material != flipMaterial)
                            {
                                background.material = flipMaterial;
                                background.SetNativeSize();
                                background.material.SetFloat("_Fade", behaviour.fadeBar);
                            }
                            if(background.material.GetTexture("_FadeTex") != behaviour.transitionRule)
                            {
                                background.material.SetTexture("_FadeTex", behaviour.transitionRule);
                            }
                            background.material.SetFloat("_Progress", Progress);
                            
                            break;
                        case TransitionType.flipFade:
                            if (background.material != flipMaterial)
                            {
                                background.material = flipMaterial;
                                background.SetNativeSize();
                            }
                            if (background.material.GetTexture("_FadeTex") != behaviour.transitionRule)
                            {
                                background.material.SetTexture("_FadeTex", behaviour.transitionRule);
                            }
                            background.material.SetFloat("_Fade", Progress);

                            break;

                        case TransitionType.TransitionGround:
                            if (background.material != transitionMaterial)
                            {
                                background.material = transitionMaterial;
                                background.SetNativeSize();
                            }
                            if (background.material.GetTexture("_MixTex") != behaviour.transitionRule)
                            {
                                background.material.SetTexture("_MixTex", behaviour.transitionRule);
                            }
                            background.material.SetFloat("_Progress", Progress);
                            break;
                    }
                }
            }

        }

        public override void OnPlayableDestroy(Playable playable)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                if (background != null)
                {
                    background.sprite = baseSprite;
                    background.material = baseMaterial;
                    background.material.SetFloat("_Progress", 0);
                    background.material.SetFloat("_Fade", 0);
                    background.SetNativeSize();
                }
            }
#endif
        }
    }
}




