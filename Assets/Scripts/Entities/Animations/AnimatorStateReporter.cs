using UnityEngine;

namespace Entities.Animations
{
    public class AnimatorStateReporter : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            if (animator.gameObject.TryGetComponent(out IAnimatorStateListener observable))
            {
                observable.OnStateEnter(animator, stateInfo, layerIndex);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            if (animator.gameObject.TryGetComponent(out IAnimatorStateListener observable))
            {
                observable.OnStateExit(animator, stateInfo, layerIndex);
            }
        }
    }
}