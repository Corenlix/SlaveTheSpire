using System;
using UnityEngine;

namespace Entities.Animations
{
    [RequireComponent(typeof(Animator))]
    public class EntityAnimator : MonoBehaviour, IAnimatorStateListener
    {
        public event Action<AnimatorStateInfo> StateEntered;
        public event Action<AnimatorStateInfo> StateExited;

        private Animator _animator;
        private PhaseAnimation? _playingPhaseAnimation;
        private Action _firstPhaseAction;
        private Action _secondPhaseAction;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetBool(int boolHashName, bool value)
        {
            _animator.SetBool(boolHashName, value);
        }

        public void PlayAnimation(PhaseAnimation phaseAnimation, Action firstPhaseAction, Action secondPhaseAction)
        {
            _animator.SetTrigger(phaseAnimation.Trigger);
            _firstPhaseAction = firstPhaseAction;
            _secondPhaseAction = secondPhaseAction;
            _playingPhaseAnimation = phaseAnimation;
        }

        private void OnStateEnd(AnimatorStateInfo animatorStateInfo)
        {
            if (animatorStateInfo.shortNameHash == _playingPhaseAnimation?.FirstPhaseAnimation)
            {
                _firstPhaseAction?.Invoke();
                _firstPhaseAction = null;
            }
            else if (animatorStateInfo.shortNameHash == _playingPhaseAnimation?.SecondPhaseAnimation)
            {
                _secondPhaseAction?.Invoke();
                _secondPhaseAction = null;
                _playingPhaseAnimation = null;
            }
        }

        public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            StateEntered?.Invoke(stateInfo);

        public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            StateExited?.Invoke(stateInfo);
            OnStateEnd(stateInfo);
        }
    }
}