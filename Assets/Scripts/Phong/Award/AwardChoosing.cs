using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AwardChoosing : MonoBehaviour
{
    [SerializeField] private int _buffChoice;

    [Header("UI Buff Image")]
    [SerializeField] private Image buffImage; // gán 1 Image trong Canvas, ban đầu SetActive(false)
    [SerializeField] private Sprite healSprite;
    [SerializeField] private Sprite powerSprite;
    [SerializeField] private Sprite heroSprite;

    [SerializeField] private float _showSeconds = 2f;

    public bool _isChoosing = false;

    public void OnPressed()
    {
        if (_isChoosing) return;

        _isChoosing = true;
        Sequence seq = DOTween.Sequence();
        GameObject part = this.gameObject;
        CanvasGroup cg = part.GetComponent<CanvasGroup>();
        if (cg == null) cg = part.AddComponent<CanvasGroup>();

        seq.Append(part.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack));
        seq.Join(cg.DOFade(0f, 0.2f));
        seq.AppendInterval(0.1f);
        seq.AppendCallback(() => part.SetActive(false));

        NextWave();

        DOVirtual.DelayedCall(5f, () => { _isChoosing = false; });
    }

    public void NextWave()
    {
        GameEventPhong.DisAppearAward();
        GameManager.Instance.ContinueNextWave();
        DOVirtual.DelayedCall(5f, () => SpawnBuff());
    }

    public void SpawnBuff()
    {
        if (buffImage == null) return;

        buffImage.gameObject.SetActive(true); // bật Image

        // đổi sprite tuỳ theo buff
        if (_buffChoice == 1)
        {
            GameEventPhong.HealTower();
            buffImage.sprite = healSprite;
        }
        else if (_buffChoice == 2)
        {
            GameEventPhong.IncreasePowerSpeed();
            buffImage.sprite = powerSprite;
        }
        else if (_buffChoice == 3)
        {
            GameEventPhong.SpawnHero();
            buffImage.sprite = heroSprite;
        }

        // thêm CanvasGroup để fade
        CanvasGroup cg = buffImage.GetComponent<CanvasGroup>();
        if (cg == null) cg = buffImage.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0f;

        RectTransform rt = buffImage.rectTransform;
        rt.localScale = Vector3.one * 0.5f; // bắt đầu nhỏ

        // hiệu ứng: fade in + scale to normal
        Sequence seq = DOTween.Sequence();
        seq.Append(cg.DOFade(1f, 0.3f));
        seq.Join(rt.DOScale(1f, 0.4f).SetEase(Ease.OutBack));

        // nhún lên nhún xuống trong lúc hiển thị
        seq.Append(rt.DOScale(1.1f, 0.25f).SetEase(Ease.OutQuad));
        seq.Append(rt.DOScale(0.9f, 0.25f).SetEase(Ease.InOutQuad));
        seq.Append(rt.DOScale(1f, 0.25f).SetEase(Ease.OutQuad));

        // giữ nguyên 1 lúc trước khi biến mất
        seq.AppendInterval(_showSeconds);

        // hiệu ứng biến mất
        seq.Append(cg.DOFade(0f, 0.3f));
        seq.Join(rt.DOScale(0.7f, 0.3f).SetEase(Ease.InBack));

        seq.OnComplete(() =>
        {
            buffImage.gameObject.SetActive(false);
        });
    }

}
