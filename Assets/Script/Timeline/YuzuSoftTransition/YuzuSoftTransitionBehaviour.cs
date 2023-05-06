using System;
using UnityEngine;
using UnityEngine.Playables;
public enum TransitionType
{
    UnBind,
    TransitionGround,
    flipGround,
    flipFade,
}
//[Serializable]
public class YuzuSoftTransitionBehaviour : PlayableBehaviour
{

    public TransitionType transitionType { get; set; }
    public Texture2D transitionRule { get; set; }
    public Sprite mainTexture { get; set; }
    public bool reverse { get; set; }
    public float fadeBar;
    //public Texture2D fadeTexture { get; set; }
}

