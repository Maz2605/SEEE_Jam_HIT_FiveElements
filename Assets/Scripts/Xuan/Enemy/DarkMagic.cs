using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMagic : Enemy
{
    private float _xFar = 5f;
    private float _xNear = -3f;
    private float _yTop = 4f;
    private float _yBottom = -4f;

    private float _timeDelaySkill = 8f;
    private float _timeDelay;
    private bool _isSkill1 = false;

    public override void Start()
    {
        StartMove();
    }

    private void Update()
    {
        if (_isSkill1) return;

        _timeDelay += Time.deltaTime;
    }
    public override void StartMove()
    {
        float randY = Random.Range(_yBottom, _yTop);
        transform.DOMove(new Vector3(_xFar, randY, 0f), 2f).OnComplete(() => 
        {
            StartAttack();
        });
    }
    public override void StartAttack()
    {
        if(base.GetCurrentHealth < base.GetHealth)
            StartCoroutine(AttackWall());
    }
    public override IEnumerator AttackWall()
    {
        Debug.Log("Dark Magic Start Attack");
        
        while (base.GetCurrentHealth < base.GetHealth && base.GetEnemyType == EnemyType.Enemy)
        {
            Attack();
            yield return new WaitForSeconds(base.GetSpeedAttack);
        }
    }

    public override void Attack()
    {
        if(_isSkill1) return;
        if (_timeDelay <= _timeDelaySkill)
        {
            Debug.Log("Skill Normal");
            //Attack
            EnemySkill.Instance.UseKill(this);
            return;
        }
        _timeDelay = 0f;
        int rand = Random.Range(0, 2);
        if(rand == 0) Skill1();
        else Skill2();
    }

    public void Skill2()
    {
        StopCoroutine(AttackWall());
        base.GetAnimator.SetTrigger("IsAttack2");

        Debug.Log("Dark Magic Skill 1");
    }
    public void Skill1()
    {
        _isSkill1 = true;
        StopCoroutine(AttackWall());
        float randY = Random.Range(_yBottom, _yTop);
        base.GetAnimator.SetBool("IsRun", true);
        transform.DOMove(new Vector3(_xNear, randY, 0f), 2f).OnComplete(() =>
        {
            base.GetAnimator.SetBool("IsRun", false);
            base.GetAnimator.SetTrigger("IsAttack1");
        });
        DOVirtual.DelayedCall(6f, () =>
        {
            transform.DOMove(new Vector3(_xFar, randY, 0f), 2f).OnComplete(() =>
            {
                StartCoroutine(AttackWall());
                _isSkill1 = false;
            });
        });

        Debug.Log("Dark Magic Skill 2");
    }

    public override void Die()
    {
        gameObject.tag = "Untagged";
        EnemyManager.Instance.RemoveEnemy(this);

        base.SpawnCoin();
        base.GetAnimator.SetBool("IsDead", true);

        DOVirtual.DelayedCall(1f, () =>
        {
            PoolingManager.Despawn(this.gameObject);
        });
    }

}
