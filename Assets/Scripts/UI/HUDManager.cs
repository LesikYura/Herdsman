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
    
        [SerializeField] private TextMeshProUGUI _score;

        private Action _onNextLevelButtonClick;
        private int _maxScore;
        public void SetData(int maxScore, Action onNextLevelButtonClick)
        {
            _maxScore = maxScore;
            _onNextLevelButtonClick = onNextLevelButtonClick;
            UpdateScore(0);
        }

        public void UpdateScore(int score)
        {
            _score.text = $"{score}/{_maxScore}";
        }
    
        public void UpdateScore(int score, int maxScore)
        {
            _maxScore = maxScore;
            _score.text = $"{score}/{_maxScore}";
        }

        public void NewLevelButtonClick()
        {
            _onNextLevelButtonClick?.Invoke();
        }
    }
}
