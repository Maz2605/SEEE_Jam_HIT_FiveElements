using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifetime = 2f;
    [SerializeField] private float _explosionRadius = 2f; 
    [SerializeField] private LayerMask enemyLayer;       

    private Vector3 _moveDirection;
    private float _speed;
    private float _timer;


    public void Launch(Vector3 direction, float bulletSpeed)
    {
        _moveDirection = direction;
        _speed = bulletSpeed;
        _timer = 0f;
    }

    private void Update()
    {
        transform.position += _moveDirection * _speed * Time.deltaTime;

        _timer += Time.deltaTime;
        if (_timer >= _lifetime)
        {
            PoolingManager.Despawn(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        DamageMainEnemy(other);

        DamageSplash(other);

        DespawnBullet();
    }

    private void DamageMainEnemy(Collider2D other)
    {
        EnemyReference refMain = other.GetComponent<EnemyReference>();
        if (refMain != null)
        {
            refMain.Health.TakeDamage(_damage);
        }
    }

    private void DamageSplash(Collider2D mainTarget)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == mainTarget.gameObject) continue; 

            EnemyReference refEnemy = hit.GetComponent<EnemyReference>();
            if (refEnemy != null)
            {
                refEnemy.Health.TakeDamage(Mathf.CeilToInt(_damage * 0.5f)); // 50% damage
            }
        }
    }

    private void DespawnBullet()
    {
        PoolingManager.Despawn(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + _moveDirection * 1.5f);
    }
}
