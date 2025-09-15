using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySkill : MonoBehaviour
{
    //Test Skill Ball
    [SerializeField] private GameObject ball;
    //
    public void UseKill(Enemy enemy)
    {
        switch(enemy.GetIDEnemy)
        {
            case "magic":
                MagicSkill(enemy);
                break;

            default:
                break;
        }
    }

    public void MagicSkill(Enemy enemy)
    {
        GameObject newObj = PoolingManager.Spawn(ball, enemy.transform.position, Quaternion.identity);
        Rigidbody2D rd = newObj.GetComponent<Rigidbody2D>();
        rd.velocity = new Vector2(10f,rd.velocity.y);
    }
}
