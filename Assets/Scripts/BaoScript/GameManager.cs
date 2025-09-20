using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Idle,
    SpawnEnemies,
    WaitEnemies,
    SpawnBosses,
    WaitBosses,
    BetweenWaves,
    Finished
}

public class GameManager : MonoBehaviour
{
    public static System.Action OnWaveCompleted;
    public static System.Action OnAllWavesFinished;

    [Header("Level Settings")]
    [SerializeField] private LevelData levelData;
    [SerializeField] private EnemyManager enemyManager;

    private int _currentWaveIndex = -2;
    private GameState _state = GameState.Idle;

    private float _spawnDelayEnemy = 0.5f;
    private float _betweenWavesDelay = 2f;
    private float _timer;

    private Dictionary<string, EnemyStats> _enemyStatsCache;
    private Dictionary<string, EnemyStats> _bossStatsCache;


    private void Awake()
    {
        InitEnemyCache();
        InitBossCache();
    }

    private void Start()
    {
        GameEventPhong.AppearAward?.Invoke();
    }

    private void Update()
    {
        switch (_state)
        {
            case GameState.SpawnEnemies:
                SpawnEnemies();
                break;

            case GameState.WaitEnemies:
                if (enemyManager.AreAllEnemiesDead())
                {
                    WaveData wave = levelData.waves[_currentWaveIndex];

                    // Nếu wave này có boss -> sang SpawnBosses
                    if (wave.bossCount > 0 && wave.bossIDs.Count > 0)   
                    {
                        Debug.Log($"Wave {_currentWaveIndex + 1} có boss → chuyển sang SpawnBosses");
                        ChangeState(GameState.SpawnBosses);
                    }
                    else
                    {
                        Debug.Log($"Wave {_currentWaveIndex + 1} KHÔNG có boss → chuyển sang BetweenWaves");
                        // Nếu KHÔNG có boss -> sang BetweenWaves ngay
                        ChangeState(GameState.BetweenWaves);
                    }
                }
                break;

            case GameState.SpawnBosses:
                SpawnBosses();
                break;

            case GameState.WaitBosses:
                if (enemyManager.AreAllEnemiesDead())
                {
                    Debug.Log($"Boss wave {_currentWaveIndex + 1} đã xong → BetweenWaves");
                    ChangeState(GameState.BetweenWaves);
                }
                break;

            case GameState.BetweenWaves:
                break;

            case GameState.Finished:
                Debug.Log("✅ All waves finished!");
                break;
        }
    }

    #region SPAWN METHODS
    private void SpawnEnemies()
    {
        if (_currentWaveIndex >= levelData.waves.Count) return;

        WaveData wave = levelData.waves[_currentWaveIndex];

        foreach (string enemyId in wave.enemyIDs)
        {
            if (_enemyStatsCache.TryGetValue(enemyId, out var stats))
            {
                for (int i = 0; i < wave.enemyCount; i++)
                {
                    Transform point = enemyManager.GetRandomSpawnPoint();
                    enemyManager.SpawnEnemy(stats, point.position);
                }
            }
        }

        ChangeState(GameState.WaitEnemies);
    }

    private void SpawnBosses()
    {
        if (_currentWaveIndex >= levelData.waves.Count) return;

        WaveData wave = levelData.waves[_currentWaveIndex];

        foreach (string bossId in wave.bossIDs)
        {
            for (int i = 0; i < wave.bossCount; i++)
            {
                enemyManager.SpawnBoss(bossId);
            }
        }

        ChangeState(GameState.WaitBosses);
    }
    #endregion

    #region HELPERS
    private void ChangeState(GameState newState)
    {
        Debug.Log($"🔄 ChangeState: {_state} → {newState} (Wave {_currentWaveIndex + 1})");
        _state = newState;

        if (newState == GameState.BetweenWaves)
        {
            Debug.Log($"✅ Wave {_currentWaveIndex + 1} completed → Hiện popup Award");
            OnWaveCompleted?.Invoke();

            var awardController = GameObject.Find("Award"); 
            if (awardController != null && !awardController.activeSelf)
            {
                awardController.SetActive(true);
            }

            GameEventPhong.AppearAward?.Invoke();
        }

        if (newState == GameState.Finished)
        {
            OnAllWavesFinished?.Invoke();
        }
    }


    private void InitEnemyCache()
    {
        _enemyStatsCache = new Dictionary<string, EnemyStats>();
        foreach (var stats in enemyManager.GetEnemyData().enemyStatsList)
        {
            if (!_enemyStatsCache.ContainsKey(stats.idEnemy))
                _enemyStatsCache.Add(stats.idEnemy, stats);
        }
    }

    private void InitBossCache()
    {
        _bossStatsCache = new Dictionary<string, EnemyStats>();
        foreach (var stats in enemyManager.GetBossData().enemyStatsList)
        {
            if (!_bossStatsCache.ContainsKey(stats.idEnemy))
                _bossStatsCache.Add(stats.idEnemy, stats);
        }
    }

    public void ContinueNextWave()
    {
        _currentWaveIndex++;

        if (_currentWaveIndex < levelData.waves.Count)
        {
            GameEventPhong.DisAppearAward?.Invoke();
            StartCoroutine(WaitThenSpawn(3f)); // chờ 1 giây rồi spawn wave
        }
        else
        {
            Debug.Log("🏁 Đã hoàn thành toàn bộ waves!");
            ChangeState(GameState.Finished);
        }
    }

    private IEnumerator WaitThenSpawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeState(GameState.SpawnEnemies);
    }

    private void OnEnable()
    {
        StoreManager.OnStoreClosed += HandleStoreClosed;
    }

    private void OnDisable()
    {
        StoreManager.OnStoreClosed -= HandleStoreClosed;
    }

    private void HandleStoreClosed()
    {
        Debug.Log("▶️ Store đã đóng → Spawn enemy");
        ChangeState(GameState.SpawnEnemies);
    }
    #endregion
}
