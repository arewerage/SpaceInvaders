using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _Project._Codebase.ECS._OLD.Player
{
    [AddComponentMenu("ECS/" + nameof(PlayerMarker))]
    public sealed class PlayerMarkerProvider : MonoProvider<PlayerMarker>
    {
    }
}