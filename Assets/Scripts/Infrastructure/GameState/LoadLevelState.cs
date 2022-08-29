using Infrastructure.Factories;
using Infrastructure.StaticData;
using Utilities;

namespace Infrastructure.GameState
{
    internal class LoadLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly SceneContainer _sceneContainer;

        public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory, SceneContainer sceneContainer)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _sceneContainer = sceneContainer;
        }

        public void Enter()
        {
            _sceneContainer.Location = _gameFactory.SpawnLocation();
            _gameFactory.SpawnPlayer();
            
            _sceneContainer.UIContainer = _gameFactory.SpawnUIContainer();
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