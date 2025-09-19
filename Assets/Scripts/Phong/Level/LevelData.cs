using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "GameData/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    [Header("Wave Settings")]
    public int waveCount = 1; 

    [Header("Enemy Settings")]
    public int enemyCount;                     
    public List<GameObject> enemyPrefabs;     

    [Header("Boss Settings")]
    public int bossCount;                      
    public List<GameObject> bossPrefabs;

    // so luong enemy spawn moi wave
    // so luong wave
    //loai enemy spawn ra
    //loai boss spawn ra
}