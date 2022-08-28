using System;
using Infrastructure;
using Infrastructure.StaticData;

namespace Entities
{
    public abstract class Enemy : Entity
    {
        public abstract event Action EnemyStepped;

        public event Action<Enemy> Destroyed;

        private EnemiesHolder _enemiesHolder;
        public void Init(EnemyStaticData staticData)
        {
            InitView(new BoundedValue(staticData.MaxHealth), staticData.Name);
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
