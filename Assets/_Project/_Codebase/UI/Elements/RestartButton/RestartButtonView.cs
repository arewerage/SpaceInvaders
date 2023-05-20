using UnityEngine;
using UnityEngine.UI;

namespace _Project._Codebase.UI.Elements.RestartButton
{
    public class RestartButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public Button Button => _button;
    }
}