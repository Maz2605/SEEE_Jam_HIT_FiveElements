using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AwardChoosing : MonoBehaviour
{
    [SerializeField] private int _buffChoice;

    public bool _isChoosing = false;

    public void OnPressed()
    {
        if (!_isChoosing)
        {
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
        }
       
        DOVirtual.DelayedCall(5f, () => { _isChoosing = false; });

    }

    public void NextWave()
    {
        // Next wave
        GameEventPhong.DisAppearAward();
        GameManager.Instance.ContinueNextWave();
        DOVirtual.DelayedCall(2f, () => SpawnBuff());
    }

    public void SpawnBuff()
    {
        if (_buffChoice == 1)
        {
            GameEventPhong.HealTower();
        }

        if (_buffChoice == 2)
        {
            GameEventPhong.IncreasePowerSpeed();
        }

        if (_buffChoice == 3)
        {
            GameEventPhong.SpawnHero();
        }
    }
}
