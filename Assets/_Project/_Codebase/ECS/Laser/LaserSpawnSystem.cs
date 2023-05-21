using _Project._Codebase.Configs;
using _Project._Codebase.Constants;
using _Project._Codebase.Core.Object;
using _Project._Codebase.ECS.Common;
using _Project._Codebase.ECS.Extensions;
using _Project._Codebase.ECS.Physics;
using _Project._Codebase.ECS.UnityRelated;
using Cysharp.Threading.Tasks;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS.Laser
{
    using Scellecs.Morpeh;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class LaserSpawnSystem : ISystem
    {
        private readonly IObjectFactory<MonoBehaviour, string> _objectFactory;
        private readonly EntitiesSpeedConfig _speedConfig;

        private Event<LaserSpawnRequest> _laserSpawnRequest;

        public World World { get; set; }

        public LaserSpawnSystem(IObjectFactory<MonoBehaviour, string> objectFactory, EntitiesSpeedConfig speedConfig)
        {
            _objectFactory = objectFactory;
            _speedConfig = speedConfig;
        }

        public void OnAwake()
        {
            _laserSpawnRequest = World.GetEvent<LaserSpawnRequest>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_laserSpawnRequest.IsPublished == false)
                return;

            foreach (LaserSpawnRequest laserSpawnRequest in _laserSpawnRequest.BatchedChanges)
            {
                SpawnAsync(laserSpawnRequest).Forget();
            }
        }

        public void Dispose()
        {
        }

        private async UniTaskVoid SpawnAsync(LaserSpawnRequest laserSpawnRequest)
        {
            MonoBehaviour laserObject = await _objectFactory.CreateAsync(AssetAddress.Laser);
            
            if (laserObject.TryGetEntity(out Entity laserEntity) == false)
                return;
            
            laserEntity.SetComponent(new LaserMarker());
            laserEntity.SetComponent(new OwnerComponent { Value = laserSpawnRequest.Owner });
            laserEntity.SetComponent(new SpeedComponent { Value = _speedConfig.ShootingLaserSpeed });
            laserEntity.SetComponent(new VelocityComponent { Value = Vector2.up });

            var rigidbody = laserEntity.GetComponent<Rigidbody2dComponent>().Value;
            rigidbody.position = laserSpawnRequest.Position;
            rigidbody.rotation = laserSpawnRequest.Angle;
        }
    }
}