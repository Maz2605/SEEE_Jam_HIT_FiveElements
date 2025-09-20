using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ice : MonoBehaviour
{
    private float _damage;
    private bool _isDamage;

    private void OnEnable()
    {
        _isDamage = true;
        DOVirtual.DelayedCall(5f, () =>
        {
            PoolingManager.Despawn(gameObject);
        });
    }
    public void InitDamage(float damage)
    {
        _damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall") && _isDamage)
        {
            Debug.Log("Ice hit the wall, dealing damage to the tower.");
            _isDamage = false;
            TowerHealth.Instance.TakeDamage(_damage);
        }
    }
}
