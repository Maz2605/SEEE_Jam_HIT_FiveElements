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
    }
    private void OnDestroy()
    {
        XuanEventManager.OnStartLevel -= OnLevel;
    }

    public void OnLevel(int index)
    { 
        _map.SetActive(true);
        _uiGamePlay.SetActive(true);
        GameEventPhong.AppearAward?.Invoke(); // Khoi tao UI choise Buff
        _playerLevelSystem.SpawnNormalPlayer();
        obj.SetActive(true);
        enemyListSpawn.SetActive(true);
    }

    public void BackLevel()
    {
        _map.SetActive(false);
        _uiGamePlay.SetActive(false);
        obj.SetActive(false);
        enemyListSpawn.SetActive(false);
    }
}
