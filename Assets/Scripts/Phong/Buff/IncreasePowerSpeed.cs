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
        GameEventPhong.LockBuffIncreasePowerSpeed += LockBuff;
        GameEventPhong.UnLockBuffIncreasePowerSpeed += UnLockBuff;
    }

    private void OnDisable()
    {
        GameEventPhong.IncreasePowerSpeed -= IncreasePowerSpeedBuff;
        GameEventPhong.LockBuffIncreasePowerSpeed -= LockBuff;
        GameEventPhong.UnLockBuffIncreasePowerSpeed -= UnLockBuff;
    }

    private void IncreasePowerSpeedBuff()
    {
        if(IsUseBuff)
            EmotionBar.Instance.SetPassiveGainPerSecond(powerSpeed);
        IsUseBuff = false;
        LockBuff();
    }
}
