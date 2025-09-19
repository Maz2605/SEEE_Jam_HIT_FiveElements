using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : Singleton<PlayerController>
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private float _bulletSpeed = 100f;
    [SerializeField] private PlayerLevel _currentLevel = PlayerLevel.Normal;

    [Header("Range Settings")]
    [SerializeField] private float _attackRange = 5f;

    [Header("Animation")]
    [SerializeField] private Animator _animator;

    [Header("UI")]
    private HealthBarController _healthBar;

    [Header("Tower Health")]
    [SerializeField] private TowerHealth _towerHealth;

    private float attackTimer;

    private bool _isDead;
    private bool _isAttacking;
    private bool _isUsingSuperHappy = false;
    private PlayerStateType _state = PlayerStateType.Idle;

    private readonly List<Transform> enemiesInRange = new List<Transform>();

    private CircleCollider2D rangeCollider;
    #region Getter Setter
    public float AttackRange => _attackRange;
    public float BulletSpeed => _bulletSpeed;   
    public float  AttackCooldown => _attackCooldown;    

    public bool IsAttacking => _isAttacking && !_isDead;
    public bool IsIdle => !_isDead && !_isAttacking;
    public bool IsDead => _isDead;


    public void SetAttackRange(float Range)
    {
        _attackRange += Range;
    }

    public void SetBulletSpeed(float bulletSpeed)
    {
        _bulletSpeed += bulletSpeed;
    }

    public void SetAttackCooldown(float attackCooldown)
    {
        _attackCooldown += attackCooldown;
    }

    #endregion
    private void Awake()
    {
        rangeCollider = GetComponent<CircleCollider2D>();
        rangeCollider.isTrigger = true;

        if (_animator == null)
            _animator = GetComponent<Animator>();

        _healthBar = GameObject.FindGameObjectWithTag("HealthBarTower").GetComponent<HealthBarController>();
        _healthBar.Initialize(30f);


    }
    #region UPDATE LOOP
    private void Update()
    {
        if (rangeCollider.radius != _attackRange)
            rangeCollider.radius = _attackRange;

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && enemiesInRange.Count > 0)
        {
            if (_towerHealth != null && _towerHealth.IsDead) return;
            Transform target = XuanEventManager.GetEnemy(transform.position,10f).transform;
            if (target != null)
            {
                PrepareAttack(target);
                attackTimer = _attackCooldown;
            }
        }
        
    }
    #endregion

    #region ATTACK LOGIC
    private Transform _cachedTarget;
    private Tween _fireLoopTween;

    private void PrepareAttack(Transform target)
    {
        if (_isDead) return;
            
        _cachedTarget = target;
        ChangeState(PlayerStateType.Attack);
        _isAttacking = true;
    }

    private void StartFireLoop(float interval)
    {
        _fireLoopTween?.Kill();

        _fireLoopTween = DOVirtual.DelayedCall(interval, () =>
        {
            FireBullet();
            StartFireLoop(interval);
        });
    }

    private void ResetAttack()
    {
        _isAttacking = false;

        _fireLoopTween?.Kill();

        if (!_isDead) ChangeState(PlayerStateType.Idle);
    }
    public void FireBullet()
    {
        if (_isDead || _cachedTarget == null || (_towerHealth != null && _towerHealth.IsDead)) return;

        GameObject bulletObj = PoolingManager.Spawn(_bulletPrefab, _firePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        Vector3 direction = (_cachedTarget.position - _firePoint.position).normalized;
        bullet.Launch(direction, _bulletSpeed);

        Invoke(nameof(ResetAttack), 0.2f);
    }

    private void FireBulletMulti(int count, float interval)
    {
        for (int i = 0; i < count; i++)
        {
            DOVirtual.DelayedCall(i * interval, () =>
            {
                FireBullet();
            });
        }
    }

    public void FireBulletByLevel()
    {
        if (_currentLevel == PlayerLevel.SuperHappy)
        {
            FireBulletMulti(4, 0f); 
        }
        else
        {
            FireBullet();
        }
    }
    #endregion

    #region DEATH LOGIC
    public void Die()
    {
        if (_isDead) return;

        _isDead = true;
        _isAttacking = false;

        ChangeState(PlayerStateType.Die);
        Debug.Log("Player died → animation Die");

        // TODO: display Game Over UI, stop the game, etc.
    }
    #endregion

    #region ENEMY DETECTION
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
        if (_state == newState) return;

        _state = newState;

        switch (newState)
        {
            case PlayerStateType.Idle:
                _animator.SetTrigger("Idle");
                break;
            case PlayerStateType.Attack:
                _animator.SetTrigger("Attack");
                break;
            case PlayerStateType.Die:
                _animator.SetTrigger("Die");
                break;
        }
    }
    #endregion


}
