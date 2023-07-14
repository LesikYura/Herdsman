using System;
using System.Collections.Generic;
using CoreSystem;
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

    public Action onGameEnd;

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _playerAndEnemyRoot;
    [SerializeField] private FieldController _fieldController;
    [SerializeField] private YardManager _yardManager;
    [SerializeField] private HUDManager _hudManager;
    [SerializeField] private GamePlayConfig _config;

    private EnemyAI _enemyAI;
    private PlayerView _playerView;
    private ObjectPool _objectPool;
    private EnemyMaker _enemyMaker;
    private List<EnemyView> _enemyViews = new List<EnemyView>();
    private int _enemiesDeliveredCount;

    private GameState _gameState;
    
    public void StartGame()
    {
        _gameState = GameState.Loading;
        _objectPool = GetObjectPool();
        
        CreatePlayer();
        CreateField();
        CreateEnemy();
        CreateAI();
        SetHUDManager();

        _gameState = GameState.Play;
    }

    private void NextLevel()
    {
        CheckCompleteLevel();
        _yardManager.CheckCompleteLevel(GetObjectPool());
        StartGame();
    }

    private void CheckCompleteLevel()
    {
        _objectPool = GetObjectPool();
        foreach (var view in _enemyViews)
        {
            _objectPool.ReturnObjectToPool(view.gameObject);
        }
        
        _enemyViews.Clear();
    }

    private void SetHUDManager()
    {
        _hudManager.SetData(_enemyViews.Count, NextLevel);
    }

    private void CreatePlayer()
    {
        if (_playerView == null)
        {
            var player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity, _playerAndEnemyRoot);
            _playerView = player.GetComponent<PlayerView>();
        }

        _playerView.SetData(_config.maxCollectEnemyCount);
    }

    private void CreateField()
    {
        _fieldController.CreateField(Camera.main, _playerView);
        _yardManager.SetData(EnemyDelivered, _objectPool);
    }

    private void CreateEnemy()
    {
        _enemyMaker ??= new EnemyMaker(_objectPool, _fieldController);

        _enemiesDeliveredCount = 0;
        _enemyMaker.AddEnemies(_enemyViews, Random.Range(_config.minEnemyCount, _config.maxEnemyCount));

        StartCoroutine(AddEnemiesRoutine());
    }

    private void AddEnemy()
    {
        if (_enemyViews.Count < _config.maxEnemyCountOnScene)
        {
            var count = Random.Range(_config.minAddEnemyCount, _config.maxAddEnemyCount);
            if (_enemyViews.Count + count > _config.maxEnemyCountOnScene)
                count = _config.maxEnemyCountOnScene - _enemyViews.Count;
            
            _enemyMaker.AddEnemies(_enemyViews, count);
            _hudManager.UpdateScore(_enemiesDeliveredCount,_enemyViews.Count);
        }
    }
    

    private void CreateAI()
    {
        _enemyAI ??= new EnemyAI();
        _enemyAI.SetData(_enemyViews, _fieldController.Bounds, _config);
    }
    
    private void EnemyDelivered(EnemyView view)
    {
        if (_enemyViews.Contains(view))
        {
            _enemyViews.Remove(view);
            _playerView.EnemyDelivered(view);
            
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
        if (_enemyViews.Count == _enemiesDeliveredCount)
        {
            onGameEnd?.Invoke();
            _gameState = GameState.End;
        }
    }
    
    private System.Collections.IEnumerator AddEnemiesRoutine()
    {
        yield return new WaitForSeconds(_config.startDelay);
        while (true)
        {
            AddEnemy();
            yield return new WaitForSeconds(Random.Range(_config.minAddEnemiesDelay, _config.maxAddEnemiesDelay));
        }
    }
    
    private ObjectPool GetObjectPool() => _objectPool ?? new ObjectPool(_enemyPrefab, _config.maxEnemyCount, _playerAndEnemyRoot);
}
