using System;
using Infrastructure;
using Infrastructure.StaticData.Buffs;
using Infrastructure.StaticData.Enemies;
using Zenject;
using Random = UnityEngine.Random;

namespace Entities.Enemies
{
    public class HomelessBandit : Enemy
    {
        public override event Action<Enemy> EnemyStepped;
        
        private IPlayerHolder _playerHolder;
        private IVisualEffectFactory _visualEffectFactory;
        private int _damage;

        private int _counterFear;

        [Inject]
        private void Inject(IPlayerHolder playerHolder, IVisualEffectFactory visualEffectFactory)
        {
            _playerHolder = playerHolder;
            _visualEffectFactory = visualEffectFactory;
        }

        protected override void OnStep()
        {
            var random = Random.Range(0, 100f);
            switch (random)
            {
                case > 50 when _counterFear == 0:
                    ApplyFear();
                    _counterFear = 3;
                    break;
                case > 50 when EntityHealth.Health < 8 && _counterFear != 0:
                case < 25 when EntityHealth.Health < 8:
                    VampireBite();
                    break;
                default:
                    Attack();
                    break;
            }
            
            if (_counterFear > 0)
                _counterFear -= 1;
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
