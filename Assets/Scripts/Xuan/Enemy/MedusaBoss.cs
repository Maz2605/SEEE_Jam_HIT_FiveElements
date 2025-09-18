using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaBoss : Enemy
{
    private float _xFar = 5f;
    private float _xNear = -3f;
    private float _yTop = 4f;
    private float _yBottom = -4f;

    private float _timeDelaySkill = 8f;
    private float _timeDelay;
    private bool _isSkill1 = false;

    [SerializeField] private GameObject _effectSkill1;
    private void Start()
    {
        base.GetAnimator.SetBool("IsStart", true);
    }

    private void Update()
    {
        if (_isSkill1) return;
        _timeDelay += Time.deltaTime;
    }
    public override void StartMove()
    {
        float randY = Random.Range(_yBottom, _yTop);
        transform.DOMove(new Vector3(0f, 0f, 0f), 2f).OnComplete(() =>
        {
            base.GetAnimator.SetBool("IsStart", false);
            StartAttack();
        });
    }
    public override void StartAttack()
    {
        if (base.GetCurrentHealth < base.GetHealth)
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
        if (_isSkill1) return;
        if (_timeDelay <= _timeDelaySkill)
        {
            Debug.Log("Skill Normal");
            //Attack
            EnemySkill.Instance.UseKill(this);
            return;
        }
        _timeDelay = 0f;
        int rand = Random.Range(0, 2);
        if (rand == 0) Skill1();
        else Skill2();
    }

    public void Skill2()
    {

        Debug.Log("Dark Magic Skill 2");
    }
    public void Skill1()
    {
        
        Debug.Log("Dark Magic Skill 2");
    }
    public void SkillSpecial()
    {

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
