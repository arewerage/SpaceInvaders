using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _Project._Codebase.ECS._OLD.Common
{
    [AddComponentMenu("ECS/" + nameof(SpriteRendererComponent))]
    public sealed class SpriteRendererComponentProvider : MonoProvider<SpriteRendererComponent>
    {
    }
}