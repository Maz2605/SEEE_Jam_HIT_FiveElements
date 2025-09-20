using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{

    public GameObject skillPart;
    public GameObject buffPart;
    public GameObject SkinPart;
    public TextMeshProUGUI _inforText;
    public TextMeshProUGUI text;

    [SerializeField] private int _coin;

    public int Coin
    {
        get => _coin;
        set => _coin = value;
    }

    private void Awake()
    {
        DataManager.Instance.ResetAll();
        OnPressSkill();
        _coin = DataManager.Instance.Coin;
        text.text = _coin.ToString();
    }

    public void UpdateCoinText(int coin)
    {
        _coin = coin;
        text.text = coin.ToString();
    }

    public void OnPressSkill()
    {
        skillPart.SetActive(true);
        buffPart.SetActive(false);
    }

    public void OnPressBuff()
    {
        skillPart.SetActive(false);
        buffPart.SetActive(true);
    }

    public void InforNotEnough()
    {
        _inforText.gameObject.SetActive(true);
        _inforText.color = new Color(_inforText.color.r, _inforText.color.g, _inforText.color.b, 0f); // reset alpha = 0

        Sequence seq = DOTween.Sequence();
        seq.Append(_inforText.DOFade(1f, 0.2f))   // fade in nhanh
            .AppendInterval(1f)                    // giữ 2 giây
            .Append(_inforText.DOFade(0f, 0.2f))   // fade out
            .OnComplete(() => _inforText.gameObject.SetActive(false)); // ẩn hẳn
    }

    public void OutShop()
    {
        // Out shop 
        
        gameObject.SetActive(false);
    }
}
