using System.Collections.Generic;
using Entities;

namespace Infrastructure
{
    public class EnemiesHolder : IEnemiesHolder
    {
        private readonly List<Enemy> _enemies = new();

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
            enemy.Destroyed += RemoveEnemy;
        }

        public void Step()
        {
            _enemies.ForEach(x=>x.Step());
        }

        public void RemoveEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
            enemy.Destroyed -= RemoveEnemy;
        }
    }
}