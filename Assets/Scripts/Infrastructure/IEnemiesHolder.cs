using System;
using System.Collections.Generic;
using Entities.Enemies;

namespace Infrastructure
{
    public interface IEnemiesHolder
    {
        void Add(Enemy enemy);
        void Remove(Enemy enemy);
        List<Enemy> Enemies { get; }
    }
}