using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class increaseMaxHeath : Buff
{
    public float heathIncrease;

    private void OnEnable()
    {
        GameEventPhong.IncreaseMaxHeath += IncreaseMaxHeath;
    }


    private void OnDisable()
    {
        GameEventPhong.IncreaseMaxHeath -= IncreaseMaxHeath;
    }

    private void IncreaseMaxHeath()
    {
        TowerHealth.Instance.SetMaxHealth(heathIncrease);
    }
}
