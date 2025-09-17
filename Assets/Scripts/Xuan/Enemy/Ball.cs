using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;

    public void InitBall(RuntimeAnimatorController run, float speedBall)
    {
        _animator.runtimeAnimatorController = run;
        _rb.velocity = new Vector2(speedBall, _rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            _animator.SetBool("IsTrigger", true);
            _rb.velocity = Vector2.zero;
            DOVirtual.DelayedCall(0.2f, () =>
            {
                PoolingManager.Despawn(gameObject);
            });
        }

    }
}
