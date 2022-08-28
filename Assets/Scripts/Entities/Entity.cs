using System;
using UIElements;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour, IAnimatorStateListener
    {
        public event Action<AnimatorStateInfo> StateEntered;
        public event Action<AnimatorStateInfo> StateExited;

        [SerializeField] protected Animator Animator;
        [SerializeField] private BarValueView _healthBar;
        [SerializeField] private TextValueView _healthText;
        private BoundedValue _health;

        protected void InitHealth(BoundedValue health)
        {
            _health = health;
            _healthBar.Init(_health);
            _healthText.Init(_health);
        }

        public void TakeDamage(int value)
        {
            _health.Subtract(value);
        }

        public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => 
            StateEntered?.Invoke(stateInfo);

        public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            StateExited?.Invoke(stateInfo);
    }
}