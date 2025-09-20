using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTower : Buff
{
    public float healAmount = 100f;
    private bool isUseBuff = false;
    
    private void OnEnable()
    {
        GameEventPhong.HealTower += HealthTower;
        GameEventPhong.LockBuffHealTower += LockBuff;
        GameEventPhong.UnLockBuffHealTower += UnLockBuff1;
    }

    private void OnDisable()
    {
        GameEventPhong.HealTower -= HealthTower;
        GameEventPhong.LockBuffHealTower -= LockBuff;
        GameEventPhong.UnLockBuffHealTower -= UnLockBuff1;
    }

    private void HealthTower()
    {
        if(IsUseBuff)
            TowerHealth.Instance.SetCurrentHealth(healAmount);
        IsUseBuff = false;
        LockBuff();
    }

    public void UnLockBuff1()
    {
        UnLockBuff();
    }
}
