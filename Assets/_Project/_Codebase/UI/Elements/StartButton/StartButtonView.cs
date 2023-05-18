using UnityEngine;
using UnityEngine.UI;

namespace _Project._Codebase.UI.Elements.StartButton
{
    public class StartButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public Button Button => _button;
    }
}