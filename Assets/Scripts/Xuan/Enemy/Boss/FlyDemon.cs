using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDemon : Enemy
{
    [SerializeField] private Ball _fireBall;
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
        if (Vector3.Distance(transform.position, new Vector3(4f, -2f, 0f)) <= 0.1f)
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
        base.GetAnimator.SetTrigger("IsAttack");
        DOVirtual.DelayedCall(0.7f, () =>
        {
            Ball news = PoolingManager.Spawn(_fireBall, transform.position + new Vector3(-1f, 1.5f, 0f), Quaternion.identity);
            Ball news1 = PoolingManager.Spawn(_fireBall, transform.position + new Vector3(-1f, 1.5f, 0f), Quaternion.identity);
            Ball news2 = PoolingManager.Spawn(_fireBall, transform.position + new Vector3(-1f, 1.5f, 0f), Quaternion.identity);
            news.InitSpecialBall(Vector2.left,8f,base.GetDamage);
            news1.InitSpecialBall(Quaternion.AngleAxis(15f, Vector3.forward) * Vector2.left, 8f,base.GetDamage);
            news2.InitSpecialBall(Quaternion.AngleAxis(-15f, Vector3.forward) * Vector2.left, 8f,base.GetDamage);

        });

        DOVirtual.DelayedCall(3f, () =>
        {
            _isUseSkill = false;
            float randY = Random.Range(1f, 2f);
            float targetY;

            if (Random.Range(0, 2) == 1) // đi lên
            {
                targetY = Mathf.Min(transform.position.y + randY, -0.5f);
            }
            else // đi xuống
            {
                targetY = Mathf.Max(transform.position.y - randY, -2.8f);
            }

            transform.DOMove(new Vector3(transform.position.x, targetY, transform.position.z), 0.5f);
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
