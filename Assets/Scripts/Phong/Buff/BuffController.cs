using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public int numberOfHealTower = 0;
    public int numberOfIncreasePowerSpeed = 0;
    public int numberOfSpeedAttack = 0;
    

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
        if (numberOfHealTower > 0)
        {
            GameEventPhong.HealTower();
            DataManager.Instance.SaveBuffHealTower(numberOfHealTower-1);
            numberOfHealTower--;
        }
    }

    public void IncreasePowerSpeed()
    {
        if (numberOfIncreasePowerSpeed > 0)
        {
            GameEventPhong.IncreasePowerSpeed();
            DataManager.Instance.SaveBuffIncreasePowerSpeed(numberOfIncreasePowerSpeed-1);
            numberOfIncreasePowerSpeed--;
        }
    }

    public void IncreaseSpeedAttack()
    {
        if (numberOfSpeedAttack > 0)
        {
            GameEventPhong.IncreaseSpeedAttack();
            DataManager.Instance.SaveBuffSpeedAttack(numberOfSpeedAttack-1);
            numberOfSpeedAttack--;
        }
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
