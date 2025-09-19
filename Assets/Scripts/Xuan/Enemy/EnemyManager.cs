using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
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
    public Coin GetCoin => _coinPrefab;

    private void Start()
    {
        SpawnEnemy(4, 5f, "fire");
        //SpawnEnemy(2, 30f, "light");
        SpawnEnemy(2, 30f, "magic");
        SpawnEnemy(3, 30f, "knight1");
        //SpawnEnemy(3, 30f, "knight2");
        //SpawnEnemy(3, 30f, "knight3");
        //SpawnDarkMagic();
        //SpawnMedusa();
        //SpawnGolem();

        XuanEventManager.SpawnEnemy += SpawnEnemy;
        XuanEventManager.GetEnemy += GetEnemyByDistance;
    }
    //
    
    private void OnDestroy()
    {
        XuanEventManager.SpawnEnemy -= SpawnEnemy;
        XuanEventManager.GetEnemy -= GetEnemyByDistance;
    }
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
    public void SpawnDarkMagic()
    {
        DarkMagic darkMagic = PoolingManager.Spawn(this._darkMagic, _bossSpawnPoint.position, Quaternion.identity, _enemyPerant);
        darkMagic.InitState(_bossData.enemyStatsList.Find(x => x.idEnemy == "dark"));
        _enemys.Add(darkMagic);
    }
    public void SpawnMedusa()
    {
        MedusaBoss medusa = PoolingManager.Spawn(this._medusa, new Vector3(18f,0f,0f), Quaternion.identity, _enemyPerant);
        medusa.InitState(_bossData.enemyStatsList.Find(x => x.idEnemy == "medusa"));
        _enemys.Add(medusa);
    }

    public void SpawnGolem()
    {
        Golem golem = PoolingManager.Spawn(this._golem, _bossSpawnPoint.position, Quaternion.identity, _enemyPerant);
        golem.InitState(_bossData.enemyStatsList.Find(x => x.idEnemy == "golem"));
        _enemys.Add(_golem);
    }

    IEnumerator SpawnEnemyRoutine(int number, float time, string idEnemy)
    {
        int safe = 0;
        EnemyStats data = _enemyData.enemyStatsList.Find(x => x.idEnemy == idEnemy);
        if (data == null) yield break;
        
        while (true)
        {
            SpwanRandomPoints(data);
            yield return new WaitForSeconds(time);
            safe++;
            if(safe >= number) yield break;
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

    public Enemy GetEnemyByDistance(Vector3 posWall)
    {
        Enemy enemy = null;
        float minDis = 100f;

        foreach(Enemy e in _enemys)
        {
            if(e.GetEnemyType != EnemyType.Enemy) continue;

            Vector2 enemyPos = new Vector2(e.transform.position.x, e.transform.position.y);
            Vector2 poswall = new Vector2(posWall.x, posWall.y);
            float dis = Vector2.Distance(enemyPos, poswall);
            if(dis < minDis)
            {
                minDis = dis;
                enemy = e;
            }
        }

        return enemy;
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
}
