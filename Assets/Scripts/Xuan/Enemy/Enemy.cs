using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Enemy State
    private float _health;
    private float _currentHealth;
    private float _speed;
    private float _damage;
    private float _speedAttack;
    private float _scoreValue;

    //UI Enemy
    [SerializeField] private Image _imotionBar;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;

    [SerializeField] private EnemyUI _enemyUI;

    [Header("Enemy Move Point")]
    private float _topMovePoint = 4f;
    private float _bottomMovePoint = -4f;
    private bool _isMoveY = false;

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
        _health = data.health;
        _currentHealth = 0f;
        _speed = data.speed;
        _damage = data.damage;
        _speedAttack = data.speedAttack;
        _scoreValue = data.scoreValue;
        _animator.runtimeAnimatorController = data.animator;

        _enemyUI.SetImotionBar(_health);
    }

    public void StartMove()
    {
        _rb.velocity = new Vector2(_speed,_rb.velocity.y);
        _animator.SetBool("IsWalk", true);

        if (transform.position.y >= _topMovePoint)
        {
            Debug.Log("Top");
            _rb.velocity = new Vector2(_rb.velocity.x, -_speed);
            _isMoveY = true;
        }
        else if(transform.position.y <= _bottomMovePoint)
        {
            Debug.Log("Bottom");
            _rb.velocity = new Vector2(_rb.velocity.x, _speed);
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
        while(_health > 0)
        {
            Attack();
            yield return new WaitForSeconds(_speedAttack);
        }
    }
    public void Attack()
    {
        _animator.SetTrigger("IsAttack");
        //
    }
    public void Hit(float damage)
    {
        
    }
    public void UseSkill()
    {

    }
    public void Die()
    {

    }
    //Va Cham
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            StopMove();
            StartAttack();
        }
    }
}
