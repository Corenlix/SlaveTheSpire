using System.Collections.Generic;
using Entities;

namespace Infrastructure
{
    public class EnemiesHolder : IEnemiesHolder
    {
        private readonly GameContainer _gameContainer;
        private readonly List<Enemy> _enemies = new();

        public EnemiesHolder(GameContainer gameContainer)
        {
            _gameContainer = gameContainer;
        }

        public void Add(Enemy enemy)
        {
            _enemies.Add(enemy);
            _gameContainer.Location.EnemiesContainer.Add(enemy);
            enemy.Destroyed += Remove;
        }

        public void Step()
        {
            Step(_enemies.GetEnumerator());
        }

        private void Step(IEnumerator<Enemy> byEnemies)
        {
            byEnemies.MoveNext();
            
            if (byEnemies.Current)
            {
                byEnemies.Current.Step();
                //подписываешься на событие врага тип когда он подходит => когда походит вызываешь Step(byEnemies)
                Step(byEnemies);
            }
            else
            {
                //вызываешь событие что все походили
            }
        }

        public void Remove(Enemy enemy)
        {
            _gameContainer.Location.EnemiesContainer.Remove(enemy);
            _enemies.Remove(enemy);
            enemy.Destroyed -= Remove;
        }
    }
}