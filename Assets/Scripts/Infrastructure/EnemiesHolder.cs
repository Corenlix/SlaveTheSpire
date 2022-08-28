using System;
using System.Collections.Generic;
using Entities;

namespace Infrastructure
{
    public class EnemiesHolder : IEnemiesHolder
    {
        public event Action AllEnemiesStepped;

        private readonly SceneContainer _sceneContainer;
        private readonly List<Enemy> _enemies = new();
        private IEnumerator<Enemy> _enemiesStepEnumerator;

        public EnemiesHolder(SceneContainer sceneContainer)
        {
            _sceneContainer = sceneContainer;
        }

        public void Add(Enemy enemy)
        {
            _enemies.Add(enemy);
            _sceneContainer.Location.EnemiesContainer.Add(enemy);
            enemy.Destroyed += Remove;
        }

        public void Step()
        {
            _enemiesStepEnumerator = _enemies.GetEnumerator();
            StepByEnemy();
        }

        private void StepByEnemy()
        {
            if(_enemiesStepEnumerator.Current != null)
                _enemiesStepEnumerator.Current.EnemyStepped -= StepByEnemy;
            
            _enemiesStepEnumerator.MoveNext();
            
            if (_enemiesStepEnumerator.Current)
            {
                _enemiesStepEnumerator.Current.EnemyStepped += StepByEnemy;
                _enemiesStepEnumerator.Current.Step();
            }
            else
            {
                AllEnemiesStepped?.Invoke();
            }
        }

        public void Remove(Enemy enemy)
        {
            _sceneContainer.Location.EnemiesContainer.Remove(enemy);
            _enemies.Remove(enemy);
            enemy.Destroyed -= Remove;
        }
    }
}