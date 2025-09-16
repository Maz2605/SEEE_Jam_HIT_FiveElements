using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XuanEventManager : Singleton<XuanEventManager>
{
    public static Action<Enemy, float> EnemyTakeDamage;

    public static Action<Enemy,float, float> ReduceSpeed;

    public static Func<Enemy> GetEnemy;

    public static Action<int, float, string> SpawnEnemy;
}
