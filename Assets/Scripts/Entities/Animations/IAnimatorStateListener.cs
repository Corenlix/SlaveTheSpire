using UnityEngine;

namespace Entities.Animations
{
    public interface IAnimatorStateListener
    {
        void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
        void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    }
}