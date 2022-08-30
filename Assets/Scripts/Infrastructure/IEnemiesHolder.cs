using System;
using Entities;
using Entities.Enemies;

namespace Infrastructure
{
    public interface IEnemiesHolder
    {
        event Action AllEnemiesStepped;
        void Add(Enemy enemy);
        void Step();
        void Remove(Enemy enemy);
    }
}