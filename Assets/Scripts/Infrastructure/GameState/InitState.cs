using System;
using Infrastructure.SaveLoad;

namespace Infrastructure.GameState
{
    public class InitState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;

        public InitState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
        }   
        
        public void Enter()
        {
            GameStage gameStage = _saveLoadService.GetValue(SaveLoadKey.GameStage, GameStage.SelectLevel);
            switch (gameStage)
            {
                case GameStage.SelectLevel:
                    _gameStateMachine.Enter<SelectLevelState>();
                    break;
                case GameStage.Battle:
                    _gameStateMachine.Enter<EnterBattleState>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Exit()
        {
            
        }
    }
}