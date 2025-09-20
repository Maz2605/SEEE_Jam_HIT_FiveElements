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
    private bool _isFar; // check enemy danh xa hay gan
    // Enemy State
    private float _health;
    private float _currentHealth;
    private float _speed;
    private float _damage;
    private float _speedAttack;
    private float _scoreValue;
    private int _countCoin;
    private EnemyType _type;

    //Get
    public float GetHealth => _health;
    public float GetCurrentHealth => _currentHealth;
    public float GetSpeed => _speed;
    public float GetDamage => _damage;
    public float GetSpeedAttack => _speedAttack;
    public float GetScoreValue => _scoreValue;
    public int GetCountCoin => _countCoin;
    public EnemyType GetEnemyType => _type;

    //UI Enemy

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _collider2D;
    public Animator GetAnimator => _animator;
    public Rigidbody2D GetRigidbody2D => _rb;

    [SerializeField] private EnemyUI _enemyUI;
    [SerializeField] private GameObject _effectEnemy;

    private Coroutine _cAttack;
    public Coroutine GetCAttack => _cAttack;
    public Coroutine SetCAttack
    {
        set { _cAttack = value; }
    }
    private bool _isStart = false;
    public bool IsStart
    {
        get => _isStart;
        set => _isStart = value;
    }
    private bool _isDead;
    private bool _isFrozen;
    private bool _isAttack;
    private Vector3 _posDoor = new Vector3(-5.12f,-0.22f,0f);
    
    private void OnEnable()
    {
        XuanEventManager.ReduceSpeed += ReductSpeed;
        XuanEventManager.EnemyBeFrozen += BeFrozen;
    }
    private void OnDisable()
    {
        XuanEventManager.ReduceSpeed -= ReductSpeed;
        XuanEventManager.EnemyBeFrozen -= BeFrozen;
    }

    public void InitState(EnemyStats data)
    {
        if(data == null)
        {
            Debug.LogError("Data Enemy is null");
            return;
        }
        _idEnemy = data.idEnemy;
        _health = data.health;
        _isFar = data.isFar;
        _currentHealth = 0f;
        _speed = data.speed;
        _damage = data.damage;
        _speedAttack = data.speedAttack;
        _scoreValue = data.scoreValue;
        _countCoin = data.countCoin;
        _animator.runtimeAnimatorController = data.animator;
        _type = EnemyType.Enemy;
        _enemyUI.SetImotionBar(_health);
        gameObject.tag = "Enemy";
        _collider2D.enabled = true;

        //Sau khi khoi tao xong bat dau di chuyen
        StartMove();
    }

    public virtual void StartMove()
    {
        if (_isDead) return; // neu da chet thi khong di chuyen nua

        if (_isFar) // neu la enemy danh xa thi di chuyen luon
        {
            _animator.SetBool("IsWalk", true);
            _rb.velocity = new Vector2(-_speed, _rb.velocity.y);
        }
        else // neu la enemy danh gan thi random di chuyen hoac chay
        {
            int random = Random.Range(0, 2);
            if (random == 1)
            {
                _animator.SetBool("IsWalk", true);
                _rb.velocity = new Vector2(-_speed, _rb.velocity.y); // di chuyen binh thuong
            }
            else
            {
                _animator.SetBool("IsRun", true);
                _rb.velocity = new Vector2(-_speed * 1.5f, _rb.velocity.y); // chay nhanh hon
            }
        }
    }
    public void StopMove() // dung di chuyen
    {
        _rb.velocity = Vector2.zero;
        _animator.SetBool("IsWalk", false);
        _animator.SetBool("IsRun", false);
    }
    
    public virtual void StartAttack() // bat dau tan cong
    {
        _cAttack = StartCoroutine(AttackWall());
    }
    public virtual IEnumerator AttackWall()  // coroutine tan cong
    {
        while(_currentHealth <= _health && _type == EnemyType.Enemy)  
        {
            Attack();  
            yield return new WaitForSeconds(_speedAttack);
        }
    }
    public virtual void Attack()
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
        _isFrozen = true;
        _animator.speed = 0f;
        _rb.velocity = Vector2.zero;
        if(_cAttack != null) StopCoroutine(_cAttack);
        DOVirtual.DelayedCall(time, () =>
        {
            _animator.speed = 1f;
            if (_currentHealth < _health && _type == EnemyType.Enemy)
            {
                if(_isAttack) _cAttack = StartCoroutine(AttackWall());

                _rb.velocity = new Vector2(-_speed, _rb.velocity.y);
                _isFrozen = false;
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
        if (_type != EnemyType.Enemy) return;

        _animator.SetTrigger("IsHit");
        StopMove();
        _currentHealth += damage;
        _enemyUI.UpdateImotionBar(_currentHealth);
        gameObject.tag = "Untagged";
        if (_currentHealth >= _health)
        {
            _currentHealth = _health;
            Die();
        }
        DOVirtual.DelayedCall(0.5f, () =>
        {
            gameObject.tag = "Enemy";
            if (_currentHealth < _health && _type == EnemyType.Enemy && !_isStart && !_isFrozen)
            {
                StartMove();
            }
        });
    }

    public void TakeDamage(Enemy enemy, float damage)
    {
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
        EnemySkill.Instance.UseKill(this);
    }
    public virtual void Die()
    {
        //////////////////////////////////////////
        //Debug.Log($"{name} đã chết, còn lại: {EnemyManager.Instance.EnemyCount}");

        gameObject.tag = "Untagged";
        _collider2D.enabled = false;
        EnemyManager.Instance.RemoveEnemy(this);
        if(_type == EnemyType.Enemy)
        {
            GameObject obj = PoolingManager.Spawn(_effectEnemy, transform.position, Quaternion.identity);
            _type = EnemyType.Village;
            _animator.SetBool("IsDead", true);
            StopMove();
            DOVirtual.DelayedCall(0.65f, () =>
            {
                _animator.runtimeAnimatorController = EnemyManager.Instance.RandomVillage();
                _enemyUI.SetActionFalseBar();
                _animator.SetBool("IsRun", true);
            });
            DOVirtual.DelayedCall(1.45f, () =>
            {
                PoolingManager.Despawn(obj);

                _rb.velocity = (transform.position - _posDoor).normalized * -_speed * 8f;
                _collider2D.enabled = true;
                DOVirtual.DelayedCall(2f, () =>
                {
                    PoolingManager.Despawn(gameObject);
                });
            });
        }
        SpawnCoin();
    }
    public void SpawnCoin()
    {
        for (int i = 0; i < _countCoin; i++)
        {
            // Spawn tại vị trí enemy (có thể thêm chút random nhỏ)
            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);

            Coin coin = PoolingManager.Spawn(EnemyManager.Instance.GetCoin, spawnPos, Quaternion.identity, EnemyManager.Instance.CoinEnemy);

            // Hướng bay về góc trái trên, thêm random để coin không chồng nhau
            Vector3 baseDir = new Vector3(1.3f, 1f, 0f).normalized; // góc trái trên
            Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);

            Vector3 finalDir = (baseDir + randomOffset).normalized;
            float force = Random.Range(15f, 25f); // lực bay

            coin.StartCoin(finalDir * force);
        }
    }


    #region Collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            if(_type == EnemyType.Enemy)
            {
                _isDead = true;
                StopMove();
                StartExplosion();
                TowerHealth.Instance.TakeDamage(_damage);
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
                _isStart = true;
                _isAttack = true;
            }
        }
    }
    #endregion

}
