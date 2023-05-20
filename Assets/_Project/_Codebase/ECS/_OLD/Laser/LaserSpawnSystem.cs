using _Project._Codebase.Constants;
using _Project._Codebase.Core.Object;
using _Project._Codebase.ECS._OLD.Common;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _Project._Codebase.ECS._OLD.Laser
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class LaserSpawnSystem : ISystem
    {
        private readonly IObjectFactory<MonoBehaviour, string> _objectFactory;
        
        public World World { get; set; }

        private Event<LaserSpawnEvent> _laserSpawnEvent;

        public LaserSpawnSystem(IObjectFactory<MonoBehaviour, string> objectFactory)
        {
            _objectFactory = objectFactory;
        }

        public void OnAwake()
        {
            _laserSpawnEvent = World.GetEvent<LaserSpawnEvent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (LaserSpawnEvent laserSpawnEvent in _laserSpawnEvent.BatchedChanges)
            {
                SpawnAsync(laserSpawnEvent).Forget();
            }
        }

        public void Dispose()
        {
        }

        private async UniTaskVoid SpawnAsync(LaserSpawnEvent laserSpawnEvent)
        {
            MonoBehaviour laser = await _objectFactory.CreateAsync(AssetAddress.Laser);
            
            if (EntityProvider.map.TryGetValue(laser.gameObject.GetInstanceID(), out EntityProvider.MapItem instance) == false)
            {
                Debug.LogError($"The {laser} is not an Entity!");
                return;
            }

            Entity laserEntity = instance.entity;
            
            laserEntity.GetComponent<TransformComponent>().Transform.position = laserSpawnEvent.Transform.position;
            laserEntity.GetComponent<SpriteRendererComponent>().Renderer.flipX = laserSpawnEvent.IsLookDown;
            
            laserEntity.SetComponent(new LaserOwnerComponent { Owner = laserSpawnEvent.Owner });
            laserEntity.SetComponent(new SpeedComponent { Speed = 6f });
            laserEntity.SetComponent(new MoveDirectionComponent { Direction = laserSpawnEvent.Direction });
            laserEntity.SetComponent(new DestroyOnOffscreenMarker());
        }
    }
}