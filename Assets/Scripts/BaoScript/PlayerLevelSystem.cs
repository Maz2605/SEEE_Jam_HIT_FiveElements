using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerPrefabs;
    [SerializeField] private GameObject _currentPlayer;
    private int currentLevel = 0;

    [Header("Spawn Settings")]
    [SerializeField] private Transform _spawnPoint;
    private void Start()
    {
        if (_currentPlayer == null || !_currentPlayer.activeInHierarchy)
        {
            SpawnPlayer(0); 
        }
    }

    public void LevelUp(int newLevel)
    {
        if (newLevel < 0 || newLevel >= _playerPrefabs.Length) return;
        if (newLevel == currentLevel) return;

        currentLevel = newLevel;
        SpawnPlayer(currentLevel);
    }

    private void SpawnPlayer(int level)
    {
        Vector3 spawnPos = _spawnPoint != null ? _spawnPoint.position : Vector3.zero;
        Quaternion spawnRot = _spawnPoint != null ? _spawnPoint.rotation : Quaternion.identity;

        if (_currentPlayer != null)
        {
            PoolingManager.Despawn(_currentPlayer);
        }

        _currentPlayer = PoolingManager.Spawn(_playerPrefabs[level], spawnPos, spawnRot);

        Debug.Log($"[PlayerLevelSystem] Spawned {_playerPrefabs[level].name} tại {spawnPos}");
    }


}
