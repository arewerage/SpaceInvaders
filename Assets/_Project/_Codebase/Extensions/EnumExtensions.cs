using System;

namespace _Project._Codebase.Extensions
{
    public static class EnumExtensions
    {
        private static readonly Random _random = new();

        public static T GetRandom<T>() where T : Enum
        {
            T[] values = (T[])Enum.GetValues(typeof(T));
            return values[_random.Next(values.Length)];
        }
    }
}