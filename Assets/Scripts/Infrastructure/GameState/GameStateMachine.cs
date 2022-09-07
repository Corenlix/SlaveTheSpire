using System;
using System.Collections.Generic;
using Infrastructure.Factories;
using Infrastructure.Progress;

namespace Infrastructure.GameState
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(IGameFactory gameFactory, UIHolder uiHolder, ITurnResolver turnResolver, IProgressService progressService, IPlayersHolder playersHolder)
        {
            _states = new Dictionary<Type, IState>
            {
                {typeof(LoadLevelState), new LoadLevelState(this, gameFactory, progressService, playersHolder)},
                {typeof(StartTurnState), new StartTurnState(this, turnResolver)},
                {typeof(PlayerTurnState), new PlayerTurnState(this, gameFactory, turnResolver, uiHolder)},
                {typeof(EnemyTurnState), new EnemyTurnState(this, turnResolver)}
            };

            Enter<LoadLevelState>();
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