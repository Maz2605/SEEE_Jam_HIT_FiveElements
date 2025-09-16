using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerPrefabs;
    [SerializeField] private GameObject _currentPlayer;
    private int currentLevel = 0;

    private void Start()
    {
        if (_currentPlayer == null && _playerPrefabs.Length > 0)
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
        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;

        if (_currentPlayer != null)
        {
            spawnPos = _currentPlayer.transform.position;
            spawnRot = _currentPlayer.transform.rotation;
            PoolingManager.Despawn(_currentPlayer);
        }

        _currentPlayer = PoolingManager.Spawn(_playerPrefabs[level], spawnPos, spawnRot);
    }
}
