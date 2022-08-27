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
            var playerDeck = uiContainer.PlayerDeck;
            var canvasTransform = uiContainer.Canvas.transform;
            playerDeck.transform.SetParent(canvasTransform);
            var targetSelectorsPool = _gameFactory.SpawnCardTargetSelectorsPool();
            targetSelectorsPool.transform.SetParent(canvasTransform);
            var cardMover = _gameFactory.SpawnCardMover(playerDeck);
            cardMover.Init(targetSelectorsPool);
            _gameContainer.UIContainer = uiContainer;
            _enemiesHolder.AddEnemy(_gameFactory.SpawnEnemy(EnemyId.Test));
            
            _gameStateMachine.Enter<PlayerTurnState>();
        }

        public void Exit()
        {
            
        }
    }
}