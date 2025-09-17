using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image fillImage; 
    [SerializeField] private Gradient gradient;

    private float maxHP;
    private float currentHP;

    public void Initialize(float maxHealth)
    {
        maxHP = maxHealth;
        currentHP = maxHP;
        UpdateBar();
    }

    public void SetHealth(float health)
    {
        currentHP = Mathf.Clamp(health, 0, maxHP);
        UpdateBar();
    }

    private void UpdateBar()
    {
        float t = currentHP / maxHP;
        fillImage.fillAmount = t;
        fillImage.color = gradient.Evaluate(t); 
    }
}
