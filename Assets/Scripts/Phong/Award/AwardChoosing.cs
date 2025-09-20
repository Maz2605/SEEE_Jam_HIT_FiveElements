using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AwardChoosing : MonoBehaviour
{
    [SerializeField] private int _buffChoice;

    public void OnPressed()
    {
        if (_buffChoice == 1)
        {
            GameEventPhong.UnLockBuffHealTower();
        }

        if (_buffChoice == 2)
        {
            GameEventPhong.UnLockBuffIncreasePowerSpeed();
        }

        if (_buffChoice == 3)
        {
            GameEventPhong.UnLockSapwnHero();
        }
        Sequence seq = DOTween.Sequence();
        GameObject part = this.gameObject;
        CanvasGroup cg = part.GetComponent<CanvasGroup>();
        if (cg == null) cg = part.AddComponent<CanvasGroup>();

        seq.Append(part.transform.DOScale(0f, 0.4f).SetEase(Ease.InBack));
        seq.Join(cg.DOFade(0f, 0.4f));
        seq.AppendInterval(0.1f);

        seq.AppendCallback(() => part.SetActive(false));
        NextWave();
    }

    public void NextWave()
    {
        // Next wave
        GameEventPhong.DisAppearAward();

        var gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.ContinueNextWave();
        }
    }
}
