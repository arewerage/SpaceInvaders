using UnityEngine;

namespace _Project._Codebase.Services.Input
{
    public interface IInputService
    {
        Vector2 MoveAxes { get; }
        bool IsShooting { get; }
    }
}