using Entities;

namespace Infrastructure
{
    public interface IEnemiesHolder
    {
        void AddEnemy(Enemy enemy);
        void Step();
        void RemoveEnemy(Enemy enemy);
    }
}