using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemys;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _enemyPerant;

    [Header("Spawn Points")]
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Transform _bossSpawnPoint;

    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private EnemyData _bossData;
    private void Start()
    {
        SpawnEnemy(20, 30f, "orc");
        SpawnEnemy(15, 30f, "slime1");
        SpawnBoss(30f, "magic");
    }
    //
    public void SpawnEnemy(int number, float timeWave, string idEnemy)
    {
        float time = timeWave / number;
        StartCoroutine(SpawnEnemyRoutine(number, time, idEnemy));
    }
    public void SpawnBoss(float timeWare,string idEnemy)
    {
        float time = timeWare/2;
        StartCoroutine(SpawnBossRoutine(time, idEnemy));
    }
    public void AttackOneEnemy(float damage)
    {

    }


    //
    IEnumerator SpawnEnemyRoutine(int number, float time, string idEnemy)
    {
        int safe = 0;
        EnemyStats data = _enemyData.enemyStatsList.Find(x => x.idEnemy == idEnemy);
        if (data == null) yield break;
        Debug.Log("Data is not null");
        while (true)
        {
            SpwanRandomPoints(data);
            yield return new WaitForSeconds(time);
            safe++;
            if(safe > number) break;
        }
    }
    IEnumerator SpawnBossRoutine(float time, string idEnemy)
    {
        yield return new WaitForSeconds(time);
        EnemyStats data = _bossData.enemyStatsList.Find(x => x.idEnemy == idEnemy);
        if (data == null) yield break;
        SpawnEnemy(data, _bossSpawnPoint.position);
    }

    public void SpwanRandomPoints(EnemyStats data)
    {
        int randomIndex = Random.Range(0, _spawnPoints.Count);
        Transform spawnPoint = _spawnPoints[randomIndex];

        SpawnEnemy(data, spawnPoint.position);
    }
    public void SpawnEnemy(EnemyStats data, Vector3 pos)
    {
        Enemy newEnemy = PoolingManager.Spawn(_enemyPrefab, pos, Quaternion.identity, _enemyPerant);
        newEnemy.InitState(data);
        _enemys.Add(newEnemy);
    }

    public void GetEnemy()
    {

    }
}
