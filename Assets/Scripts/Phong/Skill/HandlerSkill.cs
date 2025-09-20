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

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("-50 mau ");
            // Enemy take damage
            other.GetComponent<Enemy>().TakeDamage(other.gameObject.GetComponent<Enemy>(), _damage);
        }
    }

}
