using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    [Header("Player Prefabs")]
    [SerializeField] private GameObject normalPlayerPrefab;   // prefab level 0–2
    [SerializeField] private GameObject superHappyPrefab;     // prefab level 3

    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnPoint;

    private GameObject _currentPlayer;
    private int currentLevel = -1;

    private void Start()
    {
        //chi spawn play khi bat dau level
        //SpawnNormalPlayer();
        currentLevel = 0;
    }

    public void LevelUp(int newLevel)
    {
        if (newLevel == currentLevel) return;
        currentLevel = newLevel;

        if (newLevel < 3)
        {
            if (_currentPlayer != null && _currentPlayer.name.Contains("SuperHappy"))
            {
                ReplaceWithNormal(newLevel);
            }
            else
            {
                Animator anim = _currentPlayer.GetComponent<Animator>();
                if (anim != null)
                    anim.SetInteger("EmotionLevel", newLevel);
            }
        }
        else if (newLevel == 3)
        {
            ReplaceWithSuperHappy();
        }
    }

    public void SpawnNormalPlayer()
    {
        _currentPlayer = PoolingManager.Spawn(
            normalPlayerPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }

    private void ReplaceWithNormal(int newLevel)
    {
        if (_currentPlayer != null)
            PoolingManager.Despawn(_currentPlayer);

        _currentPlayer = PoolingManager.Spawn(
            normalPlayerPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        Animator anim = _currentPlayer.GetComponent<Animator>();
        if (anim != null)
            anim.SetInteger("EmotionLevel", newLevel);
    }

    private void ReplaceWithSuperHappy()
    {
        if (_currentPlayer != null)
            PoolingManager.Despawn(_currentPlayer);

        _currentPlayer = PoolingManager.Spawn(
            superHappyPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
    public void RemovePlayer()
    {
        if(_currentPlayer != null)
        {
            PoolingManager.Despawn(_currentPlayer);
            _currentPlayer = null;
        }
    }
}
