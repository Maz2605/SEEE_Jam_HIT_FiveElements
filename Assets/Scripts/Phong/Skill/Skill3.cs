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
    [SerializeField] private float _buffSpeedAttack = 10f;
    [SerializeField] private float _timer = 5f;

    [SerializeField] private float _buffRangeAttack = 10f;
 

    private void Awake()
    {
        _buffRangeAttack = DataManager.Instance.BuffRangeAttack;
        _buffSpeedAttack = DataManager.Instance.BuffSpeedAttack;
        _timer = DataManager.Instance.TimerSkill3;
    }

    public void SetLook(bool isLook)
    {
        _isLook = isLook;
    }
    private void OnEnable()
    {
        InputManager.OnPressR += UseSkill3;
    }

    private void OnDisable()
    {
        InputManager.OnPressR -= UseSkill3;

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
        
        DOVirtual.DelayedCall(_timer, () =>
        {
            PlayerController.Instance.SetAttackRange(-_buffRangeAttack);
        });
    }

    private void BuffAttackSpeed()
    {
        PlayerController.Instance.SetBulletSpeed(_buffSpeedAttack);
        
        DOVirtual.DelayedCall(_timer, () =>
        {
            PlayerController.Instance.SetBulletSpeed(-_buffSpeedAttack);
        });
    }
}
