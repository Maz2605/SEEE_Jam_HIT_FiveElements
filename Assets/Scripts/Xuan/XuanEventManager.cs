using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XuanEventManager : Singleton<XuanEventManager>
{
    public static Action<Enemy, float> EnemyTakeDamage;

    public static Action<Enemy, float> EnemyBeFrozen;

    public static Action<Enemy,float, float> ReduceSpeed;

    public static Action<int, float, string> SpawnEnemy;

    public static Func<Vector3,float,Enemy> GetEnemy;
}
