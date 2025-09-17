using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerSkill : MonoBehaviour
{

    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _upgradeDamage = 1f;

    private void Awake()
    {
       _damage =  DataManager.Instance.DamageSkill1;
    }

    private void OnEnable()
    {
        GameEventPhong.UpgradeSkill1 += UpgradeSkill;
    }

    private void OnDisable()
    {
        GameEventPhong.UpgradeSkill1 -= UpgradeSkill;
    }

    private void UpgradeSkill()
    {
        _damage += _upgradeDamage;
        DataManager.Instance.SaveDataSkill1(_damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("-50 mau ");
            // Enemy take damage
            XuanEventManager.EnemyTakeDamage(other.gameObject.GetComponent<Enemy>(),_damage);
        }
    }

}
