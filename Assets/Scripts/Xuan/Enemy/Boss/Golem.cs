using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy
{
    [SerializeField] private GameObject _effectSkill1;
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
        if(Vector3.Distance(transform.position ,new Vector3(2f,0f,0f)) <= 0.1f)
        {
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
            GameObject news = PoolingManager.Spawn(_effectSkill1, transform.position + new Vector3(-2f, 0.2f, 0f), Quaternion.identity);
            DOVirtual.DelayedCall(0.4f, () =>
            {
                GameObject news = PoolingManager.Spawn(_effectSkill1, transform.position + new Vector3(-4f, 0.2f, 0f), Quaternion.identity);
                DOVirtual.DelayedCall(0.4f, () =>
                {
                    GameObject news = PoolingManager.Spawn(_effectSkill1, transform.position + new Vector3(-6f, 0.2f, 0f), Quaternion.identity);
                });
            });
            
        });

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
