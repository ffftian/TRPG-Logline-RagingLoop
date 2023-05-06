using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AlphaSet : MonoBehaviour
{
    new public Renderer renderer;
    public Texture2D alphaTexture;

    [Button]
    private void SetPC()
    {

        //renderer.material.SetTexture("_MainTex", alphaTexture);
       //var tp =  renderer.material.GetTexture("_ExternalAlpha");
        renderer.material.SetTexture("External Alpha", alphaTexture);

      

        var tp = renderer.material.GetTexture("External Alpha");

    }
}

