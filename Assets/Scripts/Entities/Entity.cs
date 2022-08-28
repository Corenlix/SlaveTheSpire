using System;
using UIElements;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour, IAnimatorStateListener
    {
        public event Action<AnimatorStateInfo> StateEntered;
        public event Action<AnimatorStateInfo> StateExited;

        public event Action PlayEffect; 

        [SerializeField] protected Animator Animator;
        [SerializeField] private BarValueView _healthBar;
        [SerializeField] private TextValueView _healthText;
        [SerializeField] private EffectView _effectPrefab;
        [SerializeField] private Transform _effectPosition;
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
            var effectView = Instantiate(_effectPrefab, _effectPosition.position, Quaternion.identity);
            effectView.Init(value);
        }

        public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => 
            StateEntered?.Invoke(stateInfo);

        public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            StateExited?.Invoke(stateInfo);
    }
}