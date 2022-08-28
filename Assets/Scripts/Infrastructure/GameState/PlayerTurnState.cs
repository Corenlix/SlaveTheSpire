using Infrastructure.Factories;
using Infrastructure.StaticData;

namespace Infrastructure.GameState
{
    internal class PlayerTurnState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerHolder _playerHolder;
        private readonly SceneContainer _sceneContainer;

        public PlayerTurnState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IPlayerHolder playerHolder, SceneContainer sceneContainer)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _playerHolder = playerHolder;
            _sceneContainer = sceneContainer;
        }
        
        public void Enter()
        {
            _playerHolder.Energy.Refresh();
            _sceneContainer.UIContainer.EndTurnButton.onClick.AddListener(FinishStep);
            
            for (int i = 0; i < 6; i++)
            {
                _gameFactory.SpawnCard(CardId.TestBuff);
            }
        }

        private void FinishStep()
        {
            _gameStateMachine.Enter<EnemyTurnState>();
        }

        public void Exit()
        {
            _sceneContainer.UIContainer.EndTurnButton.onClick.RemoveListener(FinishStep);
            _sceneContainer.UIContainer.PlayerDeck.DiscardCards();
        }
    }
}