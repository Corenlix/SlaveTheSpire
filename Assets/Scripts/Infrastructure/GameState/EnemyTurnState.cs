using Entities.Enemies;

namespace Infrastructure.GameState
{
    internal class EnemyTurnState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ITurnResolver _turnResolver;

        public EnemyTurnState(GameStateMachine gameStateMachine, ITurnResolver turnResolver)
        {
            _gameStateMachine = gameStateMachine;
            _turnResolver = turnResolver;
        }
        
        public void Enter()
        {
            var enemy = (Enemy) _turnResolver.Current;
            enemy.EnemyStepped += OnEnemyStepped;
            enemy.Step();
        }

        private void OnEnemyStepped(Enemy enemy)
        {
            enemy.EnemyStepped -= OnEnemyStepped;
            EndStep();
        }

        private void EndStep()
        {
            _gameStateMachine.Enter<StartTurnState>();
        }

        public void Exit()
        {
        }
    }
}