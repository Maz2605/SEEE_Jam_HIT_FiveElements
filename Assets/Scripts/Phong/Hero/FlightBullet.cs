using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightBullet : MonoBehaviour
{
    public float damage = 50f;
    public GameObject boomPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Gay sat thuong
            XuanEventManager.EnemyTakeDamage(collision.gameObject.GetComponent<Enemy>(), damage);
            Instantiate(boomPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
