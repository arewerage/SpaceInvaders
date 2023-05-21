using TMPro;
using UnityEngine;

namespace _Project._Codebase.UI.Elements.CurrentWave
{
    public class CurrentWaveView : MonoBehaviour
    {
        private const string WavePrefix = "Current wave: ";
        
        [SerializeField] private TMP_Text _scoreText;

        public void SetWave(int value)
        {
            _scoreText.SetText($"{WavePrefix}{value}");
        }
    }
}