using Cards;
using Entities;
using Infrastructure.Factories;

namespace Infrastructure.GameState
{
    internal class PlayerTurnState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly ITurnResolver _turnResolver;
        private readonly UIHolder _uiHolder;
        private Player _player;

        public PlayerTurnState(GameStateMachine gameStateMachine, IGameFactory gameFactory, ITurnResolver turnResolver, UIHolder uiHolder)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _turnResolver = turnResolver;
            _uiHolder = uiHolder;
        }
        
        public void Enter()
        {
            _player = (Player) _turnResolver.Current;
            _player.Step();
            _uiHolder.UI.EndTurnButton.onClick.AddListener(FinishStep);
            _uiHolder.UI.PlayerUI.ObservePlayer(_player);

            for (int i = 0; i < 5; i++)
            {
                var card = _gameFactory.SpawnCard(_player.DeckHolder.GetCard(), _player);
                card.Destroyed += OnCardDestroyed;
            }                        
        }

        private void OnCardDestroyed(Card card)
        {
            _player.DeckHolder.PushCard(card.CardId);
            card.Destroyed -= OnCardDestroyed;
        }

        private void FinishStep()
        {
            _gameStateMachine.Enter<StartTurnState>();
        }

        public void Exit()
        {
            _uiHolder.UI.EndTurnButton.onClick.RemoveListener(FinishStep);
            _uiHolder.UI.PlayerDeck.DiscardCards();
        }
    }
}