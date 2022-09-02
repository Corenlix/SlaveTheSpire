using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Enemies;

namespace Infrastructure
{
    public class EnemiesHolder : IEnemiesHolder
    {
        public event Action AllEnemiesStepped;

        public List<Enemy> Enemies => _enemies.ToList();

        private readonly LocationHolder _locationHolder;
        private readonly List<Enemy> _enemies = new();
        private IEnumerator<Enemy> _enemiesStepEnumerator;
        
        public EnemiesHolder(LocationHolder locationHolder)
        {
            _locationHolder = locationHolder;
        }

        public void Add(Enemy enemy)
        {
            _enemies.Add(enemy);
            _locationHolder.Location.EnemiesContainer.Add(enemy);
            enemy.Destroyed += Remove;
        }

        public void Step()
        {
            _enemiesStepEnumerator = _enemies.GetEnumerator();
            StepByEnemy();
        }

        private void StepByEnemy()
        {
            _enemiesStepEnumerator.MoveNext();
            
            if (_enemiesStepEnumerator.Current)
            {
                _enemiesStepEnumerator.Current.EnemyStepped += OnEnemyStepped;
                _enemiesStepEnumerator.Current.Step();
            }
            else
            {
                AllEnemiesStepped?.Invoke();
            }
        }

        private void OnEnemyStepped(Enemy enemy)
        {
            enemy.EnemyStepped -= OnEnemyStepped;
            StepByEnemy();
        }

        public void Remove(Enemy enemy)
        {
            _locationHolder.Location.EnemiesContainer.Remove(enemy);
            _enemies.Remove(enemy);
            enemy.Destroyed -= Remove;
        }
    }
}