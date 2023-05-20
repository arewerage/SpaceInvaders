using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _Project._Codebase.ECS.UnityRelated
{
    [AddComponentMenu("ECS/" + nameof(Rigidbody2dComponent))]
    public sealed class Rigidbody2dComponentProvider : MonoProvider<Rigidbody2dComponent>
    {
    }
}