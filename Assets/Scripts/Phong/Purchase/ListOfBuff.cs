using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListOfBuff : Singleton<ListOfBuff>
{
    [SerializeField] private Image _currentImage;
    [SerializeField] private TextMeshProUGUI _currentName;
    [SerializeField] private TextMeshProUGUI _currentDescription;
    [SerializeField] private TextMeshProUGUI _inforText;
    [SerializeField] private int _currentBuffChoice;
    [SerializeField] private int _currentCost;

    private float _heathDistance = 0f;
    private float _durationDistance = 0f;

    public int CurrentBuffChoice
    {
        get => _currentBuffChoice;
        set => _currentBuffChoice = value;
    }

    [SerializeField] private List<BuffChoosing> _choosingObjs = new List<BuffChoosing>();

    private void Start()
    {
        BuffChoosing buffChoosing = _choosingObjs[0];
        _currentImage.sprite = buffChoosing.Image.sprite;
        _currentName.text = buffChoosing.NameText.text;
        _currentDescription.text = buffChoosing.DescriptionText.text;
        _currentCost = buffChoosing.Cost;
        _currentBuffChoice = 1;
    }

    public void OnPressed()
    {
        BuffChoosing buffChoosing = _choosingObjs[_currentBuffChoice-1];
        _currentImage.sprite = buffChoosing.Image.sprite;
        _currentName.text = buffChoosing.NameText.text;
        _currentDescription.text = buffChoosing.DescriptionText.text;
        _currentCost = buffChoosing.Cost;
    }

    public void PressBuy()
    {
        // Xu ly coin
        int coin = ShopManager.Instance.Coin;

        if (coin < _currentCost || coin <= 0)
        {
            ShopManager.Instance.InforNotEnough();
            return;
        }
        
        if (_currentBuffChoice == 1 && coin >= _currentCost)
        {
            _inforText.gameObject.SetActive(true);
            _inforText.text = " ";
            float _currentHeath = DataManager.Instance.TowerHealth;
            GameEventPhong.IncreaseMaxHeath();
            float _newHeath = DataManager.Instance.TowerHealth;
            _heathDistance = _newHeath - _currentHeath;
            UpdateBuff( DataManager.Instance.TowerHealth, _heathDistance , "Máu tối đa thành");
            ShowInforText();
            DataManager.Instance.PriceBuffIncreaseMaxHealth += 100;
            DataManager.Instance.SavePriceBuffIncreaseMaxHealth(DataManager.Instance.PriceBuffIncreaseMaxHealth);
            _choosingObjs[_currentBuffChoice-1].CostText.text = DataManager.Instance.PriceBuffIncreaseMaxHealth.ToString();
            DataManager.Instance.Coin -= _currentCost;
            DataManager.Instance.SaveCoin(DataManager.Instance.Coin);
            ShopManager.Instance.UpdateCoinText(DataManager.Instance.Coin);
        }

        if (_currentBuffChoice == 2 && coin >= _currentCost)
        {
            _inforText.gameObject.SetActive(true);
            _inforText.text = " ";
            float _currentDura = DataManager.Instance.PowerDuration;
            GameEventPhong.IncreaseDuration();
            float _newDura = DataManager.Instance.PowerDuration;
            _durationDistance = _newDura - _currentDura;
            UpdateBuff( DataManager.Instance.PowerDuration, _durationDistance , "Thời gian trong trạng thái cực vui");
            ShowInforText();
            DataManager.Instance.PriceBuffIncreaseDuration += 100;
            DataManager.Instance.SavePriceBuffIncreaseDuration(DataManager.Instance.PriceBuffIncreaseDuration);
            _choosingObjs[_currentBuffChoice-1].CostText.text = DataManager.Instance.PriceBuffIncreaseDuration.ToString();
            DataManager.Instance.Coin -= _currentCost;
            DataManager.Instance.SaveCoin(DataManager.Instance.Coin);
            ShopManager.Instance.UpdateCoinText(DataManager.Instance.Coin);
        }
        
        
    }

    private void UpdateBuff(float now, float distance, string buffName)
    {
        if (distance > 0)
        {
            _inforText.text = buffName + " : " + (now - distance).ToString() + " -> " + now.ToString();
        }
    }

    private void ShowInforText()
    {
        _inforText.gameObject.SetActive(true);
        _inforText.color = new Color(_inforText.color.r, _inforText.color.g, _inforText.color.b, 0f); // reset alpha = 0

        Sequence seq = DOTween.Sequence();
        seq.Append(_inforText.DOFade(1f, 0.2f))   // fade in nhanh
            .AppendInterval(1f)                    // giữ 2 giây
            .Append(_inforText.DOFade(0f, 0.2f))   // fade out
            .OnComplete(() => _inforText.gameObject.SetActive(false)); // ẩn hẳn
    }

   
    
}
