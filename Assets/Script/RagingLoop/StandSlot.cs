using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
namespace RagingLoop
{
    /// <summary>
    /// 立绘设置
    /// </summary>
    public class StandSlot : StandSlotBase
    {
        public List<Sprite> casheBrow;
        public List<Sprite> casheEye;
        public List<Sprite> casheMouth;
        //public List<Sprite> casheAcce;

        public Image brow;
        public Image eye;
        public Image mouth;

        /// <summary>
        /// 眨眼插件
        /// </summary>
        protected SequentialIntervalSprite shutEye;//眨眼要聚焦于本体，因为时间线只类似于开关
        public void Awake()
        {
            shutEye = new SequentialIntervalSprite(eye, SequentialIntervalSprite.BlinkEye());
        }

        public List<ImgBustAdv> GetSlotImgBustAdv()
        {
            var slot = transform.parent;
            int slotIndex = int.Parse(slot.name);
            List<ImgBustAdv> imgBustAdvList = RagingLoopSetting.GetBustAdv(slotIndex,id);
            return imgBustAdvList;
        }

        public void ChangeBrow(int browIndex)
        {
            brow.sprite = casheBrow[browIndex];
        }
        public void ChangeEye(int eyeIndex)
        {
            eye.sprite = casheEye[eyeIndex];
        }
        public void Update()
        {
            shutEye.Tick();
        }

        public void TickEye(int eyeIndex)
        {
            if(id == 2)
            {
                shutEye.RefreshSprites(casheEye[eyeIndex], casheEye[eyeIndex]);
            }
            else if(id == 10)
            {
                shutEye.RefreshSprites(casheEye[eyeIndex], casheEye[eyeIndex]);
            }
            else if (id == 5)
            {
                if (shutEye != null)
                {
                    shutEye.RefreshSprites(casheEye[eyeIndex], casheEye[3]);
                }
            }
            else
            if (shutEye != null)
            {
                if(eyeIndex==6 || eyeIndex == 2 || eyeIndex == 3 || eyeIndex == 13)//半睁眼动作不用刷新
                {
                    shutEye.RefreshSprites(casheEye[eyeIndex], casheEye[eyeIndex]);
                }
                else
                {
                    shutEye.RefreshSprites(casheEye[eyeIndex], casheEye[1]);
                }
            }
        }

        public void ChangeMouth(int mouthIndex)
        {
            mouth.sprite = casheMouth[mouthIndex];
        }


#if UNITY_EDITOR

        [HideInInspector]
        public int overallIndex;
        [HideInInspector]
        public int browIndex;
        [HideInInspector]
        public int eyeIndex;
        [HideInInspector]
        public int mouthIndex;
#endif
    }
}