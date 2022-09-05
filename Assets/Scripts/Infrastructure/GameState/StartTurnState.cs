using System;
using Entities;
using Entities.Enemies;

namespace Infrastructure.GameState
{
    public class StartTurnState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ITurnResolver _turnResolver;

        public StartTurnState(GameStateMachine gameStateMachine, ITurnResolver turnResolver)
        {
            _gameStateMachine = gameStateMachine;
            _turnResolver = turnResolver;
        }

        public void Enter()
        {
            _turnResolver.Next();
            
            switch (_turnResolver.Current)
            {
                case Player:
                    _gameStateMachine.Enter<PlayerTurnState>();
                    break;
                case Enemy:
                    _gameStateMachine.Enter<EnemyTurnState>();
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public void Exit()
        {
            
        }
    }
}