using System;
using UnityEngine.Playables;
using UnityEngine;
namespace Galgame
{
    /// <summary>
    /// 因为顺序原因，渐变所有效果都写在Mix下
    /// </summary>
    [Serializable]
    public class CameraControlBehaviour : PlayableBehaviour
    {
        public float orthographicSize = 5.4f;
        public Vector3 position = new Vector3(0,0,-10);
    }
}
