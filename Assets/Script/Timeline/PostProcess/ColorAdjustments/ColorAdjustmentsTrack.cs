using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Timeline;
namespace PostProcess
{
    [TrackBindingType(typeof(Volume))]
    [TrackClipType(typeof(ColorAdjustmentsClip))]
    public class ColorAdjustmentsTrack : BaseMixTrack<ColorAdjustmentsMixBehaviour>
    {

    }
}

