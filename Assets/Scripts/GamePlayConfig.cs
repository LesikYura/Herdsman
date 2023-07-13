using UnityEngine;

[CreateAssetMenu(fileName = "GamePlayConfig", menuName = "Config/GamePlayConfig")]
public class GamePlayConfig : ScriptableObject
{
    public int minEnemyCount = 5;
    public int maxEnemyCount = 15;
    public int maxCollectEnemyCount = 5;
}
