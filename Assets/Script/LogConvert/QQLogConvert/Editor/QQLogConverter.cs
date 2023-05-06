using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 将txt文本转换为<paramref name="TextAsset"/>序列化化保存和读取的格式。
/// </summary>
public class QQLogConverter : EditorWindow
{
    private Rect rect;
    private TextAsset textAsset;
    private string txtPath = string.Empty;
    private string outputFolderPath = "Assets/AssetDialogue";
    private string waringMessage;
    private bool canBuild = false;

    [MenuItem("文本编辑器/TXT转MessageAsset")]
    private static void OpenWindow()
    {
        QQLogConverter instance = GetWindow<QQLogConverter>("TXT转MessageAsset");
        instance.minSize = instance.maxSize = new Vector2(400, 400);
    }

    private void OnGUI()
    {
        #region 设置文件
        EditorGUILayout.LabelField("txt文件位置");
        Rect txtRect = EditorGUILayout.BeginHorizontal();
        txtPath = EditorGUILayout.TextField(txtPath);
        textAsset = (TextAsset)EditorGUILayout.ObjectField(textAsset, typeof(TextAsset), false);
        EditorGUILayout.EndHorizontal();
        #endregion

        #region 导出文件目录
        Rect folderRect = EditorGUILayout.BeginHorizontal();
        outputFolderPath = EditorGUILayout.TextField("导出文件目录位置", outputFolderPath);
        EditorGUILayout.EndHorizontal();
        #endregion

        #region 导出设置
        EditorGUILayout.HelpBox($"可转出的格式{nameof(QQTextAsset)}|{nameof(CommonTextAsset)}", MessageType.Info);
        EditorGUI.BeginDisabledGroup(!canBuild);
        if (GUILayout.Button("开始转换txt为QQMessageAsset", EditorStyles.miniButtonMid))
        {
            TxtToQQLogData();
        }
        EditorGUI.EndDisabledGroup();
        EditorGUI.BeginDisabledGroup(txtPath.Length == 0 || outputFolderPath.Length == 0 || !File.Exists($@"{outputFolderPath}\{textAsset.name}.asset"));
        {
            if (GUILayout.Button("强制覆盖txt为QQMessageAsset", EditorStyles.miniButtonMid))
            {
                TxtOverrideQQLogData();
            }
        }
        EditorGUI.EndDisabledGroup();
        #endregion

        #region 拖拽碰撞处理

        Object[] dargPath = DragAndDrop.objectReferences;
        int objectLength = dargPath.Length;
        if (objectLength != 0)
        {
            Event currentEvent = Event.current;
            if (txtRect.Contains(currentEvent.mousePosition))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (currentEvent.type == EventType.DragExited)
                {
                    if (dargPath[0] is TextAsset)
                    {
                        textAsset = (TextAsset)dargPath[0];
                        txtPath = DragAndDrop.paths[0];
                    }
                }
            }
            if (folderRect.Contains(currentEvent.mousePosition))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (currentEvent.type == EventType.DragExited)
                {
                    if (dargPath[0] is DefaultAsset)
                    {
                        outputFolderPath = DragAndDrop.paths[0];
                    }
                }
            }
        }
        #endregion

        #region 导出提示设置
        if (txtPath.Length == 0 || outputFolderPath.Length == 0)
        {
            canBuild = false;
            waringMessage = "参数不完全";
        }
        else if (File.Exists($@"{outputFolderPath}\{textAsset.name}.asset"))
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

    private void TxtToQQLogData()
    {

        if (TextTool.IsQQLog(textAsset.text))
        {
            QQTextAsset messageAsset = ScriptableObject.CreateInstance<QQTextAsset>();
            messageAsset.messageDataList = TextTool.QQLogSplit<QQTextData>(textAsset.text, Error).ToList();
            AssetDatabase.CreateAsset(messageAsset, $@"{outputFolderPath}\{textAsset.name}.asset");
            EditorGUIUtility.PingObject(messageAsset);
        }
        else if(TextTool.IsCommonLog(textAsset.text))
        {
            CommonTextAsset messageAsset = ScriptableObject.CreateInstance<CommonTextAsset>();
            var bb = TextTool.CommonLogSplit<BaseTextData>(textAsset.text, Error);

            messageAsset.messageDataList = TextTool.CommonLogSplit<CommonTextData>(textAsset.text, Error).ToList();
            AssetDatabase.CreateAsset(messageAsset, $@"{outputFolderPath}\{textAsset.name}.asset");
            EditorGUIUtility.PingObject(messageAsset);
        }

    }
    private void TxtOverrideQQLogData()
    {
        if (TextTool.IsQQLog(textAsset.text))
        {
            QQTextAsset messageAsset = AssetDatabase.LoadAssetAtPath<QQTextAsset>($@"{outputFolderPath}\{textAsset.name}.asset");
            messageAsset.messageDataList = TextTool.QQLogSplit<QQTextData>(textAsset.text, Error).ToList();
            EditorUtility.SetDirty(messageAsset);
            EditorGUIUtility.PingObject(messageAsset);
        }
        else if (TextTool.IsCommonLog(textAsset.text))
        {
            CommonTextAsset messageAsset = AssetDatabase.LoadAssetAtPath<CommonTextAsset>($@"{outputFolderPath}\{textAsset.name}.asset");

            messageAsset.messageDataList = TextTool.CommonLogSplit<CommonTextData>(textAsset.text, Error).ToList();
            EditorUtility.SetDirty(messageAsset);
            EditorGUIUtility.PingObject(messageAsset);
        }
        AssetDatabase.SaveAssets();
    }
    private void Error(System.Exception exception,string error)
    {
        Debug.LogError($"失败的转换<{exception}>,错误段落为{error}");
    }



    private void OnEnable()
    {
        outputFolderPath = EditorPrefs.GetString(nameof(QQLogConverter) + nameof(outputFolderPath));
    }
    private void OnDisable()
    {

        EditorPrefs.SetString(nameof(QQLogConverter) + nameof(outputFolderPath), outputFolderPath);
    }

}