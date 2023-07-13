using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    /// <summary>
    ///  HUD
    ///  Show restart button
    /// </summary>
    
    [SerializeField] private TextMeshProUGUI _score;

    private int _maxScore;
    public void SetData(int maxScore)
    {
        _maxScore = maxScore;
        UpdateScore(0);
    }

    public void UpdateScore(int score)
    {
        _score.text = $"{score}/{_maxScore}";
    }
}
