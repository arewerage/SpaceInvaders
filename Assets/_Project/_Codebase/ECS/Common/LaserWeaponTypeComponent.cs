using System;
using _Project._Codebase.Configs.Laser;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace _Project._Codebase.ECS.Common
{
    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct LaserWeaponTypeComponent : IComponent
    {
        public LaserId LaserId;
    }
}