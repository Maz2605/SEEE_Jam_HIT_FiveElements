using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public GameObject skillPart;
    public GameObject buffPart;
    public GameObject SkinPart;
    
    public Image skillImage;
    public Image buffImage;
    public Image skinImage;

    [SerializeField] private float _IncreaseDurationPrice = 100f;
    [SerializeField] private float _IncreaseMaxHeath = 100f;
    private void OnEnable()
    {
        GameEventPhong.BuyBuffIncreaseDuration += BuyBuffIncreaseDuration;
        GameEventPhong.BuyBuffIncreaseMaxHeath += BuyBuffIncreaseMaxHeath;
    }

    private void OnDisable()
    {
        GameEventPhong.BuyBuffIncreaseDuration -= BuyBuffIncreaseDuration;
        GameEventPhong.BuyBuffIncreaseMaxHeath -= BuyBuffIncreaseMaxHeath;
    }

    private void BuyBuffIncreaseDuration()
    {
        // Neu coin du
        
    }

    private void BuyBuffIncreaseMaxHeath()
    {
        // Neu coin du
        GameEventPhong.BuyBuffIncreaseMaxHeath();
    }

    private void BuySkin()
    {
        
    }
    
    
}
