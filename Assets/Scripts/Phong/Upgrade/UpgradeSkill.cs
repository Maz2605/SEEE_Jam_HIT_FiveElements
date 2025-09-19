using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSkill : MonoBehaviour
{
    [SerializeField] private float _upgradeSkill1 = 10f;
    [SerializeField] private float _timer = 1f;
    [SerializeField] private float _buffSkill3 = 0.5f;

    
    private void OnEnable()
    {
        GameEventPhong.UpgradeSkill1 += UpgradeSkill1;
        GameEventPhong.UpgradeSkill2 += UpgradeSkill2;
        GameEventPhong.UpgradeSkill3 += UpgradeSkill3;
    }

    private void OnDisable()
    {
        GameEventPhong.UpgradeSkill1 -= UpgradeSkill1;
        GameEventPhong.UpgradeSkill2 -= UpgradeSkill2;
        GameEventPhong.UpgradeSkill3 -= UpgradeSkill3;
    }

    private void UpgradeSkill3()
    {
        DataManager.Instance.TimerSkill3 += _buffSkill3;
        DataManager.Instance.SaveTimerSkill2(DataManager.Instance.TimerSkill3);
        DataManager.Instance.CurrentLevelSkill3 += 1;
        DataManager.Instance.SaveCurrentLevelSkill3(DataManager.Instance.CurrentLevelSkill3 );
    }

    private void UpgradeSkill2()
    {
        DataManager.Instance.TimerSkill2 += _timer;
        DataManager.Instance.SaveTimerSkill2(DataManager.Instance.TimerSkill2 + _timer);
        DataManager.Instance.CurrentLevelSkill2 += 1;
        DataManager.Instance.SaveCurrentLevelSkill2(DataManager.Instance.CurrentLevelSkill2);
    }
    
    private void UpgradeSkill1()
    {
        DataManager.Instance.DamageSkill1 += _upgradeSkill1;
        DataManager.Instance.SaveDataSkill1(DataManager.Instance.DamageSkill1);
        DataManager.Instance.CurrentLevelSkill1 += 1;
        DataManager.Instance.SaveCurrentLevelSkill1(DataManager.Instance.CurrentLevelSkill1);
    }
    
}
