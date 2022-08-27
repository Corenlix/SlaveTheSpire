using System;
using Entities;

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