using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private int _damage = 50;
    [SerializeField] private float _lifetime = 2f;
    [SerializeField] private float _explosionRadius = 2f; 
    [SerializeField] private LayerMask _enemyLayer;

    [Header("Timing Settings")]
    [SerializeField] private float _spawnDelay = 0.1f;

    [Header("Explosion Settings")]
    [SerializeField] private GameObject _explosionPrefab;

    private float _spinSpeed = 800f;
    private Vector3 _moveDirection;
    private float _speed;
    private float _timer;
    private float _delayTimer;
    private bool _launched;

    public void Launch(Vector3 direction, float bulletSpeed)
    {
        _moveDirection = direction;
        _speed = bulletSpeed;
        _timer = 0f;
        _delayTimer = 0f;
        _launched = true;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!_launched) return;

        if (_delayTimer < _spawnDelay)
        {
            _delayTimer += Time.deltaTime;
            return;
        }

        transform.position += _moveDirection * _speed * Time.deltaTime;

        _timer += Time.deltaTime;
        if (_timer >= _lifetime)
        {
            PoolingManager.Despawn(gameObject);
        }

        BulletSpin();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        PlayExplosion(transform.position);

        DamageMainEnemy(other);
        

        DamageSplash(other);

        DespawnBullet();

    }
    private void PlayExplosion(Vector3 pos)
    {
        if (_explosionPrefab == null) return;

        GameObject explosion = PoolingManager.Spawn(_explosionPrefab, pos, Quaternion.identity);
    }

    private void DamageMainEnemy(Collider2D other)
    {
        Enemy refMain = other.GetComponent<Enemy>();
        if (refMain != null)
        {
            Debug.Log("Main enemy damaged");
            refMain.TakeDamage(refMain,_damage);
        }
    }

    private void DamageSplash(Collider2D mainTarget)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, _enemyLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == mainTarget.gameObject) continue; 

            Enemy refEnemy = hit.GetComponent<Enemy>();
            if (refEnemy != null)
            {
                Debug.Log("Splash enemy damaged");
                refEnemy.TakeDamage(refEnemy, Mathf.CeilToInt(_damage * 0.5f));
            }
        }
    }

    private void DespawnBullet()
    {
        _launched = false;
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

    private void BulletSpin()
    {
        transform.Rotate(Vector3.forward * _spinSpeed * Time.deltaTime);
    }
}
