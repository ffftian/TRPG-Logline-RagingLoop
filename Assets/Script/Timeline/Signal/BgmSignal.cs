using DG.Tweening;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BgmSignal : MonoBehaviour
{
    Tweener stopTweenHandle;
    public AudioSource audioSource;
    public CanvasGroup canvasGroup;
    public Text bgmText;

    private void Start()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
        }
    }

    public void BGMChange(string bgmName)
    {
        if(stopTweenHandle!=null)
        {
            stopTweenHandle.Kill();
        }

        AudioClip bgm = Resources.Load<AudioClip>($"BGM\\{bgmName}");
        if (canvasGroup != null)
        {
            RectTransform rectTransform = canvasGroup.transform as RectTransform;

            Vector2 endValue = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition -= new Vector2(100, 0);
            DOTween.To(() => rectTransform.anchoredPosition, x => rectTransform.anchoredPosition = x, endValue, 0.5f);
            canvasGroup.DOFade(1, 0.5f);
        }
        if (bgmText != null)
        {
            bgmText.text = bgmName;
        }
        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.volume = 0.15f;
        audioSource.Play();
        if (canvasGroup != null)
        {
            this.StartCoroutine(HideView());
        }
    }

    public void BGMStop()
    {
        stopTweenHandle = audioSource.DOFade(0, 1);
        stopTweenHandle.OnComplete(audioSource.Stop);
        //audioSource.Stop();
    }

    private IEnumerator HideView()
    {
        yield return new WaitForSeconds(5f);
        canvasGroup.DOFade(0, 0.5f);
    }

}

