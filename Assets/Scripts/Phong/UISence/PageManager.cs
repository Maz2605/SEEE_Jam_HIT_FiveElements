using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class PageManager : MonoBehaviour
{
    public Image imageInfor;
    public Image textInfor;

    public void ShowPage(Action onComplete = null, bool autoHide = true)
    {
        gameObject.SetActive(true);

        // Reset alpha
        imageInfor.color = new Color(imageInfor.color.r, imageInfor.color.g, imageInfor.color.b, 0);
        textInfor.color = new Color(textInfor.color.r, textInfor.color.g, textInfor.color.b, 0);

        Sequence seq = DOTween.Sequence();
        seq.Append(imageInfor.DOFade(1f, 1f));
        seq.Join(textInfor.DOFade(1f, 1f));

        if (autoHide)
        {
            seq.AppendInterval(3f);
            seq.Append(imageInfor.DOFade(0f, 1f));
            seq.Join(textInfor.DOFade(0f, 1f));

            seq.OnComplete(() =>
            {
                gameObject.SetActive(false);
                onComplete?.Invoke();
            });
        }
        else
        {
            seq.OnComplete(() => { onComplete?.Invoke(); });
        }
    }

    // Hàm Fade Out thủ công (dùng cho trang cuối khi click)
    public void HidePage(Action onComplete = null)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(imageInfor.DOFade(0f, 1f));
        seq.Join(textInfor.DOFade(0f, 1f));

        seq.OnComplete(() =>
        {
            gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }
}