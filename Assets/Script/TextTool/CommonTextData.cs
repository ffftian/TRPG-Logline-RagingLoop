using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[Serializable]
public class CommonTextData : BaseTextData
{
    /// <summary>
    /// 是否保存由数组产生的序号
    /// </summary>
    public bool saveArrayIndex { get; set; } = true;
    public override string GroupID => groupID;
    public string groupID;
    //public override string GroupID => name;

    public override void Analysis(string paragraphText, int Serial, Action<Exception, string> error)
    {
        try
        {
            string[] singleConversation = Regex.Split(paragraphText, "\\r\\n");
            for (int i = 0; i < singleConversation.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        string[] value = Regex.Split(singleConversation[i], "\\(|\\)", RegexOptions.IgnoreCase);
                        name = value[0];
                        groupID = value[1];
                        if(saveArrayIndex)
                        {
                            textId = $"{singleConversation[i]}({Serial})";
                        }
                        else
                        {
                            textId = singleConversation[i];
                        }
                        break;
                    default:
                        log += singleConversation[i] + "\n";//剩下的为log
                        break;
                }
            }
        }
        catch (Exception e)
        {
            error.Invoke(e, $"错误的读取，序号{Serial}，输出原句:{paragraphText}");
        }

    }
}

