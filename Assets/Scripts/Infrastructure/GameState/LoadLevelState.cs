using Infrastructure.Factories;
using Infrastructure.StaticData;
using Utilities;

namespace Infrastructure.GameState
{
    internal class LoadLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IEnemiesHolder _enemiesHolder;
        private readonly IPlayerHolder _playerHolder;
        private readonly FinderUnderCursor _finderUnderCursor;
        private readonly SceneContainer _sceneContainer;

        public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IEnemiesHolder enemiesHolder, IPlayerHolder playerHolder, FinderUnderCursor finderUnderCursor, SceneContainer sceneContainer)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _enemiesHolder = enemiesHolder;
            _playerHolder = playerHolder;
            _finderUnderCursor = finderUnderCursor;
            _sceneContainer = sceneContainer;
        }

        public void Enter()
        {
            _sceneContainer.UIContainer = _gameFactory.SpawnUIContainer();
            _sceneContainer.Location = _gameFactory.SpawnLocation();
            
            _gameFactory.SpawnCardMover();
            _gameFactory.SpawnPlayer();

            _gameFactory.SpawnEnemy(EnemyId.Test);
            _gameFactory.SpawnEnemy(EnemyId.Test);

            _gameStateMachine.Enter<PlayerTurnState>();
        }

        public void Exit()
        {
            
        }
    }
}