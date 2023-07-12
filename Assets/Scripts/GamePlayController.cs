using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using View;

public class GamePlayController : MonoBehaviour
{
    /// <summary>
    /// Game play process
    /// Start level
    /// CreatePp player, field, enemy
    /// Game state
    /// HUD
    /// Check end game
    /// </summary>
    
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _playerAndEnemyRoot;
    [SerializeField] private FieldController _fieldController;
    [FormerlySerializedAs("_yardView")] [SerializeField] private YardManager yardManager;
    
    private PlayerController _playerController;
    private List<EnemyView> _enemyViews = new List<EnemyView>();
    private GameState _gameState;

    // todo move to config
    private readonly int _minEnemyCount = 5;
    private readonly int _maxEnemyCount = 15;
    public void StartGame()
    {
        _gameState = GameState.Loading;
        
        CreatePlayer();
        CreateField();
        CreateEnemy();
        
        _gameState = GameState.Play;
    }
    
    private void CreatePlayer()
    {
        var player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity, _playerAndEnemyRoot);
        _playerController = player.GetComponent<PlayerController>();
        _playerController.SetData();
    }

    private void CreateField()
    {
        _fieldController.CreateField(Camera.main, _playerController);
        yardManager.SetData(EnemyDelivered);
    }

    private void EnemyDelivered(EnemyView view)
    {
        if (_enemyViews.Contains(view))
        {
            _enemyViews.Remove(view);
            _playerController.EnemyDelivered(view);
        }
    }

    private void CreateEnemy()
    {
        var enemyCount = Random.Range(_minEnemyCount, _maxEnemyCount);
        for (var i = 0; i < enemyCount; i++)
        {
            var enemy = Instantiate(_enemyPrefab, Vector3.zero, Quaternion.identity, _playerAndEnemyRoot);
            var enemyView = enemy.GetComponent<EnemyView>();
            enemyView.SetData(i, _fieldController.GetSpawnPosition());
            
            _enemyViews.Add(enemyView);
        }
        
        Debug.Log($"enemyCount {enemyCount}");
    }
    
}

public enum GameState
{
    Loading = 0,
    Play,
    Pause
}

