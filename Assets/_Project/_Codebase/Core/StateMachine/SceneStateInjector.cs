using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace _Project._Codebase.Core.StateMachine
{
    public class SceneStateInjector<TBaseState, TContext> : IInitializable, IDisposable
        where TBaseState: IState<TContext>
        where TContext: class
    {
        private readonly IManageableStateMachine<TBaseState, TContext> _stateMachine;
        private readonly IEnumerable<TBaseState> _states;

        public SceneStateInjector(IManageableStateMachine<TBaseState, TContext> stateMachine, IEnumerable<TBaseState> states)
        {
            _stateMachine = stateMachine;
            _states = states;
        }

        public void Initialize()
        {
            foreach (TBaseState state in _states)
                _stateMachine.AddState(state);
        }

        public void Dispose()
        {
            foreach (TBaseState state in _states)
                _stateMachine.RemoveState(state.GetType());
        }
    }
}