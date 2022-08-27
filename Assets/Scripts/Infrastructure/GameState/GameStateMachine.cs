using System;
using System.Collections.Generic;
using Infrastructure.Factories;
using Utilities;

namespace Infrastructure.GameState
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(IGameFactory gameFactory, GameContainer gameContainer, IEnemiesHolder enemiesHolder, IPlayerHolder playerHolder, FinderUnderCursor finderUnderCursor)
        {
            _states = new Dictionary<Type, IState>
            {
                {typeof(LoadLevelState), new LoadLevelState(this, gameFactory, enemiesHolder, playerHolder, finderUnderCursor, gameContainer)},
                {typeof(PlayerTurnState), new PlayerTurnState(this, gameFactory, playerHolder, gameContainer)},
                {typeof(EnemyTurnState), new EnemyTurnState(this, enemiesHolder)}
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