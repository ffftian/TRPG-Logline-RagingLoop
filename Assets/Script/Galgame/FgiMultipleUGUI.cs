using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class FgiMultipleUGUI : FgiUGUIBase
{
    public FgiAsset[] fgiAssets;
    public override FgiAsset currentFgiAsset
    {
        get
        {
            return fgiAssets[assetIndex];
        }
    }

     
    

    //[HideInInspector]
    public int assetIndex
    {
        get
        {
            return fgiIndex.assetIndex;
        }
        set
        {
            fgiIndex.assetIndex = value;
        }
    }

}

