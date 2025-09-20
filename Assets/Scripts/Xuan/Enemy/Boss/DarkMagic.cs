using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMagic : Enemy
{
    /*private float _xFar = 5f;
    private float _xNear = -3f;
    private float _yTop = 4f;
    private float _yBottom = -4f;

    private float _timeDelaySkill = 8f;
    private float _timeDelay;
    private bool _isSkill1 = false;

    [SerializeField] private GameObject _effectSkill1;

    private Vector3 _targetPos;
    private bool _isMoving = false;
    private bool _isStunned = false;

    private void Update()
    {
        if (_isMoving && !_isStunned)
        {
            MoveToTarget();
        }
        // Delay su dung skill
        if (_isSkill1) return;
        _timeDelay += Time.deltaTime;
    }
    public override void StartMove() // goi sau khi khoi tao xong
    {
        SetNewTarget();
    }
    private void SetNewTarget()
    {
        _targetPos = RandomPos();
        _isMoving = true;
        base.GetAnimator.SetBool("IsWalk", true);
    }

    private void MoveToTarget()
    {
        Vector2 dir = (_targetPos - transform.position).normalized;
        base.GetRigidbody2D.velocity = dir * base.GetSpeed;

        if (Vector2.Distance(transform.position, _targetPos) < 0.1f)
        {
            // Đến nơi -> dừng
            base.GetRigidbody2D.velocity = Vector2.zero;
            base.GetAnimator.SetBool("IsWalk", false);
            _isMoving = false;
        }
    }
    public void CheckPos()
    {
        
    }

    public Vector3 RandomPos()
    {
        float randY = Random.Range(_yBottom, _yTop);
        float ranX = Random.Range(_xNear, _xFar);

        return new Vector3(ranX, randY, 0f);
    }
    public override void StartAttack()
    {
        if(base.GetCurrentHealth < base.GetHealth)
            base.SetCAttack = StartCoroutine(AttackWall());
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
        *//*StopCoroutine(base.GetCAttack);
        base.GetAnimator.SetTrigger("IsAttack2");

        for(int i=0;i<5;i++)
        {
            float randY = Random.Range(_yBottom, _yTop);
            float ranX = Random.Range(-2f, 5f);

            GameObject effect = PoolingManager.Spawn(_effectSkill1, new Vector3(ranX, randY, 0f), Quaternion.identity);
            DOVirtual.DelayedCall(2f, () =>
            {
                PoolingManager.Despawn(effect);
            });
        }
*//*
        Debug.Log("Dark Magic Skill 1");
    }
    public void Skill1()
    {
        _isSkill1 = true;
        *//*StopCoroutine(base.GetCAttack);
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
                base.SetCAttack = StartCoroutine(AttackWall());
                _isSkill1 = false;
            });
        });
*//*
        Debug.Log("Dark Magic Skill 1");
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
    }*/

}
