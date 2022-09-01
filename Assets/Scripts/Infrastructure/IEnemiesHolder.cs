using System;
using System.Collections.Generic;
using Entities.Enemies;

namespace Infrastructure
{
    public interface IEnemiesHolder
    {
        event Action AllEnemiesStepped;
        void Add(Enemy enemy);
        void Step();
        void Remove(Enemy enemy);
        List<Enemy> Enemies { get; }
    }
}