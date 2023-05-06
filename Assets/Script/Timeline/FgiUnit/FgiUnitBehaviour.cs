using System;
using UnityEngine.Playables;
using UnityEngine;
namespace Galgame
{
    [Serializable]
    public class FgiUnitBehaviour : PlayableBehaviour
    {
        public FgiUnit fgiUnit { get; set; }
        public FgiData[] fgiDress;
        public FgiData[] fgiFace;

        /// <summary>
        /// 画布觉得你正常没换姿势，不用换但头像不知道
        /// </summary>
        /// <param name="playable"></param>
        /// <param name="info"></param>
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (fgiUnit == null || fgiDress == null || fgiFace == null) return;
            if (Application.isPlaying)
            {
               
                fgiUnit.TweenChangeDress(fgiDress);
                fgiUnit.TweenChangeFace(fgiFace);
            }
            else
            {
                fgiUnit.ChangeDress(fgiDress);
                fgiUnit.ChangeFace(fgiFace);
            }
        }
    }
}

