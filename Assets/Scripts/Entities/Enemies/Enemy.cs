using System;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Factories;
using Infrastructure.StaticData.Enemies;
using Infrastructure.StaticData.Enemies.EnemiesActions;
using Zenject;
using Random = UnityEngine.Random;

namespace Entities.Enemies
{
    public class Enemy : Entity
    {
        public event Action EnemyStepped;
        public event Action<Enemy> Destroyed;

        private EnemiesHolder _enemiesHolder;
        private List<IEnemyAction> _enemyActions;
        private IEnemyActionsFactory _enemyActionsFactory;

        [Inject]
        private void Inject(IEnemyActionsFactory enemyActionsFactory)
        {
            _enemyActionsFactory = enemyActionsFactory;
        }

        public void Init(EnemyStaticData staticData)
        {
            base.Init(staticData.MaxHealth, staticData.MaxHealth, staticData.Name, staticData.Shield);
            _enemyActions = _enemyActionsFactory.GetActions(staticData, this);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        protected override void OnStep()
        {
            var action = SelectAction();
            action.ActionEnded += OnActionEnd;
            action.Use();
        }

        private IEnemyAction SelectAction()
        {
            return _enemyActions[Random.Range(0, _enemyActions.Count)];
        }

        private void OnActionEnd(IEnemyAction enemyAction)
        {
            enemyAction.ActionEnded -= OnActionEnd;
            EnemyStepped?.Invoke();
        }
    }
}