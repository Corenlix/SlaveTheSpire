using System;
using System.Collections.Generic;
using Infrastructure.Factories;
using Infrastructure.Progress;
using Infrastructure.SaveLoad;

namespace Infrastructure.GameState
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(IGameFactory gameFactory, UIHolder uiHolder, ITurnResolver turnResolver, IProgressService progressService, ISaveLoadService saveLoadService, IPlayersHolder playersHolder, IEnemiesHolder enemiesHolder)
        {
            _states = new Dictionary<Type, IState>
            {
                {typeof(InitState), new InitState(this, saveLoadService)},
                {typeof(SelectLevelState), new SelectLevelState(gameFactory, progressService, saveLoadService)},
                
                {typeof(EnterBattleState), new EnterBattleState(this, gameFactory, progressService, playersHolder)},
                {typeof(StartTurnState), new StartTurnState(this, turnResolver)},
                {typeof(PlayerTurnState), new PlayerTurnState(this, gameFactory, turnResolver, uiHolder, enemiesHolder)},
                {typeof(EnemyTurnState), new EnemyTurnState(this, turnResolver, playersHolder, saveLoadService)}
            };

            Enter<InitState>();
        }
        
        internal void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.Exit();
      
            TState state = GetState<TState>();
            _activeState = state;
      
            return state;
        }

        private TState GetState<TState>() where TState : class, IState => 
            _states[typeof(TState)] as TState;
    }
}