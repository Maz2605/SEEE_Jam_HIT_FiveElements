using System.Collections.Generic;
using UnityEngine;

public class HeroFlight : MonoBehaviour
{
    [Header("Stats")]
    public GameObject bulletPrefab;
    public Transform firePoint; // vị trí bắn
    public float fireRate = 1f; // bắn mỗi 1s
    public float bulletSpeed = 10f;

    private List<Transform> enemiesInRange = new List<Transform>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(other.transform))
                enemiesInRange.Add(other.transform);

            // nếu chưa có bắn thì start
            if (!IsInvoking(nameof(ShootEnemy)))
                InvokeRepeating(nameof(ShootEnemy), 0f, fireRate);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);

            // nếu không còn enemy thì dừng bắn
            if (enemiesInRange.Count == 0)
                CancelInvoke(nameof(ShootEnemy));
        }
    }

    private void ShootEnemy()
    {
        // dọn sạch enemy null (đã chết)
        enemiesInRange.RemoveAll(e => e == null);

        if (enemiesInRange.Count == 0)
        {
            CancelInvoke(nameof(ShootEnemy));
            return;
        }

        // chọn enemy ngẫu nhiên
        int randomIndex = Random.Range(0, enemiesInRange.Count);
        Transform target = enemiesInRange[randomIndex];

        if (target == null) return;

        // tạo đạn
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // tính hướng tới enemy
        Vector2 direction = (target.position - firePoint.position).normalized;

        // add velocity cho đạn
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = direction * bulletSpeed;

        Debug.Log("HeroFlight bắn đạn vào " + target.name);
    }
}