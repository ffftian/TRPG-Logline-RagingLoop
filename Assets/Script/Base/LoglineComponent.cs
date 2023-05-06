using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

abstract public class LoglineComponent : MonoBehaviour
{
    public PlayableDirector playable;
    public int serialPtr;
    public TimelineAsset useTimeLineAsset;
    public Text NameText;
    public TyperDialogue dialogue;
    abstract public string CurrentRoleName { get; }
    abstract public int? messageAssetLength { get; }


    virtual public void Start()
    {
#if !UNITY_EDITOR
        serialPtr = 0;
#endif
        SetNameText(CurrentRoleName);
        MessageRuning();
    }
    abstract public void SetNameText(string name);

    public void MessageRuning()
    {
        playable.Play();
        playable.extrapolationMode = DirectorWrapMode.None;
        playable.stopped += Next;
    }

    virtual public void Next(PlayableDirector obj)
    {
        serialPtr++;
        if (serialPtr == messageAssetLength)
        {
            Debug.Log("<color=green>播放完毕</color> ");
            return;
        }
        SelectMessage(serialPtr);
        obj.Play();
    }
    abstract public void SelectMessage(int Serial);
}

