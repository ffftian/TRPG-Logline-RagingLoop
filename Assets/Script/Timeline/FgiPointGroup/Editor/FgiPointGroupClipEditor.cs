using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
namespace Galgame
{
    [CustomEditor(typeof(FgiPointGroupClip))]
    public class FgiPointGroupClipEditor : Editor
    {
        private FgiPointGroupClip component;

        private FgiPointGroup fgiPointGroup
        {
            get
            {
                return component.template.fgiPointGroup;
            }
        }
        private int PointsPtr
        {
            get
            {
                return component.template.pointsPtr;
            }
            set
            {
                component.template.pointsPtr = value;
            }
        }
        private int FgiPtr
        {
            get
            {
                return component.template.fgiPtr;
            }
            set
            {
                component.template.fgiPtr = value;
            }
        }
        string[] fgis;
        string[] points;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (component != null)
            {

                FgiPtr = EditorGUILayout.Popup("角色选择", FgiPtr, fgis);
                PointsPtr = EditorGUILayout.Popup("位置点", PointsPtr, points);
            }
        }

        public void OnEnable()
        {
            component = (FgiPointGroupClip)target;
            fgis = fgiPointGroup.fgiUnits.Select(s => s.name).ToArray();

            //standPoints = fgiPointGroup.Points.Select(s => s.name).ToArray();

            points = new string[fgiPointGroup.Points.Length + 1];
            for (int i = 0; i < fgiPointGroup.Points.Length; i++)
            {
                points[i] = fgiPointGroup.Points[i].name;
            }
            points[fgiPointGroup.Points.Length] = "取消站位";

        }
    }
}

