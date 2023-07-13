using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using View;
using Random = UnityEngine.Random;

public class GamePlayController : MonoBehaviour
{
    /// <summary>
    /// Game play process
    /// Start level
    /// CreatePp player, field, enemy
    /// Game state
    /// Check end game
    /// </summary>

    public Action OnGameEnd;

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _playerAndEnemyRoot;
    [SerializeField] private FieldController _fieldController;
    [SerializeField] private YardManager _yardManager;
    [SerializeField] private HUDManager _hudManager;
    
    private PlayerController _playerController;
    private List<EnemyView> _enemyViews = new List<EnemyView>();
    private GameState _gameState;
    private int _enemiesCount;
    private int _enemiesDeliveredCount;

    // todo move to config
    private readonly int _minEnemyCount = 5;
    private readonly int _maxEnemyCount = 15;
    public void StartGame()
    {
        _gameState = GameState.Loading;
        
        CreatePlayer();
        CreateField();
        CreateEnemy();
        SetHUDManager();
        
        _gameState = GameState.Play;
    }

    private void SetHUDManager()
    {
        _hudManager.SetData(_enemiesCount);
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
        _yardManager.SetData(EnemyDelivered);
    }

    private void CreateEnemy()
    {
        _enemiesDeliveredCount = 0;
        _enemiesCount = Random.Range(_minEnemyCount, _maxEnemyCount);
        for (var i = 0; i < _enemiesCount; i++)
        {
            var enemy = Instantiate(_enemyPrefab, Vector3.zero, Quaternion.identity, _playerAndEnemyRoot);
            var enemyView = enemy.GetComponent<EnemyView>();
            enemyView.SetData(i, _fieldController.GetSpawnPosition());
            
            _enemyViews.Add(enemyView);
        }
    }
    
    private void EnemyDelivered(EnemyView view)
    {
        if (_enemyViews.Contains(view))
        {
            _enemyViews.Remove(view);
            _playerController.EnemyDelivered(view);
            
            AddScore();
        }
    }

    private void AddScore()
    {
        _enemiesDeliveredCount++;
        _hudManager.UpdateScore(_enemiesDeliveredCount);

        CheckEndGame();
    }

    private void CheckEndGame()
    {
        if (_enemiesCount == _enemiesDeliveredCount)
        {
            OnGameEnd?.Invoke();
            _gameState = GameState.Pause;
        }
    }
}
