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
        private IVisualEffectFactory _visualEffectFactory;
        private int _damage;

        [Inject]
        private void Inject(IPlayerHolder playerHolder, IVisualEffectFactory visualEffectFactory)
        {
            _playerHolder = playerHolder;
            _visualEffectFactory = visualEffectFactory;
        }

        public override void Init(EnemyStaticData enemyStaticData)
        {
            var data = (TestEnemyStaticData) enemyStaticData;
            base.Init(data.MaxHealth, data.MaxHealth, data.Name, data.Armor, data.Initiative, data.AttackPower);
            _damage = data.Damage;
        }

        public void Init(int health, string name, int armor, int initiative, int attackPower)
        {
            base.Init(health, health, name, armor, initiative, attackPower);
        }

        protected override void OnStep()
        {
            Animator.PlayAttackAnimation(OnAttack, OnEndAttack);
        }

        private void OnAttack()
        {
            AttackProcessor.Attack(_damage, _playerHolder.Player);
            _visualEffectFactory.SpawnDamageEffect(_damage, _playerHolder.Player.transform.position);
        }

        private void OnEndAttack()
        {
            EnemyStepped?.Invoke(this);
        }
    }
}