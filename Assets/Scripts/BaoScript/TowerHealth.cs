using UnityEngine;

public class TowerHealth : Singleton<TowerHealth>
{
    [Header("Tower Settings")]
    [SerializeField] private float _currentHealth = 100f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private HealthBarController _healthBar;

    //private float _currentHealth;
    private bool _isDead;

    #region Properties
    public bool IsDead => _isDead;
    #endregion

    #region Getter / Setter
    public float GetCurrentHealth() => _currentHealth;
    public float GetMaxHealth() => _maxHealth;
    public bool GetIsDead() => _isDead;

    public void SetCurrentHealth(float value)
    {
        _currentHealth += value;
    }

    public void SetMaxHealth(float value)
    {
        _maxHealth += value;
    }
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        _currentHealth = _maxHealth;

        if (_healthBar != null)
            _healthBar.Initialize(_maxHealth);
    }
    #endregion

    #region Health Logic
    public void TakeDamage(float damage)
    {
        Debug.Log($"Tower takes {damage} damage.");
        if (_isDead) return;

        _currentHealth -= damage;

        if (_healthBar != null)
            _healthBar.SetHealth(_currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void ReceiveDamage(float damage)
    {
        TakeDamage(damage);
    }
    #endregion

    #region Death Logic
    private void Die()
    {
        if (_isDead) return;
        _isDead = true;

        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.Die();
        }
    }

    public void Revive()
    {
        _isDead = false;
        _currentHealth = _maxHealth;

        if (_healthBar != null)
            _healthBar.Initialize(_maxHealth);
    }
    #endregion
}
