using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    
    private void OnEnable()
    {
        GameEventPhong.HandleIncreaseMaxHeath += IncreaseMaxHeath;
        GameEventPhong.HandleIncreaseDuration += IncreaseDuration;
        InputManager.OnPress1 += HeathTower;
        InputManager.OnPress2 += IncreasePowerSpeed;
        InputManager.OnPress3 += SpawnHero;
    }

    private void OnDisable()
    {
        GameEventPhong.HandleIncreaseMaxHeath -= IncreaseMaxHeath;
        GameEventPhong.HandleIncreaseDuration -= IncreaseDuration;
        InputManager.OnPress1 -= HeathTower;
        InputManager.OnPress2 -= IncreasePowerSpeed;
        InputManager.OnPress3 -= SpawnHero;
    }

    public void HeathTower()
    {
        GameEventPhong.HealTower();
    }

    public void IncreasePowerSpeed()
    {
        GameEventPhong.IncreasePowerSpeed();
    }



    public void IncreaseMaxHeath()
    {
        GameEventPhong.IncreaseMaxHeath();
    }

    public void IncreaseDuration()
    {
        GameEventPhong.IncreaseDuration();
    }

    private void SpawnHero()
    {
        GameEventPhong.SpawnHero();
    }
    
}
