using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDuration : MonoBehaviour
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
        DataManager.Instance.PowerDuration *= 1.1f;
        DataManager.Instance.SavePowerDuration(DataManager.Instance.PowerDuration);
    }
    
}
