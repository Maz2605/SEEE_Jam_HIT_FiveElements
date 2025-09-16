using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTower : MonoBehaviour
{
    public float healAmount;
    
    private void onEnable()
    {
        GameEventPhong.HealTower += HealthTower;
    }

    private void OnDisable()
    {
        GameEventPhong.HealTower -= HealthTower;
    }

    private float HealthTower()
    {
        return 0f;
    }
}
