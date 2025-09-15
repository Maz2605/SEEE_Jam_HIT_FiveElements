using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float bulletSpeed = 10f;

    private float attackTimer;

    // Danh sách enemy trong vùng bắn
    private readonly List<Transform> enemiesInRange = new List<Transform>();

    private void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && enemiesInRange.Count > 0)
        {
            Transform target = GetNearestEnemy();
            if (target != null)
            {
                Shoot(target);
                attackTimer = attackCooldown;
            }
        }
    }

    private Transform GetNearestEnemy()
    {
        Transform nearest = null;
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            if (enemiesInRange[i] == null) continue;

            float distance = Vector3.Distance(transform.position, enemiesInRange[i].position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemiesInRange[i];
            }
        }

        return nearest;
    }

    private void Shoot(Transform target)
    {
        GameObject bulletObj = PoolingManager.Spawn(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        Vector3 direction = (target.position - firePoint.position).normalized;
        bullet.Launch(direction, bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(other.transform))
            {
                enemiesInRange.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);
        }
    }
}
