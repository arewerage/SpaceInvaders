using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _Project._Codebase.ECS.UnityRelated
{
    [AddComponentMenu("ECS/" + nameof(SpriteRendererComponent))]
    public sealed class SpriteRendererComponentProvider : MonoProvider<SpriteRendererComponent>
    {
    }
}