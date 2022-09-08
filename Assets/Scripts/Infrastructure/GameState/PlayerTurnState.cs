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
        private readonly IEnemiesHolder _enemiesHolder;

        public PlayerTurnState(GameStateMachine gameStateMachine, IGameFactory gameFactory, ITurnResolver turnResolver, UIHolder uiHolder, IEnemiesHolder enemiesHolder)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _turnResolver = turnResolver;
            _uiHolder = uiHolder;
            _enemiesHolder = enemiesHolder;
        }
        
        public void Enter()
        {
            var player = (Player) _turnResolver.Current;
            player.Step();
            _uiHolder.UI.EndTurnButton.onClick.AddListener(FinishStep);
            _uiHolder.UI.PlayerUI.ObservePlayer(player);

            for (int i = 0; i < 5; i++)
            {
                _gameFactory.SpawnCard(player);
            }
        }
        
        private void FinishStep()
        {
            if(_enemiesHolder.Enemies.Count == 0)
                _gameStateMachine.Enter<SelectLevelState>();
            else _gameStateMachine.Enter<StartTurnState>();
        }

        public void Exit()
        {
            _uiHolder.UI.EndTurnButton.onClick.RemoveListener(FinishStep);
            _uiHolder.UI.PlayerDeck.DiscardCards();
        }
    }
}