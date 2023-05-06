using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace RagingLoop
{
    public class StandSlotCreater : EditorWindow
    {
        private static string[] characters;
        public static string[] GetCharacters()//实际角色音频
        {
            if (characters == null)
            {
                characters = new string[26];//全角色音频了
                characters[0] = "房石阳明";
                characters[1] = "咩子";
                characters[2] = "美佐峰美辻";
                characters[3] = "芹泽千枝实";
                characters[4] = "回末李花子";
                characters[5] = "卷岛春";//这个回头单独练貉神的音频
                characters[6] = "织部泰长";
                characters[7] = "酿田近望";
                characters[8] = "织部义次";
                characters[9] = "织部香织";
                characters[10] = "能里清之介";
                characters[11] = "室匠";
                characters[12] = "卷岛宽造";
                characters[13] = "山胁多惠";
                characters[14] = "狼老头";
                characters[15] = "马宫久子";
                characters[16] = "桥本雄大";
                characters[17] = "绵羊";
                characters[18] = "人狼老头";
                characters[19] = "警察";
                characters[20] = "日口";
                characters[21] = "失足小孩";//音频非常少不建议训练
                characters[22] = "奈亚";//建议和失足小孩并一起训练
                characters[23] = "旅店老板娘";
                characters[24] = "旅店小鬼";
                characters[25] = "教会";
            }
            return characters;
        }

        public static string GetCharcracterName(string index)
        {
            int parse = int.Parse(index);
            if(GetCharacters().Length > parse - 1)
            {
                return $"{index}{GetCharacters()[parse - 1]}";
            }
            return index;
        }

        public StandPictureCreaterSettings settings;
        //public StandPictureCreaterSettings standPictureCreaterSettings;
        //public GameObject StandPrefab;
        //public RectTransform StandPictureRoot;
        //public string StandPicturePath = "Assets/Sources/RagingLoop/StandPicture";

        [MenuItem("文本编辑器/RagingLoop立绘生成器")]
        private static void OpenWindow()
        {
            StandSlotCreater instance = GetWindow<StandSlotCreater>("RagingLoop立绘生成器");

        }
        private void OnEnable()
        {
            settings = StandPictureCreaterSettings.LoadSettings();
        }
        private void OnDisable()
        {
           UnityEditor.EditorUtility.SetDirty(settings);
        }
        private void OnGUI()
        {
            settings.StandPrefab = (GameObject)EditorGUILayout.ObjectField("立绘型预制体", settings.StandPrefab, typeof(GameObject), false);
            settings.FacePrefab = (GameObject)EditorGUILayout.ObjectField("面部型预制体", settings.FacePrefab, typeof(GameObject), false);

            settings.StandPictureRoot = (RectTransform)EditorGUILayout.ObjectField("生成位置", settings.StandPictureRoot, typeof(RectTransform), true);
            Rect rect = EditorGUILayout.BeginHorizontal();
            settings.StandPicturePath = EditorGUILayout.TextField("图片组位置", settings.StandPicturePath);
            EditorGUILayout.EndHorizontal();

            #region 拖拽碰撞处理
            Object[] dargPath = DragAndDrop.objectReferences;
            if (dargPath.Length != 0)
            {
                Event currentEvent = Event.current;
                if (rect.Contains(currentEvent.mousePosition))
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    if (currentEvent.type == EventType.DragExited)
                    {
                        settings.StandPicturePath = DragAndDrop.paths[0];
                    }
                }
            }
            #endregion
            bool buildWor = false;
            #region 导出处理
            if (settings.StandPictureRoot == null || settings.StandPicturePath == null)
            {
                buildWor = true;
            }
            EditorGUI.BeginDisabledGroup(buildWor);
            GUILayout.FlexibleSpace();
            EditorGUILayout.HelpBox("填入参数", buildWor ? MessageType.Error : MessageType.Info);
            EditorGUILayout.Space();
            if (GUILayout.Button("开始合成组", EditorStyles.miniButtonMid))
            {
                CreateStart();
            }
            if (GUILayout.Button("（测试）合成一个预制体", EditorStyles.miniButtonMid))
            {
                Create(Path.Combine(settings.StandPicturePath, "01"));
            }
            EditorGUI.EndDisabledGroup();
            #endregion
        }

        public void CreateStart()
        {
            string[] dirs = Directory.GetDirectories(settings.StandPicturePath);
            for (int i = 0; i < dirs.Length; i++)
            {
                Create(dirs[i]);
            }
            UnityEditor.EditorUtility.SetDirty(settings.StandPictureRoot);
        }
        private void Create(string directoryPath)
        {
            string name = directoryPath.Substring(directoryPath.LastIndexOf('\\') + 1);
            Transform transName = settings.StandPictureRoot.Find(name);
            if (transName == null)
            {
                string[] fileDatas = Directory.GetFiles(directoryPath);
                bool isFace = fileDatas[0].Contains("face");

                if (isFace)
                {
                    StandSlotFace standSlotFace = ((GameObject)(PrefabUtility.InstantiatePrefab(settings.FacePrefab, settings.StandPictureRoot))).GetComponent<StandSlotFace>();

                    standSlotFace.name = GetCharcracterName(name);
                    //standSlotFace.name = name;
                    CreateStandSlotFace(standSlotFace, fileDatas);
                }
                else
                {
                    //try
                    //{
                        StandSlot standSlot = ((GameObject)(PrefabUtility.InstantiatePrefab(settings.StandPrefab, settings.StandPictureRoot))).GetComponent<StandSlot>();
                        standSlot.name = GetCharcracterName(name);
                        CreateStandSlot(standSlot, fileDatas);
                    //}
                    //catch(System.Exception e)
                    //{
                    //    Debug.LogError(e.Message);
                    //}
                }
            }
        }


        private void CreateStandSlot(StandSlot standSlot, string[] fileDatas)
        {
            int acceIndex = 0;
            int otherIndex = 0;
            for (int j = 0; j < fileDatas.Length; j++)
            {
                var data = AssetDatabase.LoadAssetAtPath<Sprite>(fileDatas[j]);//筛选Add
                if (data != null)
                {
                    if (data.name.Contains("brow"))
                    {
                        standSlot.casheBrow.Add(data);
                    }
                    else if (data.name.Contains("eye"))
                    {
                        standSlot.casheEye.Add(data);
                    }
                    else if (data.name.Contains("mouth"))
                    {
                        standSlot.casheMouth.Add(data);
                    }
                    else if (data.name.Contains("body"))
                    {
                        standSlot.casheBody.Add(data);
                    }
                    else if (data.name.Contains("acce"))
                    {
                        try
                        {
                            //standSloat.casheAcce.Add(data);
                            standSlot.acce[acceIndex].sprite = data;
                            standSlot.acce[acceIndex].SetNativeSize();
                            acceIndex++;
                        }catch(System.Exception e)
                        {
                            Debug.LogError(e.Message);
                        }
                    }
                    else
                    {
                        standSlot.other[otherIndex].sprite = data;
                        standSlot.other[otherIndex].SetNativeSize();
                        otherIndex++;
                    }

                    if (data.name == "0_brow")
                    {
                        standSlot.brow.sprite = data;
                        standSlot.brow.SetNativeSize();
                    }
                    else if (data.name == "0_eye")
                    {
                        standSlot.eye.sprite = data;
                        standSlot.eye.SetNativeSize();
                    }
                    else if (data.name == "0_mouth")
                    {
                        standSlot.mouth.sprite = data;
                        standSlot.mouth.SetNativeSize();
                    }
                    else if (data.name == "body_01")
                    {
                        standSlot.body.sprite = data;
                        standSlot.body.SetNativeSize();
                    }
                }
                else
                {
                    Debug.Log(name + "已存在，忽略");
                }

            }

        }

        private void CreateStandSlotFace(StandSlotFace standSlot, string[] fileDatas)
        {
            for (int j = 0; j < fileDatas.Length; j++)
            {
                int acceIndex = 0;
                var data = AssetDatabase.LoadAssetAtPath<Sprite>(fileDatas[j]);//筛选Add
                if (data != null)
                {
                    if (data.name.Contains("body"))
                    {
                        standSlot.casheBody.Add(data);
                    }
                    else if (data.name.Contains("face"))
                    {
                        standSlot.casheFace.Add(data);
                    }
                    else if (data.name.Contains("acce"))
                    {
                        //standSloat.casheAcce.Add(data);
                        standSlot.acce[acceIndex].sprite = data;
                        standSlot.acce[acceIndex].SetNativeSize();
                        acceIndex++;
                    }
                    if (data.name == "body_01")
                    {
                        standSlot.body.sprite = data;
                        standSlot.body.SetNativeSize();
                    } 
                    if (data.name == "0_face")
                    {
                        standSlot.face.sprite = data;
                        standSlot.face.SetNativeSize();
                    }
                }
            }
        }
    }
}

