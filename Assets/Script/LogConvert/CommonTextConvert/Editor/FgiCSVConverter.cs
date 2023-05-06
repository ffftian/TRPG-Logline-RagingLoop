using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;



public class FgiCSVConverter : EditorWindow
{

    private TextAsset configTextAsset;
    private string txtConfigPath = string.Empty;
    private string folderPath = string.Empty;
    private string savePath = string.Empty;

    private TextAsset infoTextAsset;
    private string txtInfoPath = string.Empty;

    /// <summary>
    /// 文件名称
    /// </summary>
    private string fgiName = string.Empty;
    private bool canBuild;


    [MenuItem("文本编辑器/CSV转FgiAsset")]
    private static void OpenWindow()
    {
        FgiCSVConverter instance = GetWindow<FgiCSVConverter>("CSV转FgiAsset");
        instance.minSize = instance.maxSize = new Vector2(400, 400);
    }
    private void OnGUI()
    {


        #region 设置文件
        EditorGUILayout.LabelField("config文件位置");
        Rect txtConfigRect = EditorGUILayout.BeginHorizontal();
        txtConfigPath = EditorGUILayout.TextField(txtConfigPath);
        configTextAsset = (TextAsset)EditorGUILayout.ObjectField(configTextAsset, typeof(TextAsset), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("info文件位置");
        Rect txtInfoRect = EditorGUILayout.BeginHorizontal();
        txtInfoPath = EditorGUILayout.TextField(txtInfoPath);
        infoTextAsset = (TextAsset)EditorGUILayout.ObjectField(infoTextAsset, typeof(TextAsset), false);
        EditorGUILayout.EndHorizontal();


        #endregion

        #region 导出设置
        EditorGUILayout.HelpBox($"可转出的格式{nameof(FgiAsset)}", MessageType.Info);
        EditorGUI.BeginDisabledGroup(!canBuild);
        if (GUILayout.Button("开始转换", EditorStyles.miniButtonMid))
        {
            CSVToFgiAsset();
        }
        EditorGUI.EndDisabledGroup();
        if (GUILayout.Button("临时测试用button", EditorStyles.miniButtonMid))
        {
            FgiAsset messageAsset = ScriptableObject.CreateInstance<FgiAsset>();
            messageAsset.AnalysisInfo(infoTextAsset.text, fgiName, folderPath);
        }
        #endregion

        #region 拖拽碰撞处理
        Object[] dargPath = DragAndDrop.objectReferences;
        int objectLength = dargPath.Length;
        if (objectLength != 0)
        {
            Event currentEvent = Event.current;
            if (txtConfigRect.Contains(currentEvent.mousePosition))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (currentEvent.type == EventType.DragExited)
                {
                    if (dargPath[0] is TextAsset)
                    {
                        configTextAsset = (TextAsset)dargPath[0];
                        txtConfigPath = DragAndDrop.paths[0];
                        int spIndex = txtConfigPath.LastIndexOf('/');
                        fgiName = txtConfigPath.Substring(spIndex + 1, txtConfigPath.Length - spIndex - 1);
                        fgiName = fgiName.Substring(0, fgiName.IndexOf('.'));
                        folderPath = txtConfigPath.Substring(0, spIndex);
                    }
                }
            }
            if (txtInfoRect.Contains(currentEvent.mousePosition))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (currentEvent.type == EventType.DragExited)
                {
                    if (dargPath[0] is TextAsset)
                    {
                        infoTextAsset = (TextAsset)dargPath[0];
                        txtInfoPath = DragAndDrop.paths[0];
                        int spIndex = txtInfoPath.LastIndexOf('/');
                        savePath = txtInfoPath.Substring(0, spIndex);
                    }
                }
            }
        }
        #endregion

        #region 导出提示设置
        string waringMessage;
        if (txtConfigPath.Length == 0 || txtInfoPath.Length == 0)
        {
            canBuild = false;
            waringMessage = "配置不完全";
        }
        else if (File.Exists($@"{savePath}\{configTextAsset.name}.asset"))
        {
            canBuild = false;
            waringMessage = "文件已包含";
        }
        else
        {
            canBuild = true;
            waringMessage = "可以构建";
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.HelpBox(waringMessage, canBuild ? MessageType.Info : MessageType.Error);
        EditorGUILayout.Space();
        #endregion
    }

    private void CSVToFgiAsset()
    {
        FgiAsset messageAsset = ScriptableObject.CreateInstance<FgiAsset>();
        messageAsset.Init(configTextAsset.text,infoTextAsset.text, fgiName, folderPath);
        EditorUtility.SetDirty(messageAsset);
        AssetDatabase.CreateAsset(messageAsset, $@"{savePath}\{configTextAsset.name}.asset");
        EditorGUIUtility.PingObject(messageAsset);
    }
}

