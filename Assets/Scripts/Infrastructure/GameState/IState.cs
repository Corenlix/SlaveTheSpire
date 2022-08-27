namespace Infrastructure.GameState
{
    internal interface IState
    {
        void Enter();
        void Exit();
    }
}
