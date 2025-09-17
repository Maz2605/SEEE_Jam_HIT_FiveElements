using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandlerSkill2 : MonoBehaviour
{
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float tickInterval = 1f; // mỗi 1 giây gây sát thương
    [SerializeField] private float _timer = 3f;
    [SerializeField] private float _upgradeDamage = 1f;
    [SerializeField] private float _upgradeTimer = 0.5f;
    private HashSet<Collider2D> enemiesInRange = new HashSet<Collider2D>();

    private void Awake()
    {
        _damage = DataManager.Instance.DamageSkill2;
        _timer = DataManager.Instance.TimerSkill2;
    }

    private void OnEnable()
    {
        GameEventPhong.UpgradeSkill2 += UpgradeSkill;
    }

    private void OnDisable()
    {
        GameEventPhong.UpgradeSkill2 -= UpgradeSkill;
    }

    private void UpgradeSkill()
    {
        _damage += _upgradeDamage;
        _timer -= _upgradeTimer;
        DataManager.Instance.SaveDataDamageSkill2(_damage);
        DataManager.Instance.SaveTimerSkill2(_timer);
    }
    
    private void Start()
    {
        // tự hủy sau 5 giây
        DOVirtual.DelayedCall(_timer, () =>
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        });

        // bắt đầu gây damage theo interval
        StartCoroutine(DamageOverTime());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other);
            XuanEventManager.EnemyTakeDamage(other.gameObject.GetComponent<Enemy>(), _damage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other);
        }
    }

    private IEnumerator DamageOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickInterval);

            foreach (var enemy in enemiesInRange)
            {
                if (enemy != null) 
                {
                    
                    // Enemy take damage
                }
            }
        }
    }

    private void GetDamage()
    {
        
    }
}