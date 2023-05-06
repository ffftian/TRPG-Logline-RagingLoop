using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(ImageButton))]
public class ImageButtonEditor : ButtonEditor
{
    private SerializedProperty NormalProperty;
    private SerializedProperty MouseInProperty;
    private SerializedProperty MouseDownProperty;
    protected override void OnEnable()
    {
        base.OnEnable();
        NormalProperty = serializedObject.FindProperty("Normal");
        MouseInProperty = serializedObject.FindProperty("MouseIn");
        MouseDownProperty = serializedObject.FindProperty("MouseDown");
    }

    public override void OnInspectorGUI()
    {

        EditorGUILayout.PropertyField(NormalProperty);//属性字段
        EditorGUILayout.PropertyField(MouseInProperty);//属性字段
        EditorGUILayout.PropertyField(MouseDownProperty);//属性字段
        serializedObject.ApplyModifiedProperties();//应用修改的属性，这个不写就失效，很神奇
        base.OnInspectorGUI();
    }
}
