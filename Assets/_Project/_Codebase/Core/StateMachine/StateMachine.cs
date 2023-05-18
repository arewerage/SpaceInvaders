using System;
using System.Collections.Generic;

namespace _Project._Codebase.Core.StateMachine
{
    public class StateMachine<TBaseState, TContext> : IStateMachine<TBaseState, TContext>,
        IManageableStateMachine<TBaseState, TContext>
        where TBaseState : IState<TContext>
        where TContext : class
    {
        private readonly Dictionary<Type, TBaseState> _states = new();
        
        public TBaseState CurrentState { get; private set; }
        
        public StateMachine(IEnumerable<TBaseState> states)
        {
            foreach (TBaseState state in states)
                AddState(state);
        }
        
        public void ChangeState<TState>(TContext context) where TState : TBaseState
        {
            Type stateType = typeof(TState);

            if (_states.TryGetValue(stateType, out TBaseState state) == false)
                throw new ArgumentOutOfRangeException(nameof(TState), $"State '{stateType}' not found in states.");

            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.EnterWithContext(context);
        }

        public void AddState(TBaseState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void RemoveState(Type stateType)
        {
            if (_states.Remove(stateType) == false)
                throw new ArgumentException($"Key '{stateType}' not found.");
        }
    }
}