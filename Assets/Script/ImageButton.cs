using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageButton : Button
{
    public Sprite Normal;
    public Sprite MouseIn;
    public Sprite MouseDown;
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            image.sprite = MouseIn;
        }
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            image.sprite = MouseDown;
        }
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            image.sprite = MouseIn;
        }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            image.sprite = Normal;
        }
    }

}

