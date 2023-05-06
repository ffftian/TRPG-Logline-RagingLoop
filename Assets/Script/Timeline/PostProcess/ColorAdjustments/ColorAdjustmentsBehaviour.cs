
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal;
namespace PostProcess
{
    [Serializable]
    public class ColorAdjustmentsData
    {
        public float postExpsure;
        [Range(-100, 100)]
        public float contrast;
        public Color colorFilter = Color.white;
        [Range(-180, 180)]
        public float hueShift;
        [Range(-100, 100)]
        public float saturation;
    }


    [Serializable]
    public class ColorAdjustmentsBehaviour : PostProcessBehaviour<ColorAdjustments, ColorAdjustmentsData>
    {
        public override void Assign(ColorAdjustments postProcessComponent)
        {
            postProcessComponent.postExposure.value = data.postExpsure;
            postProcessComponent.contrast.value = data.contrast;
            postProcessComponent.colorFilter.value = data.colorFilter;
            postProcessComponent.hueShift.value = data.hueShift;
            postProcessComponent.saturation.value = data.saturation;

        }

        public override void Clear()
        {
            data.postExpsure = 0;
            data.contrast = 0;
            data.colorFilter = Color.white;
            data.hueShift = 0;
            data.saturation = 0;
        }

        public override void Disable(ColorAdjustments postProcessComponent)
        {
            postProcessComponent.postExposure.overrideState = false;
            postProcessComponent.contrast.overrideState = false;
            postProcessComponent.colorFilter.overrideState = false;
            postProcessComponent.hueShift.overrideState = false;
            postProcessComponent.saturation.overrideState = false;
        }

        public override void Enable(ColorAdjustments postProcessComponent)
        {
            postProcessComponent.postExposure.overrideState = true;
            postProcessComponent.contrast.overrideState = true;
            postProcessComponent.colorFilter.overrideState = true;
            postProcessComponent.hueShift.overrideState = true;
            postProcessComponent.saturation.overrideState = true;
        }

        public override void Obtain(ColorAdjustments postProcessComponent)
        {
            data.postExpsure = postProcessComponent.postExposure.value;
            data.contrast = postProcessComponent.contrast.value;
            data.colorFilter = postProcessComponent.colorFilter.value;
            data.hueShift = postProcessComponent.hueShift.value;
            data.saturation = postProcessComponent.saturation.value;
        }

        public override void Plus(ColorAdjustmentsData addData, float weight)
        {
            data.postExpsure = addData.postExpsure * weight;
            data.contrast = addData.contrast * weight;
            data.colorFilter = addData.colorFilter * weight;
            data.hueShift = addData.hueShift * weight;
            data.saturation = addData.saturation * weight;
        }
    }
}

