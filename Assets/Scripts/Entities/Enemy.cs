using System;
using Infrastructure.StaticData;

namespace Entities
{
    public abstract class Enemy : Entity
    {
        public event Action<Enemy> Destroyed;

        public abstract void Init(EnemyStaticData staticData);
        
        public abstract void Step();

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
