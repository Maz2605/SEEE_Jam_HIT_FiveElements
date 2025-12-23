using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyEntry
{
    public string enemyID;   // ID phải khớp với EnemyStats trong EnemyData
    public int count;        // Số lượng spawn
}

[System.Serializable]
public class WaveData
{
    [Header("Enemy Settings")]
    public List<EnemyEntry> enemies = new List<EnemyEntry>();

    [Header("Boss Settings")]
    public List<EnemyEntry> bosses = new List<EnemyEntry>();
}

[CreateAssetMenu(fileName = "LevelData", menuName = "GameData/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    [Header("Wave Settings")]
    public List<WaveData> waves = new List<WaveData>();
}
