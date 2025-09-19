using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffChoosing : MonoBehaviour
{
    

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private int _cost;
    [SerializeField] private int _currentBuff;

    private void Awake()
    {
        if (_currentBuff == 1)
        {
            _cost = DataManager.Instance.PriceBuffIncreaseMaxHealth;
        }
        else
        {
            _cost = DataManager.Instance.BuffIncreaseDuration;
        }
    }
    
    public Image Image
    {
        get => image;
        set => image = value;
    }

    public TextMeshProUGUI NameText
    {
        get => _nameText;
        set => _nameText = value;
    }

    public TextMeshProUGUI DescriptionText
    {
        get => _descriptionText;
        set => _descriptionText = value;
    }

    public int Cost
    {
        get => _cost;
        set => _cost = value;
    }

    public int CurrentBuff
    {
        get => _currentBuff;
        set => _currentBuff = value;
    }
    
    

}
