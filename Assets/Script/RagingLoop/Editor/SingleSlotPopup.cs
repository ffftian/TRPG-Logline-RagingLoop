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
    public bool OnInspectorGUI(ref int Index)
    {
        bool dirty = false;
        int cashe = Index;
        Index = EditorGUILayout.Popup(name, Index, slots);
        if(cashe!= Index)
        {
            dirty = true;
        }
        return dirty;
    }
}

