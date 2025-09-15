using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public List<EnemyStats> enemyStatsList;
}
[System.Serializable]
public class EnemyStats
{
    // Enemy State
    public float health;
    public float speed;
    public int damage;
    public int scoreValue;
}
