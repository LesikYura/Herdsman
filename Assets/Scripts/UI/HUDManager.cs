using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HUDManager : MonoBehaviour
    {
        /// <summary>
        ///  HUD
        ///  Show restart button
        /// </summary>
    
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _levelText;

        private Action _onNextLevelButtonClick;
        private int _maxScore;
        private int _levelIndex;
        public void SetData(int maxScore, int levelIndex, Action onNextLevelButtonClick)
        {
            _maxScore = maxScore;
            _onNextLevelButtonClick = onNextLevelButtonClick;
            _levelIndex = levelIndex;
            UpdateScore(0);
        }

        public void UpdateScore(int score)
        {
            _scoreText.text = $"{score}/{_maxScore}";
            _levelText.text = $"Level {_levelIndex + 1}";
        }
    
        public void UpdateScore(int score, int maxScore)
        {
            _maxScore = maxScore;
            UpdateScore(score);
        }

        public void NewLevelButtonClick()
        {
            _onNextLevelButtonClick?.Invoke();
        }
    }
}
