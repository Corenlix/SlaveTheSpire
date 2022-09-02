using Infrastructure.Factories;
using Infrastructure.StaticData.Enemies;

namespace Infrastructure.GameState
{
    internal class LoadLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            _gameFactory.SpawnLocation();
            _gameFactory.SpawnPlayer();
            
            _gameFactory.SpawnUIContainer();
            _gameFactory.SpawnCardMover();

            _gameFactory.SpawnEnemy(EnemyId.Test);
            _gameFactory.SpawnEnemy(EnemyId.Test);

            _gameStateMachine.Enter<PlayerTurnState>();
        }

        public void Exit()
        {
            
        }
    }
}