using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities.Animations
{
    [RequireComponent(typeof(Animator))]
    public class EntityAnimator : MonoBehaviour, IAnimatorStateListener
    {
        public event Action<AnimatorStateInfo> StateEntered;
        public event Action<AnimatorStateInfo> StateExited;

        private Animator _animator;
        private List<AnimationAction?> _animationActions;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetBool(int boolHashName, bool value)
        {
            _animator.SetBool(boolHashName, value);
        }

        public void PlayPhaseAnimationWithActions(PhaseAnimation phaseAnimation, Action firstPhaseAction, Action secondPhaseAction)
        {
            _animator.SetTrigger(phaseAnimation.Trigger);
            _animationActions = new List<AnimationAction?>
            {
                new AnimationAction(phaseAnimation.FirstPhaseAnimation, firstPhaseAction),
                new AnimationAction(phaseAnimation.SecondPhaseAnimation, secondPhaseAction),
            };
        }

        public void PlayAnimationWithAction(int triggerHashName, Action animationAction)
        {
            _animator.SetTrigger(triggerHashName);
            _animationActions = new List<AnimationAction?>
            {
                new AnimationAction(triggerHashName, animationAction),
            };
        }

        private void OnStateEnd(AnimatorStateInfo animatorStateInfo)
        {
            var action = _animationActions?.FirstOrDefault(animationAction =>
                animationAction?.StateNameHash == animatorStateInfo.shortNameHash);
            if(action != null)
            {
                action.Value.StateAction?.Invoke();
                _animationActions.Remove(action);
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

    struct AnimationAction
    {
        public readonly int StateNameHash;
        public readonly Action StateAction;

        public AnimationAction(int stateNameHash, Action stateAction)
        {
            StateNameHash = stateNameHash;
            StateAction = stateAction;
        }
    }
}