using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RecordTestAsset : ScriptableObject
{
    public object A;
}

public class RecordTestAssetT<T> : SerializedScriptableObject
{
    public T A;
}