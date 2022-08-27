namespace Infrastructure.GameState
{
    public class PlayerTurnState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly GameContainer _gameContainer;

        public PlayerTurnState(GameStateMachine gameStateMachine, IGameFactory gameFactory, GameContainer gameContainer)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _gameContainer = gameContainer;
        }
        
        public void Enter()
        {
            var playerDeck = _gameContainer.UIContainer.PlayerDeck;
            for (int i = 0; i < 6; i++)
            {
                _gameFactory.SpawnCard(playerDeck, CardId.Damage);
            }
        }

        public void Exit()
        {
            _gameContainer.UIContainer.PlayerDeck.DiscardCards();
        }
    }
}