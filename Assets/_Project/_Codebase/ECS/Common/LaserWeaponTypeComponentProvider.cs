using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _Project._Codebase.ECS.Common
{
    [AddComponentMenu("ECS/" + nameof(LaserWeaponTypeComponent))]
    public sealed class LaserWeaponTypeComponentProvider : MonoProvider<LaserWeaponTypeComponent>
    {
    }
}