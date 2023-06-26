using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace RagingLoop
{

    public class StandSlotPointGroup : MonoBehaviour
    {
        public string GetIndexName(int index)
        {
            if (standPoints[index].name=="7")
            {
                return standPoints[index].name + "特写";
            }
            else if (standPoints[index].name == "9")
            {
                return standPoints[index].name + "头像";
            }
            else
            {
                return standPoints[index].name;
            }    
        }

        /// <summary>
        /// 可绑定立绘的位置
        /// </summary>
        public RectTransform[] standPoints;
        /// <summary>
        /// 可绑定的立绘
        /// </summary>
        public RectTransform[] standSlotGroup;

        /// <summary>
        /// 将拥有指定索引Slot释放
        /// </summary>
        /// <param name="standSlotIndex"></param>
        public void FreeSlot(int standSlotIndex)
        {
            RectTransform standSlot = standSlotGroup[standSlotIndex];
            standSlot.SetParent(this.transform);
            standSlot.gameObject.SetActive(false);
        }
         public List<ImgBustAdv> GetSlotImgBustAdv(int standIndex, int standPointIndex)
        {
            RectTransform standPoint = standPoints[standPointIndex];
            string slot = standPoint.name;

            List<ImgBustAdv> imgBustAdvList = RagingLoopSetting.GetBustAdv(int.Parse(slot), standIndex + 1);
            return imgBustAdvList;
        }
        public void BindSlot(int standIndex, int standPointIndex,int index)
        {
            RectTransform standSlot = standSlotGroup[standIndex];
            RectTransform standPoint = standPoints[standPointIndex];

            string slot = standPoint.name;

            standSlot.SetParent(standPoint, false);
            standSlot.gameObject.SetActive(true);
            SetPos(standSlot, int.Parse(slot), standIndex +1,index);
            //standSlot.anchoredPosition = Vector3.zero;
            //standSlot.localScale = Vector3.one;

        }

        public void SetPos(RectTransform standSlotRect,int point,int id,int index)
        {
            //StandSlotBase standSlotBase = standSlotRect.GetComponent<StandSlotBase>();
            //float width = standSlotBase.body.mainTexture.width;
            //float height = standSlotBase.body.mainTexture.height;
            List<ImgBustAdv> imgBustAdvList = RagingLoopSetting.GetBustAdv(point, id);
            ImgBustAdv imgBustAdv = imgBustAdvList[index];

            if (point == 9)
            {
                standSlotRect.anchoredPosition = imgBustAdv.xy;
                if (imgBustAdv.anchorMax != Vector2.zero)
                {
                    standSlotRect.anchorMax = imgBustAdv.anchorMax;
                }
                if (imgBustAdv.anchorMin != Vector2.zero)
                {
                    standSlotRect.anchorMin = imgBustAdv.anchorMin;
                }
                if (imgBustAdv.pivot != Vector2.zero)
                {
                    standSlotRect.pivot = imgBustAdv.pivot;
                }
            }
            else
            {
                standSlotRect.anchorMin = new Vector2(0.5f, 0);
                standSlotRect.anchorMax = new Vector2(0.5f, 0);
                standSlotRect.pivot = new Vector2(0.5f, 0);
                standSlotRect.anchoredPosition = new Vector2(imgBustAdv.xy.x,0);
            }

            standSlotRect.localScale = new Vector3(imgBustAdv.s / 100, imgBustAdv.s/100, 1);
        }


        /// <summary>
        /// 已占据的点
        /// </summary>
        [HideInInspector]
        public int[] occupyStandSlot;

        public int[] slotIndexs;
    }

}
