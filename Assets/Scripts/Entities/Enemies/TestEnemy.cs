using System;
using Infrastructure;
using Infrastructure.StaticData.Enemies;
using Utilities;
using Zenject;

namespace Entities.Enemies
{
    public class TestEnemy : Enemy
    {
        public override event Action<Enemy> EnemyStepped;

        private IPlayersHolder _playersHolder;
        private IVisualEffectFactory _visualEffectFactory;
        private int _damage;

        [Inject]
        private void Inject(IPlayersHolder playersHolder, IVisualEffectFactory visualEffectFactory)
        {
            _playersHolder = playersHolder;
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
            Player target = _playersHolder.Players.Random();
            AttackProcessor.Attack(_damage, target);
            _visualEffectFactory.SpawnDamageEffect(_damage, target.transform.position);
        }

        private void OnEndAttack()
        {
            EnemyStepped?.Invoke(this);
        }
    }
}