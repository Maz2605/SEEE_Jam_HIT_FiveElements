using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private HealthBarController healthBar;


    private float currentHealth;
    private bool isDead;


    private void Awake()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.Initialize(maxHealth);
    }


    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.Die();
        }
    }

    #region Getter / Setter
    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
    public bool GetIsDead() => isDead;

    public void SetCurrentHealth(float value)
    {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0 && !isDead)
            Die();
    }

    public void SetMaxHealth(float value)
    {
        maxHealth = Mathf.Max(1, value);
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        healthBar.Initialize(maxHealth);
    }

    public void Revive()
    {
        isDead = false;
        currentHealth = maxHealth;
        healthBar.Initialize(maxHealth);
    }
    #endregion
}
