namespace Infrastructure.GameState
{
    public class LoadLevelState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly GameContainer _gameContainer;
        private readonly GameStateMachine _gameStateMachine;

        public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory, GameContainer gameContainer)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
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
            
            _gameStateMachine.Enter<PlayerTurnState>();
        }

        public void Exit()
        {
            
        }
    }
}