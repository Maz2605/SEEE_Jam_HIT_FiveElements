using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListObjPart : Singleton<ListObjPart>
{
    [SerializeField] private Image _currentImage;
    [SerializeField] private TextMeshProUGUI _currentName;
    [SerializeField] private TextMeshProUGUI _currentDescription;
    [SerializeField] private TextMeshProUGUI _currentCostText;
    [SerializeField] private int _currentlevel;
    [SerializeField] private int _currentSkillChoice;
    [SerializeField] private int _currentCost;

    [SerializeField] private List<ChoosingObj> _choosingObjs = new List<ChoosingObj>();

    private void Start()
    {
        DataManager.Instance.ResetAll();
        ChoosingObj choosingObj = _choosingObjs[0];
        _currentImage.sprite = choosingObj.Image.sprite;
        RectTransform rt = _currentImage.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.sizeDelta = new Vector2(200, 200);
        }
        _currentlevel = choosingObj.Level;
        _currentName.text = choosingObj.NameText.text;
        _currentDescription.text = choosingObj.DescriptionText.text;
        _currentCost = choosingObj.Cost;
        _currentCostText.text = _currentCost.ToString();
    }
    public void SetCurrentSkillChoice(int choice)
    {
        _currentSkillChoice = choice;
    }
    private void OnEnable()
    {
        GameEventPhong.PressObject += OnPressObj;
    }

    private void OnDisable()
    {
        GameEventPhong.PressObject -= OnPressObj;
    }

    public void OnPressObj()
    {
        ChoosingObj choosingObj = _choosingObjs[_currentSkillChoice-1];
        _currentImage.sprite = choosingObj.Image.sprite;
        RectTransform rt = _currentImage.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.sizeDelta = new Vector2(200, 200);
        }
        _currentlevel = choosingObj.Level;
        _currentName.text = choosingObj.NameText.text;
        _currentDescription.text = choosingObj.DescriptionText.text;
        _currentCost = choosingObj.Cost;
        _currentCostText.text = _currentCost.ToString();
        
    }
    
    public void PressUpgrade()
    {
        ChoosingObj choosingObj = _choosingObjs[_currentSkillChoice-1];
        choosingObj.PressUpgrade();
        
    }
}
