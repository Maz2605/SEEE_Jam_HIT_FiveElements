using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandlerSkill2 : MonoBehaviour
{
    
    [SerializeField] private float _timer = 1f;
   
    [SerializeField] private float _upgradeTimer = 0.5f;
    

    private void Awake()
    {
        
        _timer = DataManager.Instance.TimerSkill2;
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Frezon Enemy other
            collision.gameObject.GetComponent<Enemy>().Frozen(_timer);
        }
    }
}