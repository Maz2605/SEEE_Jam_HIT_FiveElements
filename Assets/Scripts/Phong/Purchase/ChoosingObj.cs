using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoosingObj : MonoBehaviour
{
    public int Cost
    {
        get => _cost;
        set => _cost = value;
    }

    [SerializeField] private Image image;
    [SerializeField] private List<Image> _imageList = new List<Image>();
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private int _level = 0;
    [SerializeField] private int _currentSkill;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private int _cost;

    private void Awake()
    {
        if (_currentSkill == 1)
        {
            _level = DataManager.Instance.CurrentLevelSkill1;
            _cost = DataManager.Instance.PriceSkill1;
        }
        if (_currentSkill == 2)
        {
            _level = DataManager.Instance.CurrentLevelSkill2;
            _cost = DataManager.Instance.PriceSkill2;
        }
        if (_currentSkill == 3)
        {
            _level = DataManager.Instance.CurrentLevelSkill3;
            _cost = DataManager.Instance.PriceSkill3;
        }
        
        UpdateLevelBar();
    }
    public TextMeshProUGUI DescriptionText
    {
        get => _descriptionText;
        set => _descriptionText = value;
    }

    public int CurrentSkill
    {
        get => _currentSkill;
        set => _currentSkill = value;
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

    public int Level
    {
        get => _level;
        set => _level = value;
    }

    private void UpdateLevelBar()
    {
        if(_level > 5) return;
            for (int i = 0; i < _level; i++)
            {
                _imageList[i].gameObject.SetActive(true);
            }
    }

    public void Press()
    {
        ListObjPart.Instance.SetCurrentSkillChoice(_currentSkill);
        GameEventPhong.PressObject();
    }

    public void PressUpgrade()
    {
        _level++;
        if (_currentSkill == 1 && _level <= 5)
        {
            GameEventPhong.UpgradeSkill1();
        }

        if (_currentSkill == 2 && _level <= 5)
        {
            GameEventPhong.UpgradeSkill2();
        }

        if (_currentSkill == 3 && _level <= 5)
        {
            GameEventPhong.UpgradeSkill3();
        }
        
        UpdateLevelBar();
    }
    
}
