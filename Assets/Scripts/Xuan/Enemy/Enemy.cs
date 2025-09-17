using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private string _idEnemy;
    public string GetIDEnemy => _idEnemy;
    private bool _isFar;
    // Enemy State
    private float _health;
    private float _currentHealth;
    private float _speed;
    private float _damage;
    private float _speedAttack;
    private float _scoreValue;
    private EnemyType _type;

    public float GetHealth => _health;
    public float GetCurrentHealth => _currentHealth;
    public float GetSpeed => _speed;
    public float GetDamage => _damage;
    public float GetSpeedAttack => _speedAttack;
    public float GetScoreValue => _scoreValue;

    //UI Enemy

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _collider2D;
    public Animator GetAnimator => _animator;

    [SerializeField] private EnemyUI _enemyUI;
    //[SerializeField] private EnemySkill _enemySkill;

    [Header("Enemy Move Point")]
    private float _topMovePoint = 4f;
    private float _bottomMovePoint = -4f;
    private bool _isMoveY = false;
    private bool _isDelayTakeDamage = false;

    private void OnEnable()
    {
        XuanEventManager.EnemyTakeDamage += TakeDamage;
        XuanEventManager.ReduceSpeed += ReductSpeed;
        XuanEventManager.EnemyBeFrozen += BeFrozen;
    }
    private void OnDisable()
    {
        XuanEventManager.EnemyTakeDamage -= TakeDamage;
        XuanEventManager.ReduceSpeed -= ReductSpeed;
        XuanEventManager.EnemyBeFrozen -= BeFrozen;
    }

    private void Start()
    {
        StartMove();
    }
    private void Update()
    {
        CheckMove();
    }

    public void InitState(EnemyStats data)
    {
        _idEnemy = data.idEnemy;
        _health = data.health;
        _isFar = data.isFar;
        _currentHealth = 0f;
        _speed = data.speed;
        _damage = data.damage;
        _speedAttack = data.speedAttack;
        _scoreValue = data.scoreValue;
        _animator.runtimeAnimatorController = data.animator;
        _type = EnemyType.Enemy;
        _enemyUI.SetImotionBar(_health);
    }

    public void StartMove()
    {
        if(_isFar)
        {
            _animator.SetBool("IsWalk", true);
            _rb.velocity = new Vector2(_speed, _rb.velocity.y);
        }
        else
        {
            int random = Random.Range(0, 2);
            if (random == 1)
            {
                _animator.SetBool("IsWalk", true);
                _rb.velocity = new Vector2(_speed, _rb.velocity.y);
            }
            else
            {
                _animator.SetBool("IsRun", true);
                _rb.velocity = new Vector2(_speed * 1.5f, _rb.velocity.y);
            }
        }
        if (transform.position.y >= _topMovePoint)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _speed);
            _isMoveY = true;
        }
        else if (transform.position.y <= _bottomMovePoint)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -_speed);
            _isMoveY = true;
        }
    }
    public void CheckMove()
    {
        if(!_isMoveY) return;

        if (transform.position.y <= _topMovePoint && transform.position.y >= _bottomMovePoint)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _isMoveY = false;
        }
    }
    public void StopMove()
    {
        _rb.velocity = Vector2.zero;
        _animator.SetBool("IsWalk", false);
    }
    
    public void StartAttack()
    {
        StartCoroutine(AttackWall());
    }
    private IEnumerator AttackWall()
    {
        while(_health > 0 && _type == EnemyType.Enemy)
        {
            Attack();
            yield return new WaitForSeconds(_speedAttack);
        }
    }
    public void Attack()
    {
        _animator.SetTrigger("IsAttack");
        UseSkill();
    }
    public void BeFrozen(Enemy enemy, float time)
    {
        enemy.Frozen(time);
    }
    public void Frozen(float time)
    {
        _animator.speed = 0f;
        _rb.velocity = Vector2.zero;
        StopCoroutine(AttackWall());
        DOVirtual.DelayedCall(time, () =>
        {
            _animator.speed = 1f;
            if (_currentHealth < _health && _type == EnemyType.Enemy)
            {
                StartCoroutine(AttackWall());
                _rb.velocity = new Vector2(_speed, _rb.velocity.y);
            }

        });
    }

    public void StartExplosion()
    {
        EnemyManager.Instance.RemoveEnemy(this);
        _animator.SetBool("IsDead", true);
        EnemyManager.Instance.SpawnExplosionInEnemy(this);
        DOVirtual.DelayedCall(1f, () =>
        {
            PoolingManager.Despawn(gameObject);
        });
    }
    
    public void Hit(float damage)
    {
        _animator.SetTrigger("IsHit");
        _currentHealth += damage;
        _enemyUI.UpdateImotionBar(_currentHealth);
        if (_currentHealth >= _health)
        {
            _currentHealth = _health;
            Die();
        }
    }
    public void TakeDamage(Enemy enemy, float damage)
    {
        Debug.Log("Enemy Take Damage" + damage);
        enemy.Hit(damage);
    }
    public void ReductSpeed(Enemy enemy, float r, float time)
    {
        enemy.SetSpeed(r, time);
    }
    public void SetSpeed(float r, float time)
    {
        StartCoroutine(Speed(r, time));
    }
    private IEnumerator Speed(float r, float time)
    {
        _rb.velocity = new Vector2(_speed * r, _rb.velocity.y);

        yield return new WaitForSeconds(time);

        _rb.velocity = new Vector2(_speed, _rb.velocity.y);
    }
    public void UseSkill()
    {
        //_enemySkill.UseKill(this);
        EnemySkill.Instance.UseKill(this);
    }
    public void Die()
    {
        EnemyManager.Instance.RemoveEnemy(this);
        if(_type == EnemyType.Enemy)
        {
            _type = EnemyType.Village;
            _animator.SetBool("IsDead", true);
            StopMove();
            DOVirtual.DelayedCall(0.5f, () =>
            {
                _animator.runtimeAnimatorController = EnemyManager.Instance.RandomVillage();
                _animator.SetBool("IsRun", true);
            });
            DOVirtual.DelayedCall(1.2f, () =>
            {
                _rb.velocity = new Vector2(_speed * 1.5f, _rb.velocity.y);
            });
        }
    }
    //Va Cham
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            if(_type == EnemyType.Enemy)
            {
                StopMove();
                StartExplosion();
            }
            else
            {
                PoolingManager.Despawn(gameObject);
            }
        }
        if(collision.CompareTag("Stop") && _isFar)
        {
            if (_type == EnemyType.Enemy)
            {
                StopMove();
                StartAttack();
            }
        }
    }
}
