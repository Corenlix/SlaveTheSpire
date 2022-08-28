using System;
using Infrastructure;
using Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class DefaultEnemy : Enemy
    {
        public override event Action EnemyStepped;

        private IPlayerHolder _playerHolder;
        private int _damage;

        [Inject]
        private void Inject(IPlayerHolder playerHolder)
        {
            _playerHolder = playerHolder;
        }
        
        protected override void OnInit(EnemyStaticData staticData)
        {
            var testEnemyStaticData = (TestEnemyStaticData) staticData;
            _damage = testEnemyStaticData.Damage;
        }

        protected override void OnStep()
        {
            Animator.SetTrigger(AnimationNames.AttackTrigger);
            StateExited += OnStateExited;
        }

        private void OnStateExited(AnimatorStateInfo animatorStateInfo)
        {
            if (animatorStateInfo.shortNameHash == AnimationNames.FirstPhaseAttack)
                OnAttack();
            else if (animatorStateInfo.shortNameHash == AnimationNames.SecondPhaseAttack)
                OnEndAttack();
        }

        private void OnAttack()
        {
            _playerHolder.Health.Subtract(_damage);
        }

        private void OnEndAttack()
        {
            StateExited -= OnStateExited;
            EnemyStepped?.Invoke();
        }
    }
}