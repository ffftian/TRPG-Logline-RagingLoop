using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[SettingsName("CommonTextSetting.asset")]
public class CommonTextSettings : BaseSettings<CommonTextSettings>
{
    public CommonTextAsset commonTextAsset;
    public Vector2Int messageListRange;
}
