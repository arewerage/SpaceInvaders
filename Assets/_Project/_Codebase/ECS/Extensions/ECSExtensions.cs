using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _Project._Codebase.ECS.Extensions
{
    public static class ECSExtensions
    {
        public static bool TryGetEntity(this Component component, out Entity entity)
        {
            if (EntityProvider.map.TryGetValue(component.gameObject.GetInstanceID(), out EntityProvider.MapItem instance) == false)
            {
                Debug.LogError($"The {component.gameObject} is not an Entity!");
                entity = null;
                return false;
            }

            entity = instance.entity;
            return true;
        }
    }
}