using System;
using Infrastructure;
using Infrastructure.StaticData.Buffs;
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
            ApplyFear();
        }

        private void ApplyFear()
        {
            Animator.PlayAttackAnimation(OnApplyFearEnter, OnApplyFearEnd);
           
        }

        private void OnApplyFearEnter()
        {
            _playerHolder.Player.BuffsHolderFacade.AddBuff(BuffId.Fear,2);
        }

        private void OnApplyFearEnd()
        {
            EnemyStepped?.Invoke(this);
        }
        
        private void VampireBite()
        {
            Animator.PlayAttackAnimation(OnVampireEnter, OnVampireBiteEnd);
        }

        private void OnVampireEnter()
        {
             AttackProcessor.Attack(1, _playerHolder.Player);
             EntityHealth.ApplyHeal(1);
             _visualEffectFactory.SpawnPopUp(PopUpType.Sword, transform.position);
        }

        private void OnVampireBiteEnd()
        {   
            EnemyStepped?.Invoke(this);
        }
        
        private void Attack()
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
