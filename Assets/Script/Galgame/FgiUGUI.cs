﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FgiUGUI : FgiUGUIBase
{
    public FgiAsset fgiAsset;

    public override FgiAsset currentFgiAsset
    {
        get
        {
            return fgiAsset;
        }
    }
}
