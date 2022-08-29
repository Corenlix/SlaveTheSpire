using System;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.StaticData;
using Zenject;
using Random = UnityEngine.Random;

namespace Entities
{
    public class Enemy : Entity
    {
        public event Action EnemyStepped;
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
            Init(staticData.MaxHealth, staticData.MaxHealth, staticData.Name);
            _enemyActions = staticData.GetEnemyActions(_diContainer, this);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        protected override void OnStep()
        {
            var action = _enemyActions[Random.Range(0, _enemyActions.Count)];
            action.ActionEnded += OnActionEnd;
            action.Use();
        }

        private void OnActionEnd(IEnemyAction enemyAction)
        {
            enemyAction.ActionEnded -= OnActionEnd;
            EnemyStepped?.Invoke();
        }
    }
}
