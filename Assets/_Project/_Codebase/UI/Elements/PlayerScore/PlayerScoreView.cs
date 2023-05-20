using TMPro;
using UnityEngine;

namespace _Project._Codebase.UI.Elements.PlayerScore
{
    public class PlayerScoreView : MonoBehaviour
    {
        private const string ScorePrefix = "Score: ";
        
        [SerializeField] private TMP_Text _scoreText;

        public void SetScore(int value)
        {
            _scoreText.SetText($"{ScorePrefix}{value}");
        }
    }
}