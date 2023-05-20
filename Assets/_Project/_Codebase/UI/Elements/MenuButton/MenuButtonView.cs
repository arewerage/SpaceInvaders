using UnityEngine;
using UnityEngine.UI;

namespace _Project._Codebase.UI.Elements.MenuButton
{
    public class MenuButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public Button Button => _button;
    }
}