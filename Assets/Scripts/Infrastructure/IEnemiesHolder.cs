using Entities;

namespace Infrastructure
{
    public interface IEnemiesHolder
    {
        void Add(Enemy enemy);
        void Step();
        void Remove(Enemy enemy);
    }
}