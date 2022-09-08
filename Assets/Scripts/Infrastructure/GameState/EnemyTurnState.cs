using Entities.Enemies;
using Infrastructure.SaveLoad;
using UnityEngine.SceneManagement;

namespace Infrastructure.GameState
{
    internal class EnemyTurnState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ITurnResolver _turnResolver;
        private readonly IPlayersHolder _playersHolder;
        private readonly ISaveLoadService _saveLoadService;

        public EnemyTurnState(GameStateMachine gameStateMachine, ITurnResolver turnResolver, IPlayersHolder playersHolder, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _turnResolver = turnResolver;
            _playersHolder = playersHolder;
            _saveLoadService = saveLoadService;
        }
        
        public void Enter()
        {
            var enemy = (Enemy) _turnResolver.Current;
            enemy.EnemyStepped += OnEnemyStepped;
            enemy.Step();
        }

        private void OnEnemyStepped(Enemy enemy)
        {
            enemy.EnemyStepped -= OnEnemyStepped;
            EndStep();
        }

        private void EndStep()
        {
            if (_playersHolder.Players.Count == 0)
            {
                _saveLoadService.Clear();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else 
                _gameStateMachine.Enter<StartTurnState>();
        }

        public void Exit()
        {
        }
    }
}