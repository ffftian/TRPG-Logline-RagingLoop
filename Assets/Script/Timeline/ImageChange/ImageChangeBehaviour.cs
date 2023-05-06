using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[Serializable]
public class ImageChangeBehaviour : PlayableBehaviour
{

    public Sprite changeSprite;
    protected Sprite baseSprite;
    public Image image { get; set; }


    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (image == null)
        {
            image = playerData as Image;
            baseSprite = image.sprite;
        }
        if (image.sprite != changeSprite)
        {
            image.sprite = changeSprite;
        }
    }
    public override void OnPlayableDestroy(Playable playable)
    {
        if(!Application.isPlaying)
        {
            if (image != null)
            {
                image.sprite = baseSprite;
            }
        }
    }
}

