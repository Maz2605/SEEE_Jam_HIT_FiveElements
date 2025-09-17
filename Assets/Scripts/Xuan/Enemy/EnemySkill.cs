using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySkill : MonoBehaviour
{
    [Header("Ball Skill")]
    [SerializeField] private Ball ball;
    [SerializeField] private Transform ballParent;
    [Header("Ball Animation")]
    [SerializeField] private RuntimeAnimatorController fireBall;
    [SerializeField] private RuntimeAnimatorController lightBall;
    [SerializeField] private RuntimeAnimatorController magicBall;
    [SerializeField] private RuntimeAnimatorController magicArrow;
    //
    public void UseKill(Enemy enemy)
    {
        switch(enemy.GetIDEnemy)
        {
            //Magic Skill
            case "fire":
                SpawnFireBall(enemy);
                break;
            case "light":
                SpawnLightBall(enemy);
                break;
            case "magic":
                SpawnMagicBall(enemy);
                break;

            //Enemy Type Zoombie Will Explosion
            case "zombie1":
                Explosion(enemy);
                break;
            case "zombie2":
                Explosion(enemy);
                break;
            case "zombie3":
                Explosion(enemy);
                break;
            case "zombie4":
                Explosion(enemy);
                break;

            default:
                break;
        }
    }
    public void SpawnFireBall(Enemy enemy)
    {
        Ball newObj = PoolingManager.Spawn(ball, enemy.transform.position + new Vector3(0.2f, 0f, 0f), Quaternion.identity, ballParent);
        newObj.InitBall(fireBall, 10f);
    }
    public void SpawnLightBall(Enemy enemy)
    {
        Ball newObj = PoolingManager.Spawn(ball, enemy.transform.position + new Vector3(0.2f, 0f, 0f), Quaternion.identity, ballParent);
        newObj.InitBall(lightBall, 10f);
    }
    public void SpawnMagicBall(Enemy enemy)
    {
        Ball newObj = PoolingManager.Spawn(ball, enemy.transform.position + new Vector3(0.2f, 0f, 0f), Quaternion.identity, ballParent);
        int rand = Random.Range(0, 2);
        if(rand == 0)
            newObj.InitBall(magicArrow, 10f);
        else
            newObj.InitBall(magicBall, 10f);
    }

    public void Explosion(Enemy enemy)
    {
        //Code here
        enemy.GetAnimator.SetBool("IsExplo", true);
        DOVirtual.DelayedCall(1f, () =>
        {
            PoolingManager.Despawn(enemy.gameObject);
        });
    }
}
