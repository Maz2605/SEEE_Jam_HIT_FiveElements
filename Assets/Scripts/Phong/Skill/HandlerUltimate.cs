using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandlerUltimate : MonoBehaviour
{
    
    [SerializeField] private float _damage;
    
    private void Start()
    {
        ShakeCamera.Instance.Shake();
        DOVirtual.DelayedCall(5f, (() => Destroy(gameObject)));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("-100 mau");
            // Enemy take damage
        }
    }

    private void GetDamage()
    {
        
    }
}
