#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundRecord : MonoBehaviour, IRecord, IRecover
{
    public LoglineComponent loglineComponent;
    public Image backGround;
    private RecordSpriteAsset recordAsset;

    protected int? length
    {
        get
        {
            return loglineComponent.messageAssetLength;
        }
    }


    public void StartRecord(string messageAssetName, int currentIndex)
    {
        if (recordAsset == null)
        {
            recordAsset = RecordSpriteAsset.GetAsset(messageAssetName, (int)length);
        }

    }

    public void RecordData(string messageAssetName, string targetName, int currentIndex, object value)
    {
        Sprite sprite =  value as Sprite;
        if (sprite!=null)
        {
            recordAsset.RecordSprite[currentIndex] = sprite;

        }
    }

    public void RecoverData(string messageAssetName, int currentIndex)
    {
        if (recordAsset == null)
        {
            recordAsset = RecordSpriteAsset.GetAsset(messageAssetName, (int)length);
        }
        Sprite sprite = recordAsset.GetNearSprite(currentIndex);

        if (sprite != null)
        {
            backGround.sprite = sprite;
        }

    }
}
#endif
