namespace Infrastructure.GameState
{
    internal class EnemyTurnState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IEnemiesHolder _enemiesHolder;

        public EnemyTurnState(GameStateMachine gameStateMachine, IEnemiesHolder enemiesHolder)
        {
            _gameStateMachine = gameStateMachine;
            _enemiesHolder = enemiesHolder;
        }
        
        public void Enter()
        {
            _enemiesHolder.Step();
            //подписываешься на enemiesHolder.Когда все подходили => переходишь в состояние PlayerTurnState
            _gameStateMachine.Enter<PlayerTurnState>();
        }

        public void Exit()
        {
            
        }
    }
}