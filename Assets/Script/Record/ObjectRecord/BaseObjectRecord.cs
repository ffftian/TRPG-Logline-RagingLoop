#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


public abstract class BaseObjectRecord : MonoBehaviour, IRecover
{
    public Transform recordObjectTransform;
    DialogRecordManager recordManager;
    virtual protected void OnValidate()
    {
        recordManager = this.GetComponent<DialogRecordManager>();
        recordManager.RecordDataCall -= RecordData;
        recordManager.RecordDataCall += RecordData;
    }

    abstract public void RecordData(string assetName, string targetName, int ptr, object value);
    abstract public void RecoverData(string messageAssetName, int currentIndex);
}
#endif