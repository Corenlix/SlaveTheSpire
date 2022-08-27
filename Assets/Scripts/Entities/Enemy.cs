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
