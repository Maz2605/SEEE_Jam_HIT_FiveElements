using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MedusaBoss : Enemy
{
    [SerializeField] private Ball _green;
    [SerializeField] private Ball _power;
    private bool _isUseSkill;
    private void Update()
    {
        if (!base.IsStart) CheckPosStart();
    }
    public override void StartMove()
    {
        if (base.IsStart) return;

        base.GetAnimator.SetBool("IsWalk", true);

        base.GetRigidbody2D.velocity = new Vector2(-base.GetSpeed, 0f);
    }
    private void CheckPosStart()
    {
        if (Vector3.Distance(transform.position, new Vector3(3f, -2f, 0f)) <= 0.2f)
        {
            base.GetRigidbody2D.velocity = Vector2.zero;
            base.IsStart = true;
            StartAttack();
        }
    }
    public override void StartAttack()
    {
        if (base.GetCurrentHealth < base.GetHealth)
            base.SetCAttack = StartCoroutine(AttackWall());
    }
    public override IEnumerator AttackWall()
    {
        while (base.GetCurrentHealth < base.GetHealth && base.GetEnemyType == EnemyType.Enemy)
        {
            Attack();
            yield return new WaitForSeconds(base.GetSpeedAttack);
        }
    }

    public override void Attack()
    {
        if (_isUseSkill) return;

        _isUseSkill = true;
        if (Random.Range(0, 2) == 1) 
        {
            base.GetAnimator.SetTrigger("IsAttack");

            DOVirtual.DelayedCall(0.7f, () =>
            {
                Ball news = PoolingManager.Spawn(_green, transform.position + new Vector3(-1f,0f, 0f), Quaternion.identity);
                news.InitSpecialBall(Vector2.left, 8f, base.GetDamage);
            });
        }
        else 
        {
            base.GetAnimator.SetTrigger("IsSpecial");
            EnemyManager.Instance.SpawnEnemy(4, 1f, "fire");
            DOVirtual.DelayedCall(0.7f, () =>
            {
                Ball news = PoolingManager.Spawn(_power, transform.position + new Vector3(-1f, 1f, 0f), Quaternion.identity);
                news.InitSpecialBall(Vector2.left, 8f, base.GetDamage);
            });

        }
        DOVirtual.DelayedCall(3f, () =>
        {
            _isUseSkill = false;
        });
    }

    public override void Die()
    {
        gameObject.tag = "Untagged";
        EnemyManager.Instance.RemoveEnemy(this);
        base.GetAnimator.SetBool("IsDead", true);

        DOVirtual.DelayedCall(2f, () =>
        {
            PoolingManager.Despawn(gameObject);
            SpawnCoin();
        });
    }
}
