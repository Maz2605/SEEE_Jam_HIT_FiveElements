using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.iOS;

public enum GameState
{
    Idle,
    SpawnEnemies,
    WaitEnemies,
    SpawnBosses,
    WaitBosses,
    BetweenWaves,
    Finished,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    public static System.Action OnWaveCompleted;
    public static System.Action OnAllWavesFinished;

    [Header("Level Settings")]
    [SerializeField] private List<LevelData> _levelData;
    [SerializeField] private EnemyManager enemyManager;

    [Header("UI")]
    [SerializeField] private TMP_Text _waveText;   
    [SerializeField] private float _waveTextDuration = 2f;

    [Header("Hero Settings")]
    private GameObject _currentHero;

    private int _currentWaveIndex = -1;
    private int _currentLevelIndex = -1;
   [SerializeField] private GameState _state = GameState.Idle;

    public void ResetState()
    {
        _state = GameState.Idle;
    }

    private Dictionary<string, EnemyStats> _enemyStatsCache;
    private Dictionary<string, EnemyStats> _bossStatsCache;

    public int CurrentWaveIndex
    {
               get { return _currentWaveIndex; }
                set { _currentWaveIndex = value; }
    }
    public int CurrentLevel
    {
        get => _currentLevelIndex;
        set => _currentLevelIndex = value;
    }
    private void Awake()
    {
        InitEnemyCache();
        InitBossCache();
    }

    private void Start()
    {
        //Tat di ko chi goi khi nhan nut level
        //GameEventPhong.AppearAward?.Invoke();   
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
                    WaveData wave = _levelData[CurrentWaveIndex].waves[_currentWaveIndex];

                    // Nếu wave này có boss -> sang SpawnBosses
                    if (wave.bosses != null && wave.bosses.Count > 0)
                    {
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
        if (_currentWaveIndex < 0 || _currentWaveIndex >= _levelData[_currentLevelIndex].waves.Count) return;

        WaveData wave = _levelData[_currentLevelIndex].waves[_currentWaveIndex];
        bool hasEnemies = wave.enemies != null && wave.enemies.Count > 0;
        bool hasBosses = wave.bosses != null && wave.bosses.Count > 0;

        // Nếu wave chỉ có boss thì nhảy sang spawn boss luôn
        if (!hasEnemies && hasBosses)
        {
            ChangeState(GameState.SpawnBosses);
            return;
        }

        if (hasEnemies)
        {
            if (!hasBosses) ShowWaveText($"Wave {_currentWaveIndex + 1}");

            foreach (var entry in wave.enemies)
            {
                if (_enemyStatsCache.TryGetValue(entry.enemyID, out var stats))
                {
                    for (int i = 0; i < entry.count; i++)
                    {
                        Transform point = enemyManager.GetRandomSpawnPoint();
                        enemyManager.SpawnEnemy(stats, point.position);
                    }
                }
                else
                {
                    Debug.LogWarning($"EnemyID '{entry.enemyID}' không có trong cache.");
                }
            }
        }

        ChangeState(GameState.WaitEnemies);
    }

    private void SpawnBosses()
    {
        if (_currentWaveIndex < 0 || _currentWaveIndex >= _levelData[_currentLevelIndex].waves.Count) return;

        WaveData wave = _levelData[_currentLevelIndex].waves[_currentWaveIndex];
        if (wave.bosses == null || wave.bosses.Count == 0)
        {
            ChangeState(GameState.BetweenWaves);
            return;
        }

        ShowWaveText("Boss Wave!");

        foreach (var entry in wave.bosses)
        {
            if (_bossStatsCache.TryGetValue(entry.enemyID, out var _))
            {
                for (int i = 0; i < entry.count; i++)
                {
                    enemyManager.SpawnBoss(entry.enemyID);
                }
            }
            else
            {
                Debug.LogWarning($"BossID '{entry.enemyID}' không có trong cache.");
            }
        }

        ChangeState(GameState.WaitBosses);
    }

    #endregion
    #region UI HELPERS
    private void ShowWaveText(string text)
    {
        if (_waveText == null) return;

        _waveText.text = text;
        _waveText.gameObject.SetActive(true);
        _waveText.alpha = 0;

        // hiệu ứng fade in/out
        _waveText.DOFade(1f, 0.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(_waveTextDuration, () =>
            {
                _waveText.DOFade(0f, 0.5f);
            });
        });
    }
    #endregion
    #region HELPERS
    private void ChangeState(GameState newState)
    {
        Debug.Log($"🔄 ChangeState: {_state} → {newState} (Wave {_currentWaveIndex + 1})");
        _state = newState;
        if(_currentHero != null)
        {
            GameObject hero = _currentHero;
            Destroy(hero);
            _currentHero = null;
        }
        if (newState == GameState.BetweenWaves)
        {
            if(_currentWaveIndex == _levelData[_currentLevelIndex].waves.Count - 1)
            {
                Debug.Log("🏁 Đã hoàn thành toàn bộ waves!");
                ChangeState(GameState.Finished);
                return;
            }

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
            DOVirtual.DelayedCall(1f, () =>
            {
                UIWinLose.Instance.ShowWin();
            });
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

        if (_currentWaveIndex < _levelData[_currentLevelIndex].waves.Count)
        {
            GameEventPhong.DisAppearAward?.Invoke();
            //StartCoroutine(WaitThenSpawn(3f)); // chờ 1 giây rồi spawn wave
        }
        else
        {
            Debug.Log("🏁 Đã hoàn thành toàn bộ waves!");
            ChangeState(GameState.Finished);
        }

        DOVirtual.DelayedCall(6f, () =>
        {
            GetHero();
        });
    }

    private void GetHero()
    {
        HeroFlight hero = FindObjectOfType<HeroFlight>();
        if(hero != null)
        {
            _currentHero = hero.gameObject;
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

    public void GameOver()
    {

    }
    #endregion
}
