using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 2;
    private int _currentHealth;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            if (EmotionBar.Instance != null)
                EmotionBar.Instance.AddEmotion(20f); 

            gameObject.SetActive(false);
        }
    }

}
