using System;

namespace Entities.Animations
{
    public class EntityAnimatorFacade
    {
        private readonly EntityAnimator _entityAnimator;

        public EntityAnimatorFacade(EntityAnimator entityAnimator)
        {
            _entityAnimator = entityAnimator;
        }
        
        public void SelectState()
        {
            _entityAnimator.SetBool(AnimationNames.SelectBool, true);
        }

        public void DeselectState()
        {
            _entityAnimator.SetBool(AnimationNames.SelectBool, false);
        }

        public void PlayAttackAnimation(Action onAttack, Action onEndAttack)
        {
            _entityAnimator.PlayPhaseAnimationWithActions(AnimationNames.AttackAnimation, onAttack, onEndAttack);
        }
    }
}