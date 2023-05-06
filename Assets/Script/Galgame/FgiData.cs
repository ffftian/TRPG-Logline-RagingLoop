using System;
using UnityEngine;

[Serializable]
public class FgiData
{
    public string name;//1
    public int left;
    public int top;
    public int width;
    public int hegiht;
    /// <summary>
    /// 好像是面部分级
    /// </summary>
    public int type;
    /// <summary>
    /// 透明度
    /// </summary>
    public byte opacity;
    /// <summary>
    /// 可视层级
    /// </summary>
    public int visible;
    /// <summary>
    /// 索引图片的id
    /// </summary>
    public int layerId;
    /// <summary>
    /// 如果有group layer 就证明是表情，而本身group layer是用来索引最下面4个0,单纯做，layer id用的
    /// 
    /// </summary>
    public int groupLayerId;

    public Sprite sprite;
}
