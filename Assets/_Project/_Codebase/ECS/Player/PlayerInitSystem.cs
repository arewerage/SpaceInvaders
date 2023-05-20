using _Project._Codebase.Configs;
using _Project._Codebase.Constants;
using _Project._Codebase.Core.Object;
using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Extensions;
using _Project._Codebase.ECS.Input;
using Cysharp.Threading.Tasks;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Player
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerInitSystem : IInitializer
    {
        private readonly IObjectFactory<MonoBehaviour, string> _objectFactory;
        private readonly EntitiesSpeedConfig _speedConfig;

        public World World { get; set; }

        public PlayerInitSystem(IObjectFactory<MonoBehaviour, string> objectFactory, EntitiesSpeedConfig speedConfig)
        {
            _objectFactory = objectFactory;
            _speedConfig = speedConfig;
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
            MonoBehaviour playerObject = await _objectFactory.CreateAsync(AssetAddress.Player);

            if (playerObject.TryGetEntity(out Entity playerEntity) == false)
                return;
            
            playerEntity.SetComponent(new PlayerMarker());
            playerEntity.SetComponent(new GameplayInputComponent { MoveAxis = Vector2.zero, IsShooting = false });
            playerEntity.SetComponent(new SpeedComponent { Value = _speedConfig.PlayerSpeed });
            playerEntity.SetComponent(new VelocityComponent { Value = Vector2.zero });
        }
    }
}