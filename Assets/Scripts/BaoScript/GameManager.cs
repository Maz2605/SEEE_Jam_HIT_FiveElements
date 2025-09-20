using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private LevelData levelData;       
    [SerializeField] private EnemyManager enemyManager;

    private int _currentWaveIndex = 0;

    private WaitForSeconds _spawnDelayEnemy = new WaitForSeconds(0.5f);
    private WaitForSeconds _betweenWavesDelay = new WaitForSeconds(2f);

    private Dictionary<string, EnemyStats> _enemyStatsCache;
    private Dictionary<string, EnemyStats> _bossStatsCache;

    private void Awake()
    {
        _enemyStatsCache = new Dictionary<string, EnemyStats>();
        foreach (var stats in enemyManager.GetEnemyData().enemyStatsList)
        {
            if (!_enemyStatsCache.ContainsKey(stats.idEnemy))
                _enemyStatsCache.Add(stats.idEnemy, stats);
        }

        _bossStatsCache = new Dictionary<string, EnemyStats>();
        foreach (var stats in enemyManager.GetBossData().enemyStatsList)
        {
            if (!_bossStatsCache.ContainsKey(stats.idEnemy))
                _bossStatsCache.Add(stats.idEnemy, stats);
        }
    }

    private void Start()
    {
        StartCoroutine(RunWaves());
    }

    private IEnumerator RunWaves()
    {
        while (_currentWaveIndex < levelData.waves.Count)
        {
            WaveData wave = levelData.waves[_currentWaveIndex];

            // --- Spawn Enemies ---
            yield return StartCoroutine(SpawnWaveEnemies(wave));

            // --- Spawn Bosses ---
            yield return StartCoroutine(SpawnWaveBosses(wave));

            _currentWaveIndex++;
            yield return _betweenWavesDelay;
        }
    }

    private IEnumerator SpawnWaveEnemies(WaveData wave)
    {
        foreach (string enemyId in wave.enemyIDs)
        {
            if (_enemyStatsCache.TryGetValue(enemyId, out var stats))
            {
                for (int i = 0; i < wave.enemyCount; i++)
                {
                    Transform point = enemyManager.GetRandomSpawnPoint();
                    enemyManager.SpawnEnemy(stats, point.position);
                    yield return _spawnDelayEnemy;
                }
            }
        }

        yield return new WaitUntil(() => enemyManager.AreAllEnemiesDead());
    }

    private IEnumerator SpawnWaveBosses(WaveData wave)
    {
        foreach (string bossId in wave.bossIDs)
        {
            for (int i = 0; i < wave.bossCount; i++)
            {
                enemyManager.SpawnBoss(bossId);
            }
        }

        yield return new WaitUntil(() => enemyManager.AreAllEnemiesDead());
    }


}
