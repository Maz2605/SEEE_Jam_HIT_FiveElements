using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandlerUltimate : MonoBehaviour
{
    
    [SerializeField] private float _damage = 100f;
    [SerializeField] private float _upgradeDamage = 10f;
    
    private void Awake()
    {
        _damage = DataManager.Instance.DamageSkillUltimate;
    }

    private void OnEnable()
    {
        GameEventPhong.UpgradeSkillUltimate += UpgradeSkill;
    }

    private void OnDisable()
    {
        GameEventPhong.UpgradeSkillUltimate -= UpgradeSkill;
    }

    private void UpgradeSkill()
    {
        _damage += _upgradeDamage;
        DataManager.Instance.SaveDataSkillUltimate(_damage);
    }

    private void Start()
    {
        ShakeCamera.Instance.Shake();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("-100 mau");
            // Enemy take damage
            XuanEventManager.EnemyTakeDamage(other.gameObject.GetComponent<Enemy>(),_damage);
        }
    }

    private void GetDamage()
    {
        
    }
}
