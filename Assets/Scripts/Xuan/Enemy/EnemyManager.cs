using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public int EnemyCount => _enemys.Count;


    [SerializeField] private List<Enemy> _enemys;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _enemyPerant;

    [Header("Spawn Points")]
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Transform _bossSpawnPoint;

    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private EnemyData _bossData;

    [Header("Boss Prefab")]
    [SerializeField] private DarkMagic _darkMagic;
    [SerializeField] private MedusaBoss _medusa;
    [SerializeField] private Golem _golem;
    [SerializeField] private FlyDemon _flydemon;
    [SerializeField] private Stayr _stayr;

    [Header("Animation")]
    [SerializeField] private RuntimeAnimatorController _boy;
    [SerializeField] private RuntimeAnimatorController _gird;
    [SerializeField] private GameObject _effectExplosion;

    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform _coinEnemy;
    public Transform CoinEnemy => _coinEnemy;
    public Coin GetCoin => _coinPrefab;
    private Enemy _currentEnemy;
    private Vector3 _posWall;

    private void Start()
    {
        XuanEventManager.GetEnemy += GetEnemyByDistance;
    }
    private void OnDestroy()
    {
        XuanEventManager.GetEnemy -= GetEnemyByDistance;
    }
    public void SpawnBoss(string bossId)
    {
        switch (bossId.ToLower())
        {
            case "golem":
                SpawnGolem();
                break;
            case "medusa":
                SpawnMedusa();
                break;
            case "flydemon":
                SpawnFlyDemon();
                break;
            default:
                Debug.LogWarning($"❌ Don't have boss is ID {bossId}");
                break;
        }
    }

    public void SpawnBoss(float timeWare,string idEnemy)
    {
        float time = timeWare/2;
        
    }

    public void SpawnDarkMagic()
    {
        DarkMagic darkMagic = PoolingManager.Spawn(this._darkMagic, _bossSpawnPoint.position, Quaternion.identity, _enemyPerant);
        darkMagic.InitState(_bossData.enemyStatsList.Find(x => x.idEnemy == "dark"));
        _enemys.Add(darkMagic);
    }
    public void SpawnMedusa()
    {
        MedusaBoss medusa = PoolingManager.Spawn(this._medusa, _bossSpawnPoint.position, Quaternion.identity, _enemyPerant);
        medusa.InitState(_bossData.enemyStatsList.Find(x => x.idEnemy == "medusa"));
        _enemys.Add(medusa);
    }

    public void SpawnGolem()
    {
        Golem golem = PoolingManager.Spawn(this._golem, _bossSpawnPoint.position, Quaternion.identity, _enemyPerant);
        golem.InitState(_bossData.enemyStatsList.Find(x => x.idEnemy == "golem"));
        _enemys.Add(golem);
    }
    public void SpawnFlyDemon()
    {
        FlyDemon flyDemon = PoolingManager.Spawn(this._flydemon, _bossSpawnPoint.position, Quaternion.identity, _enemyPerant);
        flyDemon.InitState(_bossData.enemyStatsList.Find(x => x.idEnemy == "flydemon"));
        _enemys.Add(flyDemon);
    }

    public void SpawnEnemy(EnemyStats data, Vector3 pos)
    {
        Enemy newEnemy = PoolingManager.Spawn(_enemyPrefab, pos, Quaternion.identity, _enemyPerant);
        newEnemy.InitState(data);
        _enemys.Add(newEnemy);
    }

    public Enemy GetEnemyByDistance(Vector3 posWall, float range)
    {
        Enemy nearest = null;
        float minDist = Mathf.Infinity;

        foreach (Enemy e in _enemys)
        {
            if (e == null || !e.gameObject.activeInHierarchy) continue;
            if (e.GetEnemyType != EnemyType.Enemy) continue; 

            float dist = Vector2.Distance(e.transform.position, posWall);          
            if (dist <= range && dist < minDist)
            {
                minDist = dist;
                nearest = e;
            }
        }
        return nearest;
    }
    public RuntimeAnimatorController RandomVillage()
    {
        int random = Random.Range(0, 2);
        if (random == 1)
        {
            return _boy;
        }
        else
        {
            return _gird;
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemys.Remove(enemy);
    }

    public void SpawnExplosionInEnemy(Enemy enemy)
    {
        GameObject explosion = PoolingManager.Spawn(_effectExplosion, enemy.transform.position, Quaternion.identity);
        DOVirtual.DelayedCall(1f, () =>
        {
            PoolingManager.Despawn(explosion);
        });
    }

    public EnemyData GetEnemyData() => _enemyData;
    public EnemyData GetBossData() => _bossData;

    public Transform GetRandomSpawnPoint()
    {
        if (_spawnPoints.Count == 0) return null;
        int rand = Random.Range(0, _spawnPoints.Count);
        return _spawnPoints[rand];
    }

    public bool AreAllEnemiesDead()
    {
        for (int i = _enemys.Count - 1; i >= 0; i--)
        {
            if (_enemys[i] == null || !_enemys[i].gameObject.activeInHierarchy)
            {
                _enemys.RemoveAt(i);
            }
        }

        return _enemys.Count == 0;
    }

    public Transform GetBossSpawnPoint()
    {
        return _bossSpawnPoint;
    }
}
