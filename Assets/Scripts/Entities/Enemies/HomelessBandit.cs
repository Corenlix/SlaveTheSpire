using System;
using Infrastructure;
using Infrastructure.StaticData.Enemies;
using Zenject;

namespace Entities.Enemies
{
    public class HomelessBandit : Enemy
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

        protected override void OnStep()
        {
            Animator.PlayAttackAnimation(OnAttack, OnEndAttack);
        }

        public override void Init(EnemyStaticData enemyStaticData)
        {
            var data = (HomelessBanditStaticData) enemyStaticData;
            base.Init(data.MaxHealth, data.MaxHealth, data.Name, data.Armor, data.Initiative, data.AttackPower);
            _damage = data.Damage;
        }

        private void  OnAttack()
        {
            AttackProcessor.Attack(_damage, _playerHolder.Player );
            _visualEffectFactory.SpawnDamageEffect(_damage, _playerHolder.Player.transform.position);
        }

        private void OnEndAttack()
        {
            EnemyStepped?.Invoke(this);
        }
    }
}
