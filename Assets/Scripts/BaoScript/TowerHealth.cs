using System;
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

    private void Start()
    {
        _maxHealth = DataManager.Instance.TowerHealth;
        _healthBar.Initialize(_maxHealth);
        _currentHealth = _maxHealth;
    
    }

    public void SetCurrentHealth(float value)
    {
        _currentHealth += value;
        
        if(_currentHealth >= _maxHealth) _currentHealth = _maxHealth;
        
        _healthBar.SetHealth(_currentHealth);
    }

    public void SetMaxHealth(float value)
    {
        _maxHealth = value;
    }
    #endregion

    #region Unity Lifecycle
    public void InitTower()
    {
        _currentHealth = _maxHealth;

        if (_healthBar != null)
            _healthBar.Initialize(_maxHealth);
        else
        {
            Debug.LogWarning("No health Bar found!");
        }
        
       
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
            UIWinLose.Instance.ShowLose();
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

   
    #endregion
}
