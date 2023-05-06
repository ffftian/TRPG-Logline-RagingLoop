using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class SingleSlotPopup
{
    string[] slots;
    string name;
    public SingleSlotPopup(string[] slots,string name)
    {
        this.slots = slots;
        this.name = name;
    }
    public void OnInspectorGUI(ref int Index)
    {
        Index = EditorGUILayout.Popup(name, Index, slots);
    }
}

