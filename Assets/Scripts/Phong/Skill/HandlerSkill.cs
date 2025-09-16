using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerSkill : MonoBehaviour
{

    [SerializeField] private float _damage = 1f;
    


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("-50 mau ");
            // Enemy take damage
            XuanEventManager.EnemyTakeDamage(other.gameObject.GetComponent<Enemy>(),_damage);
        }
    }

    private void GetDamage()
    {
        
    }
}
