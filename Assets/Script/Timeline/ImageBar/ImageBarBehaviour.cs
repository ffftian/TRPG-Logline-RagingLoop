using System;
using UnityEngine.Playables;
using UnityEngine.UI;

[Serializable]
public class ImageBarBehaviour : PlayableBehaviour
{
    private Image image;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (image == null)
        {
            image = (Image)playerData;
            if (image == null)
            {
                return;
            }
        }

        image.fillAmount = (float)(playable.GetTime() / playable.GetDuration());


    }
    public override void OnPlayableDestroy(Playable playable)
    {
        if (image != null)
        {
            image.fillAmount = 0;
        }
    }
}

