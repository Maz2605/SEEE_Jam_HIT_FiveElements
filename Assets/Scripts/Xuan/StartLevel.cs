using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private GameObject _map;
    [SerializeField] private PlayerLevelSystem _playerLevelSystem;
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject enemyListSpawn;
    [SerializeField] private GameObject _uiGamePlay;

    private void Start()
    {
        XuanEventManager.OnStartLevel += OnLevel;
        XuanEventManager.OnBackLevel += BackLevel;
    }
    private void OnDestroy()
    {
        XuanEventManager.OnStartLevel -= OnLevel;
        XuanEventManager.OnBackLevel -= BackLevel;
    }

    public void OnLevel(int index)
    { 
        _map.SetActive(true);
        _uiGamePlay.SetActive(true);
        GameEventPhong.AppearAward?.Invoke(); // Khoi tao UI choise Buff
        _playerLevelSystem.SpawnNormalPlayer();
        obj.SetActive(true);
        enemyListSpawn.SetActive(true);
        GameManager.Instance.CurrentWaveIndex = -1;
    }

    public void BackLevel()
    {
        _map.SetActive(false);
        _uiGamePlay.SetActive(false);
        obj.SetActive(false);
        enemyListSpawn.SetActive(false);
        EnemyManager.Instance.RemoveAllEnemy();
        _playerLevelSystem.RemovePlayer();

        HeroFlight heroFlight = FindObjectOfType<HeroFlight>();
        if (heroFlight != null)
        {
            PoolingManager.Despawn(heroFlight.gameObject);
        }
        HeroKnight heroKnight = FindObjectOfType<HeroKnight>();
        if (heroKnight != null)
        {
            PoolingManager.Despawn(heroKnight.gameObject);
        }
    }
}
