using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandlerSkill2 : MonoBehaviour
{
    
    [SerializeField] private float _timer = 3f;
   
    [SerializeField] private float _upgradeTimer = 0.5f;
    private HashSet<Collider2D> enemiesInRange = new HashSet<Collider2D>();

    private void Awake()
    {
        
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
      
        _timer += _upgradeTimer;
        
        DataManager.Instance.SaveTimerSkill2(_timer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Frezon Enemy
            
        }
    }
}