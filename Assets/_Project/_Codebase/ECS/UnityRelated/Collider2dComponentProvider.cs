using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _Project._Codebase.ECS.UnityRelated
{
    [AddComponentMenu("ECS/" + nameof(Collider2dComponent))]
    public sealed class Collider2dComponentProvider : MonoProvider<Collider2dComponent>
    {
    }
}