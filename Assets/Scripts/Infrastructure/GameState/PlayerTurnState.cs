using Infrastructure.Factories;

namespace Infrastructure.GameState
{
    internal class PlayerTurnState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerHolder _playerHolder;
        private readonly GameContainer _gameContainer;

        public PlayerTurnState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IPlayerHolder playerHolder, GameContainer gameContainer)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _playerHolder = playerHolder;
            _gameContainer = gameContainer;
        }
        
        public void Enter()
        {
            _playerHolder.Energy.Refresh();
            _gameContainer.UIContainer.EndTurnButton.onClick.AddListener(FinishStep);
            
            var playerDeck = _gameContainer.UIContainer.PlayerDeck;
            for (int i = 0; i < 6; i++)
            {
                _gameFactory.SpawnCard(playerDeck, CardId.Damage);
            }
        }

        private void FinishStep()
        {
            _gameStateMachine.Enter<EnemyTurnState>();
        }

        public void Exit()
        {
            _gameContainer.UIContainer.EndTurnButton.onClick.RemoveListener(FinishStep);
            _gameContainer.UIContainer.PlayerDeck.DiscardCards();
        }
    }
}