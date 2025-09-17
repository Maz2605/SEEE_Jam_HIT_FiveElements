using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTower : Buff
{
    public float healAmount = 100f;
    
    private void OnEnable()
    {
        GameEventPhong.HealTower += HealthTower;
    }

    private void OnDisable()
    {
        GameEventPhong.HealTower -= HealthTower;
    }

    private void HealthTower()
    {
        PlayerController.Instance.SetCurrentHealthTower(healAmount);
    }
}
