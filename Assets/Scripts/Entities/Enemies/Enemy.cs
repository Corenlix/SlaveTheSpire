using System;
using Infrastructure.StaticData.Enemies;

namespace Entities.Enemies
{
    public abstract class Enemy : Entity
    {
        public abstract event Action<Enemy> EnemyStepped;
        public event Action<Enemy> Destroyed;
		
        public abstract void Init(EnemyStaticData enemyStaticData);

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}