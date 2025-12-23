using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image frontFill;   
    private float maxHP;
    private float currentHP;

    [SerializeField] private float tweenSpeed = 0.3f;

    public void Initialize(float maxHealth)
    {
        maxHP = maxHealth;
        currentHP = maxHP;
        frontFill.fillAmount = 1f; 
    }

    public void SetHealth(float health)
    {
        currentHP = Mathf.Clamp(health, 0, maxHP);
        float t = currentHP / maxHP;

        frontFill.DOFillAmount(t, tweenSpeed).SetEase(Ease.OutCubic);
    }
}
