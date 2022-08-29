using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Infrastructure;
using Infrastructure.StaticData;
using Zenject;

namespace Entities
{
    public abstract class Enemy : Entity
    {
        public abstract event Action EnemyStepped;
        public event Action<Enemy> Destroyed;

        private EnemiesHolder _enemiesHolder;
        private List<IEnemyAction> _enemyActions;
        private DiContainer _diContainer;
        
        [Inject]
        private void Inject(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public void Init(EnemyStaticData staticData)
        {
            InitView(new BoundedValue(staticData.MaxHealth), staticData.Name);
            OnInit(staticData);
            _enemyActions = staticData.GetEnemyActions(_diContainer);
        }
        
        protected abstract void OnInit(EnemyStaticData staticData);
        
        private void OnDestroy()
        {
            
            Destroyed?.Invoke(this);
        }

        protected override void OnStep()
        {
            _enemyActions.ForEach(x=> x.Use());
        }
    }
}
