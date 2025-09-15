using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeTime = 2f;

    private Vector3 moveDirection;
    private float speed;
    private float timer;

    public void Launch(Vector3 direction, float bulletSpeed)
    {
        moveDirection = direction;
        speed = bulletSpeed;
        timer = 0f;
    }

    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            PoolingManager.Despawn(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            PoolingManager.Despawn(gameObject);
        }
    }
}
