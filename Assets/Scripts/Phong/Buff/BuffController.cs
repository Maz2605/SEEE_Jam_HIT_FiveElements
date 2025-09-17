using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    List<Buff> buffInGames = new List<Buff>();
    
    List<Buff> buffOutGames = new List<Buff>();

    private void OnEnable()
    {
        InputManager.OnPress1 += HeathTower;
        InputManager.OnPress2 += IncreasePowerSpeed;
        InputManager.OnPress3 += IncreaseSpeedAttack;
    }

    private void OnDisable()
    {
        InputManager.OnPress1 -= HeathTower;
        InputManager.OnPress2 -= IncreasePowerSpeed;
        InputManager.OnPress3 -= IncreaseSpeedAttack;
    }

    public void HeathTower()
    {
        GameEventPhong.HealTower();
    }

    public void IncreasePowerSpeed()
    {
        GameEventPhong.IncreasePowerSpeed();
    }

    public void IncreaseSpeedAttack()
    {
        GameEventPhong.IncreaseSpeedAttack();
    }
    
    
    
    
}
