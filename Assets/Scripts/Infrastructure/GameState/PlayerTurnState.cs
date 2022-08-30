using Card;
using Infrastructure.Factories;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Cards;

namespace Infrastructure.GameState
{
    internal class PlayerTurnState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerHolder _playerHolder;
        private readonly UIHolder _uiHolder;
        private readonly IDeckHolder _deckHolder;

        public PlayerTurnState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IPlayerHolder playerHolder, UIHolder uiHolder, IDeckHolder deckHolder)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _playerHolder = playerHolder;
            _uiHolder = uiHolder;
            _deckHolder = deckHolder;
        }
        
        public void Enter()
        {
            _playerHolder.Player.Energy.Refresh();
            _uiHolder.UI.EndTurnButton.onClick.AddListener(FinishStep);

            for (int i = 0; i < 5; i++)
            {
                var card = _gameFactory.SpawnCard(_deckHolder.GetCard());
                card.Destroyed += OnCardDestroyed;
            }                        
        }

        private void OnCardDestroyed(CardHolder cardHolder)
        {
            _deckHolder.PushCard(cardHolder.CardStaticData.Id);
            cardHolder.Destroyed -= OnCardDestroyed;
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