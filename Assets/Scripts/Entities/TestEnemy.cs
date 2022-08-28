using System;
using Infrastructure;
using Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class TestEnemy : Enemy
    {
        public override event Action EnemyStepped;

        private static readonly int Attack = Animator.StringToHash("Attack");
        private int _damage;
        private IPlayerHolder _playerHolder;

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
            Animator.SetTrigger(Attack);
            StateExited += OnStateExited;
        }

        private void OnStateExited(AnimatorStateInfo animatorStateInfo)
        {
            if(animatorStateInfo.shortNameHash == Attack)
                OnAttack();
        }

        private void OnAttack()
        {
            _playerHolder.Health.Subtract(_damage);
            StateExited -= OnStateExited;
            EnemyStepped?.Invoke();
        }
    }
}