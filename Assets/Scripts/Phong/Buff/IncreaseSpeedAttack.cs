using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedAttack : Buff
{
    public float speed;

    private void OnEnable()
    {
        GameEventPhong.IncreaseSpeedAttack += IncreaseSpeedBullet;
    }

    private void OnDisable()
    {
        GameEventPhong.IncreaseSpeedAttack += IncreaseSpeedBullet;
    }

    private void IncreaseSpeedBullet()
    {
        PlayerController.Instance.SetBulletSpeed(speed);
    }
    
}
