using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3 : MonoBehaviour
{

    public float timeCoolDown = 10f;
    
    private bool _isCooldown = false;
    [SerializeField] private float _buffSpeedAttack;
    [SerializeField] private float _buffRangeAttack;
    private void OnEnable()
    {
        InputManager.OnPressR += UseSkill3;
        GameEventPhong.PlayerTakeDamageRange += GetBuffDamageRange;
        GameEventPhong.PlayerTakeAttackSpeed += GetBuffAttackSpeed;
    }

    private void OnDisable()
    {
        InputManager.OnPressR -= UseSkill3;
        GameEventPhong.PlayerTakeDamageRange -= GetBuffDamageRange;
        GameEventPhong.PlayerTakeAttackSpeed -= GetBuffAttackSpeed;
    }

    private void UseSkill3()
    {
        if(_isCooldown) return;
        // Lam gi do voi player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.AddComponent<PlayerController>();
        // Bắt đầu cooldown
        StartCooldown();
        
    }
    
    // ================= COOL DOWN =================
    private void StartCooldown()
    {
        _isCooldown = true;
        Invoke(nameof(ResetCooldown), timeCoolDown);
    }

    private void ResetCooldown()
    {
        _isCooldown = false;
    }

    private float GetBuffDamageRange()
    {
        return PlayerController.Instance.AttackRange + _buffRangeAttack;
    }

    private float GetBuffAttackSpeed()
    {
        return PlayerController.Instance.BulletSpeed + _buffSpeedAttack;
    }
}
