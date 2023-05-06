using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class FgiDressGroup
{
    /// <summary>
    /// 使用的服装
    /// </summary>
    public List<string> diffName = new List<string>();
}

/// <summary>
/// 衣服名称
/// </summary>
[Serializable]
public class FgiDressInfo
{
    /// <summary>
    /// 衣服名称
    /// </summary>
    public string dress;
    /// <summary>
    /// 差异组
    /// </summary>
    public List<FgiDressGroup> diff = new List<FgiDressGroup>();
}

