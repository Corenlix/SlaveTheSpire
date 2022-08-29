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
        
        protected override void OnInit(EnemyStaticData staticData)
        {
            var testEnemyStaticData = (DefaultEnemyStaticData) staticData;
        }

        //protected override void OnStep()
        //{
        //    Animator.SetTrigger(AnimationNames.AttackTrigger);
        //    StateExited += OnStateExited;
        //}

        private void OnStateExited(AnimatorStateInfo animatorStateInfo)
        {
            if (animatorStateInfo.shortNameHash == AnimationNames.FirstPhaseAttack)
                OnAttack();
            else if (animatorStateInfo.shortNameHash == AnimationNames.SecondPhaseAttack)
                OnEndAttack();
        }

        private void OnAttack()
        {
            
        }

        private void OnEndAttack()
        {
            StateExited -= OnStateExited;
            EnemyStepped?.Invoke();
        }
    }
}