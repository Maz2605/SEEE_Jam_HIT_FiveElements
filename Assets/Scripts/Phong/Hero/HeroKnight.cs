using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class HeroKnight : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _damage = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform targetEnemy;

    // Danh sách enemy trong vùng trigger
    private List<Transform> enemiesInRange = new List<Transform>();

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

            // nếu chưa có target thì chọn enemy mới
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

                // chọn lại target khác trong vùng
                UpdateTargetEnemy();
            }
        }
    }

    private void FixedUpdate()
    {
        if (targetEnemy == null)
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isAttack", false);
            return;
        }

        // Enemy mất tag hoặc bị destroy
        if (!targetEnemy.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(targetEnemy);
            targetEnemy = null;
            UpdateTargetEnemy();
            return;
        }

        float distance = Vector2.Distance(transform.position, targetEnemy.position);

        // Quay mặt theo hướng
        Vector2 direction = (targetEnemy.position - transform.position).normalized;
        if (direction.x > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (distance > 0.5f) // chưa tới gần
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetEnemy.position, _speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            anim.SetBool("isRun", true);
            anim.SetBool("isAttack", false);
        }
        else
        {
            // đủ gần -> attack
            anim.SetBool("isRun", false);
            anim.SetBool("isAttack", true);

            Enemy enemyComp = targetEnemy.GetComponent<Enemy>();
            if (enemyComp != null)
                XuanEventManager.EnemyTakeDamage(enemyComp, _damage);
        }
    }

    private void UpdateTargetEnemy()
    {
        // chọn enemy gần nhất trong danh sách còn lại
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
}
