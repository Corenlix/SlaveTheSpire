using System;
using System.Collections.Generic;

namespace Infrastructure.GameState
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(IGameFactory gameFactory, GameContainer gameContainer)
        {
            _states = new Dictionary<Type, IState>
            {
                {typeof(LoadLevelState), new LoadLevelState(this, gameFactory, gameContainer)},
                {typeof(PlayerTurnState), new PlayerTurnState(this, gameFactory, gameContainer)},
            };

            Enter<LoadLevelState>();
        }
        
        public void Enter<TState>() where TState : class, IState
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