using UnityEngine;

namespace Entities.Animations
{
    public static class AnimationNames
    {
        public static readonly int SelectBool = Animator.StringToHash("Selected");
        public static readonly int DeathAnimation = Animator.StringToHash("Death");
        public static readonly PhaseAnimation AttackAnimation = new PhaseAnimation("Attack");
    }

    public struct PhaseAnimation
    {
        public readonly int Trigger;
        public readonly int FirstPhaseAnimation;
        public readonly int SecondPhaseAnimation;

        public PhaseAnimation(int trigger, int firstPhase, int secondPhase)
        {
            Trigger = trigger;
            FirstPhaseAnimation = firstPhase;
            SecondPhaseAnimation = secondPhase;
        }

        public PhaseAnimation(string triggerName)
        {
            Trigger = Animator.StringToHash(triggerName);
            FirstPhaseAnimation = Animator.StringToHash($"{triggerName} First Phase");
            SecondPhaseAnimation = Animator.StringToHash($"{triggerName} Second Phase");
        }
    } 
}