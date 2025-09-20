using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    [Header("Enemy Settings")]
    public int enemyCount;                   
    public List<string> enemyIDs;    

    [Header("Boss Settings")]
    public int bossCount;                      
    public List<string> bossIDs;      
}

[CreateAssetMenu(fileName = "LevelData", menuName = "GameData/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    [Header("Wave Settings")]
    public List<WaveData> waves = new List<WaveData>(); 
}
