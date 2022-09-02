using System;
using Infrastructure;
using Infrastructure.StaticData.Enemies;
using Zenject;

namespace Entities.Enemies
{
    public class TestEnemy : Enemy
    {
        public override event Action<Enemy> EnemyStepped;

        private IPlayerHolder _playerHolder;
        private int _damage;

        [Inject]
        private void Inject(IPlayerHolder playerHolder)
        {
            _playerHolder = playerHolder;
        }

        public override void Init(EnemyStaticData enemyStaticData)
        {
            var data = (TestEnemyStaticData) enemyStaticData;
            base.Init(data.MaxHealth, data.MaxHealth, data.Name, data.Armor, data.Initiative, data.AttackPower);
            _damage = data.Damage;
        }

        protected override void OnStep()
        {
            Animator.PlayAttackAnimation(OnAttack, OnEndAttack);
        }

        private void OnAttack()
        {
            AttackProcessor.Attack(_damage, _playerHolder.Player);
            
        }

        private void OnEndAttack()
        {
            EnemyStepped?.Invoke(this);
        }
    }
}