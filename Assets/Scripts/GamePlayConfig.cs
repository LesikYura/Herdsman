using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePlayConfig", menuName = "Config/GamePlayConfig")]
public class GamePlayConfig : ScriptableObject
{
    public int minEnemyCount = 5;
    public int maxEnemyCount = 15;
    public int maxEnemyCountOnScene = 40;
    public int maxCollectEnemyCount = 5;
    
    [Header("Enemy AI")]
    public float minDelay = 2f;
    public float maxDelay = 5f;
    public float maxStep = 200f;
    
    [Header("Add enemies")]
    public float startDelay = 5f;
    public float minAddEnemiesDelay = 5f;
    public float maxAddEnemiesDelay = 10f;
    public int minAddEnemyCount = 1;
    public int maxAddEnemyCount = 5;

    [Header("Level data")] 
    public List<LevelConfig> _LevelConfigs = new List<LevelConfig>();
}

[Serializable]
public class LevelConfig
{
    public int collectedCountOnLevel;
}
