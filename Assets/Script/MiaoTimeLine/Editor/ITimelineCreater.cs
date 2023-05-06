using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Timeline;

public interface ITimelineCreater
{
    public TimelineAsset CreateMessageTimeLine(string timelineName, string Dialogue, string SpeakAssetName);
}

