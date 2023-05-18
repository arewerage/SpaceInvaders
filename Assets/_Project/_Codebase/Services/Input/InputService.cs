using UnityEngine;
using VContainer.Unity;

namespace _Project._Codebase.Services.Input
{
    public class InputService : IInputService, IInitializable
    {
        private readonly Controls _controls;

        public Vector2 MoveAxes => _controls.Gameplay.Move.ReadValue<Vector2>();
        public bool IsShooting => _controls.Gameplay.Shoot.WasPressedThisFrame();

        public InputService(Controls controls)
        {
            _controls = controls;
        }
        
        public void Initialize()
        {
            _controls.Gameplay.Enable();
        }
    }
}