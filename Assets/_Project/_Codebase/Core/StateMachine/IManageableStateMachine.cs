namespace _Project._Codebase.Core.StateMachine
{
    public interface IManageableStateMachine<in TBaseState, in TContext>
        where TBaseState : IState<TContext>
        where TContext : class
    {
        void AddState(TBaseState state);
        void RemoveState(System.Type stateType);
    }
}