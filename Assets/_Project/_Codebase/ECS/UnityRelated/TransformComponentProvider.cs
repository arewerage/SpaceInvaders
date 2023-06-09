﻿using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _Project._Codebase.ECS.UnityRelated
{
    [AddComponentMenu("ECS/" + nameof(TransformComponent))]
    public sealed class TransformComponentProvider : MonoProvider<TransformComponent>
    {
    }
}