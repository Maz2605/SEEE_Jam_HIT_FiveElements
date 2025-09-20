using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class HeroKnight : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _damage = 60f;
    [SerializeField] private float _attackCooldown = 0.5f;
    [SerializeField] private float _explosionRadius = 2f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private GameObject _explosionPrefab;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform targetEnemy;
    private float lastAttackTime;

    private List<Transform> enemiesInRange = new List<Transform>();
    private bool hasExploded = false; // ✅ ngăn việc nổ nhiều lần

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(other.transform))
                enemiesInRange.Add(other.transform);

            if (targetEnemy == null)
                targetEnemy = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);

            if (other.transform == targetEnemy)
            {
                targetEnemy = null;
                anim.SetBool("isRun", false);
                anim.SetBool("isAttack", false);
                UpdateTargetEnemy();
            }
        }
    }

    private void FixedUpdate()
    {
        if (hasExploded) return; // đã nổ rồi thì ko làm gì nữa

        if (targetEnemy == null)
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isAttack", false);
            return;
        }

        if (!targetEnemy.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(targetEnemy);
            targetEnemy = null;
            UpdateTargetEnemy();
            return;
        }

        float distance = Vector2.Distance(transform.position, targetEnemy.position);

        Vector2 direction = (targetEnemy.position - transform.position).normalized;
        if (direction.x > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (distance > 0.5f)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetEnemy.position, _speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            anim.SetBool("isRun", true);
            anim.SetBool("isAttack", false);
        }
        else
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isAttack", true);

            if (Time.time - lastAttackTime >= _attackCooldown)
            {
                AttackWithExplosion();
                lastAttackTime = Time.time;
            }
        }
    }

    private void AttackWithExplosion()
    {
        if (hasExploded) return; // chỉ nổ 1 lần
        hasExploded = true;

        if (targetEnemy != null)
        {
            Enemy mainEnemy = targetEnemy.GetComponent<Enemy>();
            if (mainEnemy != null)
            {
                mainEnemy.TakeDamage(_damage);
            }
        }

        if (_explosionPrefab != null)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, _enemyLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit.transform == targetEnemy) continue;

            Enemy splashEnemy = hit.GetComponent<Enemy>();
            if (splashEnemy != null)
            {
                splashEnemy.TakeDamage(Mathf.CeilToInt(_damage * 0.5f));
            }
        }

        // 🔥 Biến mất sau khi explosion
        Destroy(gameObject, 0.2f);
    }

    private void UpdateTargetEnemy()
    {
        float minDist = float.MaxValue;
        Transform nearest = null;

        foreach (Transform enemy in enemiesInRange)
        {
            if (enemy == null) continue;
            if (!enemy.CompareTag("Enemy")) continue;

            float dist = Vector2.Distance(transform.position, enemy.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        targetEnemy = nearest;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
