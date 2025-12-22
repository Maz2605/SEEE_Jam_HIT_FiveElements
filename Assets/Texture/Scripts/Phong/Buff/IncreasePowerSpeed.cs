using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePowerSpeed : Buff
{
    public float powerSpeed = 1f;

    private void OnEnable()
    {
        GameEventPhong.IncreasePowerSpeed += IncreasePowerSpeedBuff;

    }

    private void OnDisable()
    {
        GameEventPhong.IncreasePowerSpeed -= IncreasePowerSpeedBuff;

    }

    private void IncreasePowerSpeedBuff()
    {
        
            EmotionBar.Instance.SetPassiveGainPerSecond(powerSpeed);
        
        
    }
}
