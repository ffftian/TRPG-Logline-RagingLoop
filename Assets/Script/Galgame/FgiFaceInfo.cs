using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class FgiFaceGroup
{
    public bool isFace;//是否该从表情组查找
    public string faceKey;
}

/// <summary>
/// 面部信息
/// </summary>
[Serializable]
public class FgiFaceInfo
{
    /// <summary>
    /// 标签ID
    /// </summary>
    public string face;

    /// <summary>
    /// 实际画面表现索引
    /// </summary>
    public  List<FgiFaceGroup> faceGroup = new List<FgiFaceGroup>();
}

