using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _Project._Codebase.ECS._OLD.Common
{
    [AddComponentMenu("ECS/" + nameof(RigidbodyComponent))]
    public sealed class RigidbodyComponentProvider : MonoProvider<RigidbodyComponent>
    {
    }
}