using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifetime = 2f;

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
        if (other.CompareTag("Enemy"))
        {
            PoolingManager.Despawn(gameObject);
        }
    }
}
