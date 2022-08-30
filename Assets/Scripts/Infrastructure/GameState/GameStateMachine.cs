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

        public GameStateMachine(IGameFactory gameFactory, UIHolder uiHolder, IEnemiesHolder enemiesHolder, IPlayerHolder playerHolder, IDeckHolder deckHolder)
        {
            _states = new Dictionary<Type, IState>
            {
                {typeof(LoadLevelState), new LoadLevelState(this, gameFactory)},
                {typeof(PlayerTurnState), new PlayerTurnState(this, gameFactory, playerHolder, uiHolder, deckHolder)},
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