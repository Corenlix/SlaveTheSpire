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
        private readonly GameContainer _gameContainer;

        public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IEnemiesHolder enemiesHolder, IPlayerHolder playerHolder, FinderUnderCursor finderUnderCursor, GameContainer gameContainer)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _enemiesHolder = enemiesHolder;
            _playerHolder = playerHolder;
            _finderUnderCursor = finderUnderCursor;
            _gameContainer = gameContainer;
        }

        public void Enter()
        {
            var uiContainer = _gameFactory.SpawnUIContainer();
            _gameContainer.UIContainer = uiContainer;
            var playerDeck = uiContainer.PlayerDeck;
            var canvasTransform = uiContainer.Canvas.transform;
            playerDeck.transform.SetParent(canvasTransform);
            var targetSelectorsPool = _gameFactory.SpawnCardTargetSelectorsPool();
            targetSelectorsPool.transform.SetParent(canvasTransform);
            var cardMover = _gameFactory.SpawnCardMover();
            cardMover.Init(targetSelectorsPool, playerDeck, _finderUnderCursor);
            _gameContainer.UIContainer.EnergyView.Init(_playerHolder.Energy);

            _gameContainer.Location = _gameFactory.SpawnLocation();
            var player = _gameFactory.SpawnPlayer();
            player.transform.SetParent(_gameContainer.Location.PlayerSpawnPoint);
            player.transform.position = _gameContainer.Location.PlayerSpawnPoint.position;
            _playerHolder.SetPlayer(player);
            _enemiesHolder.Add(_gameFactory.SpawnEnemy(EnemyId.Test));
            _enemiesHolder.Add(_gameFactory.SpawnEnemy(EnemyId.Test));

            _gameStateMachine.Enter<PlayerTurnState>();
        }

        public void Exit()
        {
            
        }
    }
}