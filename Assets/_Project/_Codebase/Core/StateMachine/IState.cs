namespace _Project._Codebase.Core.StateMachine
{
    public interface IState<in TContext> where TContext : class
    {
        void EnterWithContext(TContext context);
        void Exit();
    }
}