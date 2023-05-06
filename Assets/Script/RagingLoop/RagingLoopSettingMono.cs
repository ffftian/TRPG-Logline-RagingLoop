using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RagingLoopSettingMono : MonoBehaviour
{
    public static RagingLoopSettingMono Instance;

    public BustAdvConfig bustAdvConfig;

    public void Awake()
    {
        Instance = this;

    }

}

