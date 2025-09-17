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
    
    private bool _isCooldown = false;
    [SerializeField] private bool _isLook = true;
    [SerializeField] private float _buffSpeedAttack;
    [SerializeField] private float _buffRangeAttack;
    
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
