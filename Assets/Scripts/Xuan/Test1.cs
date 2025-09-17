using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        XuanEventManager.EnemyTakeDamage(collision.GetComponent<Enemy>(), 10);
    }
}
