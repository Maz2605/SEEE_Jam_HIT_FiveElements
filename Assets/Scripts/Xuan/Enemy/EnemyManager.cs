using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemys;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _enemyPerant;
    public void SpawnEnemy(EnemyStats data)
    {
        Enemy newEnemy = PoolingManager.Spawn(_enemyPrefab, Vector3.zero, Quaternion.identity, _enemyPerant);
        _enemys.Add(newEnemy);
    }
}
