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
    public string idEnemy;
    public bool isFar;
    // Enemy State
    public float health;
    public float speed;
    public float damage;
    public float speedAttack;
    public float scoreValue;
    public int countCoin;
    public EnemyEmotion emotion;
    // Enemy Animation
    public RuntimeAnimatorController animator;
}

public enum EnemyEmotion
{
    Normal,
    Angry,
    Happy,
}

public enum EnemyType
{
    Enemy,
    Village,
}
