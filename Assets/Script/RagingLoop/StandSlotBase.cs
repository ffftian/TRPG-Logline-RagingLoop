using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StandSlotBase : MonoBehaviour
{
    public int id;
    public List<Sprite> casheBody;

    public Image body;
    /// <summary>
    /// 配件，配件是按需加不限每次一个。
    /// </summary>
    public Image[] acce;
    /// <summary>
    /// 还存在一些，没有命名序号的图片，一并收入
    /// </summary>
    public Image[] other;
    public void ChangeBody(int index)
    {
        body.sprite = casheBody[index];
    }

    public void AddAcce(int index)
    {
        acce[index].gameObject.SetActive(true);
    }
    public void RemoveAcce(int index)
    {
        acce[index].gameObject.SetActive(false);
    }

    public void AddOther(int index)
    {
        other[index].gameObject.SetActive(true);
    }

    public void RemoveOther(int index)
    {
        other[index].gameObject.SetActive(false);
    }




    //半身像没准是特殊坐标，其他的都是通常的

    //virtual public void SaveBustAdv()
    //{
    //    var slot = this.transform.parent;
    //    int slotIndex = int.Parse(slot.name);
    //    RagingLoopSetting.SaveBustAdv(slotIndex, id, transform as RectTransform);
    //}

    //[Sirenix.OdinInspector.Button("尝试设置游戏中半身像坐标")]
    //public void SetHeadPos()
    //{
    //    ImgBustAdv imgBustAdv = RagingLoopSetting.GetBustAdv(9, id);
    //    RectTransform rectTransform = transform as RectTransform;
    //    rectTransform.anchoredPosition = imgBustAdv.xy;
    //}
}

