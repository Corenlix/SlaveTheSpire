using Infrastructure.Factories;
using Infrastructure.Progress;
using Infrastructure.StaticData.Enemies;

namespace Infrastructure.GameState
{
    internal class LoadLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;
        private readonly IPlayersHolder _playersHolder;

        public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IProgressService progressService, IPlayersHolder playersHolder)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _playersHolder = playersHolder;
        }

        public void Enter()
        {
            _gameFactory.SpawnLocation();
            _progressService.AddClient(_playersHolder);

            _gameFactory.SpawnUIContainer();
            _gameFactory.SpawnCardMover();
            
            _progressService.Load();

            _gameFactory.SpawnEnemy(EnemyId.HomelessBandit);
            _gameFactory.SpawnEnemy(EnemyId.HomelessBandit);

            _gameStateMachine.Enter<StartTurnState>();
        }

        public void Exit()
        {
            
        }
    }
}