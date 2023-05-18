using _Project._Codebase.Core.StateMachine;
using _Project._Codebase.Services.Game.States;
using Zenject;

namespace _Project._Codebase.Services.Game
{
    public class Game : IGame, IInitializable, ITickable, IFixedTickable, ILateTickable
    {
        private readonly IStateMachine<BaseState, Game> _stateMachine;

        public Game(IStateMachine<BaseState, Game> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            ChangeState<BootstrapState>();
        }

        public void Tick()
        {
            _stateMachine.CurrentState.Tick();
        }

        public void FixedTick()
        {
            _stateMachine.CurrentState.FixedTick();
        }

        public void LateTick()
        {
            _stateMachine.CurrentState.LateTick();
        }

        public void Restart()
        {
            _stateMachine.CurrentState.Restart();
        }

        public void ChangeState<TState>() where TState : BaseState
        {
            _stateMachine.ChangeState<TState>(this);
        }
    }
}