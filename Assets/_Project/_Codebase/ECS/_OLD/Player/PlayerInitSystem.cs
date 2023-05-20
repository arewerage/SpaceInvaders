using _Project._Codebase.Constants;
using _Project._Codebase.Core.Object;
using _Project._Codebase.ECS._OLD.Common;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS._OLD.Player
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerInitSystem : IInitializer
    {
        private readonly IObjectFactory<MonoBehaviour, string> _objectFactory;

        public World World { get; set; }

        public PlayerInitSystem(IObjectFactory<MonoBehaviour, string> objectFactory)
        {
            _objectFactory = objectFactory;
        }

        public void OnAwake()
        {
            InitializeAsync().Forget();
        }

        public void Dispose()
        {
        }

        private async UniTaskVoid InitializeAsync()
        {
            MonoBehaviour player = await _objectFactory.CreateAsync(AssetAddress.Player);
            
            if (EntityProvider.map.TryGetValue(player.gameObject.GetInstanceID(), out EntityProvider.MapItem instance) == false)
            {
                Debug.LogError($"The {player} is not an Entity!");
                return;
            }

            Entity playerEntity = instance.entity;
            
            playerEntity.SetComponent(new SpeedComponent { Speed = 3f });
            playerEntity.SetComponent(new MoveDirectionComponent { Direction = Vector2.zero });
        }
    }
}