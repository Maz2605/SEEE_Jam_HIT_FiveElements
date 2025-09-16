using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandlerUltimate : MonoBehaviour
{
    
    [SerializeField] private float _damage = 100f;
    
    private void Start()
    {
        ShakeCamera.Instance.Shake();
        DOVirtual.DelayedCall(0.6f, (() => Destroy(gameObject)));
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
