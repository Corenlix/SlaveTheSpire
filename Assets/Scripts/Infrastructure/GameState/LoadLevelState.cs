using Infrastructure.Factories;
using Infrastructure.StaticData;

namespace Infrastructure.GameState
{
    internal class LoadLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IEnemiesHolder _enemiesHolder;
        private readonly GameContainer _gameContainer;

        public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IEnemiesHolder enemiesHolder, GameContainer gameContainer)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _enemiesHolder = enemiesHolder;
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
            var cardMover = _gameFactory.SpawnCardMover(playerDeck);
            cardMover.Init(targetSelectorsPool);
            
            _gameContainer.Location = _gameFactory.SpawnLocation();
            var player = _gameFactory.SpawnPlayer();
            player.transform.SetParent(_gameContainer.Location.PlayerSpawnPoint);
            player.transform.position = _gameContainer.Location.PlayerSpawnPoint.position;
            _enemiesHolder.AddEnemy(_gameFactory.SpawnEnemy(EnemyId.Test));

            _gameStateMachine.Enter<PlayerTurnState>();
        }

        public void Exit()
        {
            
        }
    }
}