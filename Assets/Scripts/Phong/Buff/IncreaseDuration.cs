using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDuration : Buff
{
    public float duration;

    private void OnEnable()
    {
        GameEventPhong.IncreaseDuration += IncreaseDurationBuff;
    }

    private void OnDisable()
    {
        GameEventPhong.IncreaseDuration -= IncreaseDurationBuff;
    }

    private void IncreaseDurationBuff()
    {
        
    }
    
}
