using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _Project._Codebase.ECS._OLD.Laser
{
    [AddComponentMenu("ECS/" + nameof(LaserMarker))]
    public sealed class LaserMarkerProvider : MonoProvider<LaserMarker>
    {
    }
}