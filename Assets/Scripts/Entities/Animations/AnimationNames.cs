using UnityEngine;

namespace Entities.Animations
{
    public static class AnimationNames
    {
        public static readonly int SelectBool = Animator.StringToHash("Selected");
        public static readonly int AttackTrigger = Animator.StringToHash("Attack");
        public static readonly int FirstPhaseAttack = Animator.StringToHash("First Phase Attack");
        public static readonly int SecondPhaseAttack = Animator.StringToHash("Second Phase Attack");
    }
}