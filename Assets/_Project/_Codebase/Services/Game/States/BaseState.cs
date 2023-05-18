using _Project._Codebase.Core.StateMachine;

namespace _Project._Codebase.Services.Game.States
{
    public abstract class BaseState : IState<Game>
    {
        protected Game Game { get; private set; }

        public void EnterWithContext(Game context)
        {
            Game = context;
            Enter();
        }
        
        public virtual void Enter() {}

        public virtual void Exit() {}

        public virtual void Tick() {}

        public virtual void FixedTick() {}
        
        public virtual void LateTick() {}

        public virtual void Restart() {}
    }
}