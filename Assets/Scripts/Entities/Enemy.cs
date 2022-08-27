using System;
using Infrastructure;
using Infrastructure.StaticData;

namespace Entities
{
    public abstract class Enemy : Entity
    {
        public abstract event Action EnemySteped;

        public event Action<Enemy> Destroyed;

        private EnemiesHolder _enemiesHolder;
        public void Init(EnemyStaticData staticData)
        {
            InitHealth(new BoundedValue(staticData.MaxHealth));
            OnInit(staticData);
        }
        
        protected abstract void OnInit(EnemyStaticData staticData);
        
        public void Step()
        {
            OnStep();
        }
        
        protected abstract void OnStep();

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
