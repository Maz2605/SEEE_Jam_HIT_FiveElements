using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FlightBullet : MonoBehaviour
{
    public float damage = 50f;
    public GameObject boomPrefab;

    private void Start()
    {
        DOVirtual.DelayedCall(3f, () => { Destroy(gameObject); });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Gay sat thuong
            collision.GetComponent<Enemy>().TakeDamage(collision.gameObject.GetComponent<Enemy>(),damage);
            Instantiate(boomPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
