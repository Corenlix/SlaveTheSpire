using System;
using Infrastructure.StaticData;

namespace Entities
{
    public abstract class Enemy : Entity
    {
        public event Action<Enemy> Destroyed;

        public void Init(EnemyStaticData staticData)
        {
            InitHealth(staticData.MaxHealth);
            OnInit(staticData);
        }
        
        protected abstract void OnInit(EnemyStaticData staticData);

        public abstract void Step();

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
