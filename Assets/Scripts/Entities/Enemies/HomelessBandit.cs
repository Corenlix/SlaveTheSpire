using System;
using Infrastructure;
using Infrastructure.StaticData.Buffs;
using Infrastructure.StaticData.Enemies;
using Utilities;
using Zenject;
using Random = UnityEngine.Random;

namespace Entities.Enemies
{
    public class HomelessBandit : Enemy
    {
        public override event Action<Enemy> EnemyStepped;
        
        private IPlayersHolder _playersHolder;
        private IVisualEffectFactory _visualEffectFactory;
        private int _damage;
        private int _stealHealthDamage = 1;
        private int _counterFear;

        [Inject]
        private void Inject(IPlayersHolder playersHolder, IVisualEffectFactory visualEffectFactory)
        {
            _playersHolder = playersHolder;
            _visualEffectFactory = visualEffectFactory;
        }

        public override void Init(EnemyStaticData enemyStaticData)
        {
            var data = (HomelessBanditStaticData) enemyStaticData;
            base.Init(data.MaxHealth, data.MaxHealth, data.Name, data.Armor, data.Initiative, data.AttackPower);
            _damage = data.Damage;
        }

        protected override void OnStep()
        {
            var random = Random.Range(0, 100f);
            switch (random)
            {
                case < 50 when _counterFear == 0:
                    ApplyFear();
                    _counterFear = 3;
                    break;
                case < 50 when EntityHealth.Health < 8 && _counterFear != 0:
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
            _playersHolder.Players.Random().BuffsHolderFacade.AddBuff(BuffId.Fear,2);
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
            AttackProcessor.Attack(_stealHealthDamage, _playersHolder.Players.Random());
             EntityHealth.ApplyHeal(_stealHealthDamage);
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

        private void  OnAttack()
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
