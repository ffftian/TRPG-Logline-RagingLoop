using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AutoImage : MonoBehaviour
{
    public Button AutoButton;
    public float time = 1;
    public Image image;
    public Sprite changeImageA;
    public Sprite changeImageB;

    private float timer;
    private void Start()
    {
        this.gameObject.SetActive(false);
        AutoButton.onClick.AddListener(() => this.gameObject.SetActive(true));
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            timer = 0;
            if (image.sprite == changeImageA)
            {
                image.sprite = changeImageB;
            }
            else if (image.sprite == changeImageB)
            {
                image.sprite = changeImageA;
            }
        }
    }
}

