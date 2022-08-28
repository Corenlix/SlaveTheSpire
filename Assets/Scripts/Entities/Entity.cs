using System;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI _nameText;
        private BoundedValue _health;

        protected void InitView(BoundedValue health, string name)
        {
            _health = health;
            _healthBar.Init(_health);
            _healthText.Init(_health);
            _nameText.text = name;
        }

        public void TakeDamage(int value)
        {
            _health.Subtract(value);
        }

        public void Select()
        {
            Animator.SetBool(AnimationNames.SelectBool, true);
        }

        public void Deselect()
        {
            Animator.SetBool(AnimationNames.SelectBool, false);
        }

        public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => 
            StateEntered?.Invoke(stateInfo);

        public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            StateExited?.Invoke(stateInfo);
    }
}