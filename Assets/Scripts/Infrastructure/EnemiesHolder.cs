using System;
using System.Collections.Generic;
using Entities;

namespace Infrastructure
{
    public class EnemiesHolder : IEnemiesHolder
    {
        public event Action AllEnemySteped;

        private readonly GameContainer _gameContainer;
        private readonly List<Enemy> _enemies = new();
        private IEnumerator<Enemy> _enemiesStepEnumerator;

        public EnemiesHolder(GameContainer gameContainer)
        {
            _gameContainer = gameContainer;
        }

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
            _gameContainer.Location.EnemiesContainer.Add(enemy);
            enemy.Destroyed += RemoveEnemy;
        }

        public void Step()
        {
            _enemiesStepEnumerator = _enemies.GetEnumerator();
            StepByEnemy();
        }

        private void StepByEnemy()
        {
            if(_enemiesStepEnumerator.Current != null)
                _enemiesStepEnumerator.Current.EnemySteped -= StepByEnemy;
            
            _enemiesStepEnumerator.MoveNext();
            
            if (_enemiesStepEnumerator.Current)
            {
                _enemiesStepEnumerator.Current.EnemySteped += StepByEnemy;
                _enemiesStepEnumerator.Current.Step();
            }
            else
            {
                AllEnemySteped?.Invoke();
            }
        }

        public void RemoveEnemy(Enemy enemy)
        {
            _gameContainer.Location.EnemiesContainer.Remove(enemy);
            _enemies.Remove(enemy);
            enemy.Destroyed -= RemoveEnemy;
        }
    }
}