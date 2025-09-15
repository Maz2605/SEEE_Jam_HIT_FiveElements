using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandlerSkill2 : MonoBehaviour
{
    [SerializeField] private float damage = 5f;
    [SerializeField] private float tickInterval = 1f; // mỗi 1 giây gây sát thương
    private HashSet<Collider2D> enemiesInRange = new HashSet<Collider2D>();

    private void Start()
    {
        // tự hủy sau 5 giây
        DOVirtual.DelayedCall(5f, () =>
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        });

        // bắt đầu gây damage theo interval
        StartCoroutine(DamageOverTime());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other);
        }
    }

    private IEnumerator DamageOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickInterval);

            foreach (var enemy in enemiesInRange)
            {
                if (enemy != null) 
                {
                    Debug.Log($"-{damage} máu vào {enemy.name}");
                    // ví dụ: enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
        }
    }
}