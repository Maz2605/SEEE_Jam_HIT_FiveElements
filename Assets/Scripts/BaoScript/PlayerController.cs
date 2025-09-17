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

    [Header("Tower Health")]
    [SerializeField] private float _healthTower;
    [SerializeField] private float _currentHealthTower;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private float attackTimer;

    private bool _isDead;
    private bool _isAttacking;
    private PlayerStateType _state = PlayerStateType.Idle;

    private readonly List<Transform> enemiesInRange = new List<Transform>();

    private CircleCollider2D rangeCollider;
    #region Getter Setter
    public float AttackRange => _attackRange;
    public float BulletSpeed => _bulletSpeed;   
    public float  AttackCooldown => _attackCooldown;    

    public float HealthTower => _healthTower;   

    public float CurrentHealthTower => _currentHealthTower;

    public bool IsAttacking => _isAttacking && !_isDead;
    public bool IsIdle => !_isDead && !_isAttacking;

    public void SetAttackRange(float Range)
    {
        _attackRange += Range;
    }

    public void SetHealthTower(float health)
    {
        _healthTower += health;
    }

    public void SetBulletSpeed(float bulletSpeed)
    {
        _bulletSpeed += bulletSpeed;
    }

    public void SetAttackCooldown(float attackCooldown)
    {
        _attackCooldown += attackCooldown;
    }

    public void SetCurrentHealthTower(float currentHealthTower)
    {
        _currentHealthTower += currentHealthTower;
    }
    #endregion
    private void Awake()
    {
        rangeCollider = GetComponent<CircleCollider2D>();
        if (animator == null)
            animator = GetComponent<Animator>();
        rangeCollider.isTrigger = true;
    }
    #region PlAYER ATTACK
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
        if (_isDead) return; 

        GameObject bulletObj = PoolingManager.Spawn(_bulletPrefab, _firePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        Vector3 direction = (target.position - _firePoint.position).normalized;
        bullet.Launch(direction, _bulletSpeed);

        ChangeState(PlayerStateType.Attack);

        _isAttacking = true;

        Invoke(nameof(ResetAttack), 0.2f);
    }
    private void ResetAttack()
    {
        _isAttacking = false;
        if (!_isDead) ChangeState(PlayerStateType.Idle);
    }

    public void Die()
    {
        if (_isDead) return;

        _isDead = true;
        _isAttacking = false;

        ChangeState(PlayerStateType.Die);
        Debug.Log("Player died → animation Die");
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
    #endregion

    #region ANIMATION CONTROL
    private void ChangeState(PlayerStateType newState)
    {
        if (_state == newState) return; // tránh spam trigger

        _state = newState;

        switch (newState)
        {
            case PlayerStateType.Idle:
                animator.SetTrigger("Idle");
                break;
            case PlayerStateType.Attack:
                animator.SetTrigger("Attack");
                break;
            case PlayerStateType.Die:
                animator.SetTrigger("Die");
                break;
        }
    }
    #endregion
}
