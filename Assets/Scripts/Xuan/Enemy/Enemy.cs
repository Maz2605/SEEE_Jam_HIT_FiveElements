using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy State
    private float _health = 1f;
    private float _speed = 1.2f;
    private float _damage;
    private float _speedAttack = 1f;
    private float _scoreValue;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        StartMove();
    }

    public void InitState(EnemyStats data)
    {
        _health = data.health;
        _speed = data.speed;
        _damage = data.damage;
        _scoreValue = data.scoreValue;
    }

    public void StartMove()
    {
        _rb.velocity = Vector2.left * -_speed;
        _animator.SetBool("IsWalk", true);
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
