using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 处理触发眨眼
/// </summary>
public class SequentialIntervalSprite
{
    public Image image;
    public Sprite[] sprites;
    public int[] TimingArray;

    public static int[] BlinkEye()
    {
        int[] TimingArray;
        System.Random r = new System.Random();
        TimingArray = new int[6] { 0, 4, 0, 4, 0, 4 };//4这块可能是变眼睛？
        //TimingArray[0] = 50 + r.Next(60);
        //TimingArray[2] = 50 + r.Next(60);
        //TimingArray[4] = 10 + r.Next(3);

        TimingArray[0] = 50 + 20 + r.Next(40);
        TimingArray[2] = 50 + 20 + r.Next(40);
        TimingArray[4] = 10 + r.Next(3);
        return TimingArray;
    }

    public SequentialIntervalSprite(Image eye, int[] timeArray)
    {
        this.image = eye;
        this.TimingArray = timeArray;
        //this.sprite = sprite;
    }
    public void RefreshSprites(params Sprite[] sprites)
    {
        if (this.sprites == null)
        {
            this.sprites = sprites;
            return;
        }
        for (int i = 0; i < sprites.Length; i++)
        {
            if (this.sprites[i] != sprites[i])
            {
                BlinkEye();
                delta = 0;
                image.sprite = sprites[0];
            }
        }
        this.sprites = sprites;
    }

    protected int CurrentFrame;
    protected float delta;
    public void Tick()
    {
        if (sprites != null)
        {
            delta += Time.deltaTime;
            if (delta < 1.0 / 30.0)
            {
                return;
            }
            delta = 0;

            CurrentFrame++;
            if (TimingArray == null || TimingArray.Length <= 0)
            {
                return;
            }
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            for (num3 = 0; num3 < TimingArray.Length; num3++)
            {
                num += TimingArray[num3];
            }
            for (num3 = 0; num3 < TimingArray.Length; num3++)
            {
                num2 += TimingArray[num3];
                if (CurrentFrame % num <= num2)
                {
                    break;
                }
            }
            image.sprite = sprites[num3 % sprites.Length];
        }
    }
}

