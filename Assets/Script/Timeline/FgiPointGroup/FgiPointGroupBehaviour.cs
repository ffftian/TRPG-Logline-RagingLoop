using System;
using UnityEngine;
using UnityEngine.Playables;
namespace Galgame
{
    [Serializable]
    public class FgiPointGroupBehaviour : PlayableBehaviour
    {
        public FgiPointGroup fgiPointGroup { get; set; }
        public int fgiPtr;
        public int pointsPtr;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (Application.isPlaying)
            {
                fgiPointGroup.TweenChangeFgiPoint(fgiPtr, pointsPtr);
            }
            else
            {
                fgiPointGroup.ChangeFgiPoint(fgiPtr, pointsPtr);
            }
        }
    }
}
