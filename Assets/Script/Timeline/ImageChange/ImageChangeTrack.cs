using UnityEngine.Timeline;
using UnityEngine.UI;

[TrackBindingType(typeof(Image))]
[TrackClipType(typeof(ImageChangeClip))]
public class ImageChangeTrack : TrackAsset, IRecordTack
{
    public object SaveValue
    {
        get
        {
            object lastImage = null;
            double endTime = 0;
            foreach (var clip in GetClips())
            {

                if (endTime < clip.end)
                {
                    endTime = clip.end;

                    lastImage = (clip.asset as ImageChangeClip).template.changeSprite;
                }
            }
            return lastImage;
        }
    }
}