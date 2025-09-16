using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : Singleton<PlayerController>
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private float _bulletSpeed = 10f;

    [Header("Range Settings")]
    [SerializeField] private float _attackRange = 5f; 

    private float attackTimer;
    private readonly List<Transform> enemiesInRange = new List<Transform>();

    private CircleCollider2D rangeCollider;

    public float AttackRange => _attackRange;
    public float BulletSpeed => _bulletSpeed;   

    
    private void Awake()
    {
        rangeCollider = GetComponent<CircleCollider2D>();
        rangeCollider.isTrigger = true;
    }

    private void Update()
    {
        if (rangeCollider.radius != _attackRange)
            rangeCollider.radius = _attackRange;

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && enemiesInRange.Count > 0)
        {
            Transform target = GetNearestEnemy();
            if (target != null)
            {
                Shoot(target);
                attackTimer = _attackCooldown;
            }
        }
    }

    private Transform GetNearestEnemy()
    {
        Transform nearest = null;
        float minDistance = Mathf.Infinity;

        enemiesInRange.RemoveAll(e => e == null); 

        foreach (var enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.position);
            if (distance < minDistance && distance <= _attackRange)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;
    }

    private void Shoot(Transform target)
    {
        GameObject bulletObj = PoolingManager.Spawn(_bulletPrefab, _firePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        Vector3 direction = (target.position - _firePoint.position).normalized;
        bullet.Launch(direction, _bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !enemiesInRange.Contains(other.transform))
            enemiesInRange.Add(other.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInRange.Remove(other.transform);
    }
}
