using Infrastructure.Factories;
using Infrastructure.StaticData;

namespace Infrastructure.GameState
{
    internal class PlayerTurnState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerHolder _playerHolder;
        private readonly UIHolder _uiHolder;

        public PlayerTurnState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IPlayerHolder playerHolder, UIHolder uiHolder)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _playerHolder = playerHolder;
            _uiHolder = uiHolder;
        }
        
        public void Enter()
        {
            _playerHolder.Player.Energy.Refresh();
            _uiHolder.UI.EndTurnButton.onClick.AddListener(FinishStep);
            
            for (int i = 0; i < 3; i++)
            {
                var card = _gameFactory.SpawnCard(CardId.TestBuff);
                _gameFactory.SpawnCard(CardId.Damage);
            }
        }

        private void FinishStep()
        {
            _gameStateMachine.Enter<EnemyTurnState>();
        }

        public void Exit()
        {
            _uiHolder.UI.EndTurnButton.onClick.RemoveListener(FinishStep);
            _uiHolder.UI.PlayerDeck.DiscardCards();
        }
    }
}