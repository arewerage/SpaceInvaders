using _Project._Codebase.ECS;
using Scellecs.Morpeh;

namespace _Project._Codebase.Services.Game.States
{
    public class GameLoopState : BaseState
    {
        private readonly ECSWorld _ecsWorld;

        public GameLoopState(ECSWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
        }
        
        public override void Enter()
        {
        }

        public override void Exit()
        {
            _ecsWorld.Dispose();
        }

        public override void Tick(float deltaTime)
        {
            _ecsWorld.World.Update(deltaTime);
            _ecsWorld.World.CleanupUpdate(deltaTime);
        }
        
        public override void FixedTick(float fixedDeltaTime)
        {
            _ecsWorld.World.FixedUpdate(fixedDeltaTime);
        }
        
        public override void LateTick(float deltaTime)
        {
            _ecsWorld.World.LateUpdate(deltaTime);
        }

        public override void Restart()
        {
            Game.ChangeState<LoadLevelState>();
        }
    }
}