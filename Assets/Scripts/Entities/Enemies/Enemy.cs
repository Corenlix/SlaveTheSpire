using System;
using Infrastructure.StaticData.Enemies;

namespace Entities.Enemies
{
    public abstract class Enemy : Entity
    {
        public abstract event Action<Enemy> EnemyStepped;

        public abstract void Init(EnemyStaticData enemyStaticData);
    }
}