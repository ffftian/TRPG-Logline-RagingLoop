using Miao;
using Spine.Unity;
using Spine.Unity.Playables;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// 这是一个演示，用于在Dialog脚本选中特定轨道时，预先进行赋值
/// </summary>
public static class TimelineSelectExtension
{
    [TimelineSelectAttribute]
    public static void CameraFocusRole(DialogComponent dialogComponent, BaseTextData serialData, TrackAsset track)
    {
        CameraFocusTrack cameraFocusTrack = track as CameraFocusTrack;
        if (cameraFocusTrack != null)
        {
            dialogComponent.playable.SetGenericBinding(track, Camera.main);
            //var clips = cameraFocusTrack.GetClips();
            //foreach (TimelineClip timelineClip in clips)
            //{
            //    ExposedReference<Transform> asExposedReference = new ExposedReference<Transform>();
            //    asExposedReference.defaultValue = dialogComponent.roleGroup.Find(serialData.roleName);
            //    CameraFocusClip focusClip = (CameraFocusClip)timelineClip.asset;
            //    focusClip.focus = asExposedReference;
            //    ///默认拥有的类，没准以后会用到。
            //    //ControlPlayableAsset controlPlayableAsset;
            //}
        }
    }
    [TimelineSelectAttribute]
    public static void RoleDialogue(DialogComponent dialogComponent, BaseTextData serialData, TrackAsset track)
    {
        DialogueTextMeshProTrack dialogueTextMeshProTrack = track as DialogueTextMeshProTrack;

        if (dialogueTextMeshProTrack != null)
        {

            if (dialogComponent.playable.GetGenericBinding(track) != null)
            {
                //Transform target = dialogComponent.roles.Find((a) => a.name + "TextMeshPro");
                Transform target = null;
                for (int i=0;i< dialogComponent.roles.Count;i++)
                {
                    target = dialogComponent.roles[i].transform.Find(serialData.GroupID + "TextMeshPro");
                }

                //Transform target =  dialogComponent.roleGroup.Find(serialData.roleName + "TextMeshPro");
                if (target != null)
                {
                    var text = target.GetComponent<TyperDialogTextMeshPro>();
                    dialogComponent.playable.SetGenericBinding(track, text);
                }
            }
        }

    }

}
