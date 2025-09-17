using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Skill3 : MonoBehaviour
{
    public UISkill uiSkill;
    public float timeCoolDown = 10f;
    public Image lookImage;
    public GameObject skill3Prefab;
    
    private bool _isCooldown = false;
    [SerializeField] private bool _isLook = true;
    [SerializeField] private float _buffSpeedAttack;
    [SerializeField] private float _upgradeBuffSpeedAttack = 0.2f;
    [SerializeField] private float _buffRangeAttack;
    [SerializeField] private float _upgradeBuffRangeAttack = 0.2f;

    private void Awake()
    {
        _buffRangeAttack = DataManager.Instance.BuffRangeAttack;
        _buffSpeedAttack = DataManager.Instance.BuffSpeedAttack;
    }

    public void SetLook(bool isLook)
    {
        _isLook = isLook;
    }
    private void OnEnable()
    {
        GameEventPhong.UpgradeSkill3 += UpgradeSkill;
        InputManager.OnPressR += UseSkill3;
    }

    private void OnDisable()
    {
        GameEventPhong.UpgradeSkill3 -= UpgradeSkill;
        InputManager.OnPressR -= UseSkill3;

    }

    private void UpgradeSkill()
    {
        _buffRangeAttack += _upgradeBuffRangeAttack;
        _buffSpeedAttack += _upgradeBuffSpeedAttack;
        
        DataManager.Instance.SaveBuffRangeAttack(_buffRangeAttack);
        DataManager.Instance.SaveBuffSpeedAttack(_buffSpeedAttack);
    }

    private void UseSkill3()
    {
        if(_isCooldown || _isLook) return;
        
        Instantiate(skill3Prefab, PlayerController.Instance.transform.position, Quaternion.identity);
        // Lam gi do voi player
        BuffDamageRange();
        BuffAttackSpeed();
        // Bắt đầu cooldown
        StartCooldown();
        
    }
    
    // ================= COOL DOWN =================
    private void StartCooldown()
    {
        _isCooldown = true;
        Invoke(nameof(ResetCooldown), timeCoolDown);
        
        // Gọi UI cooldown
        if (uiSkill != null)
            uiSkill.StartLoading(timeCoolDown, ResetCooldown);

    }

    private void ResetCooldown()
    {
        _isCooldown = false;
    }

    private void BuffDamageRange()
    {
        PlayerController.Instance.SetAttackRange(_buffRangeAttack);
        
        DOVirtual.DelayedCall(5f, () =>
        {
            PlayerController.Instance.SetAttackRange(-_buffRangeAttack);
        });
    }

    private void BuffAttackSpeed()
    {
        PlayerController.Instance.SetBulletSpeed(_buffSpeedAttack);
        
        DOVirtual.DelayedCall(5f, () =>
        {
            PlayerController.Instance.SetBulletSpeed(-_buffSpeedAttack);
        });
    }
}
