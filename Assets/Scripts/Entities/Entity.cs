using System;
using Entities.Buffs;
using TMPro;
using UIElements;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IAnimatorStateListener
    {
        public event Action<AnimatorStateInfo> StateEntered;
        public event Action<AnimatorStateInfo> StateExited;

        [SerializeField] protected Animator Animator;
        [SerializeField] private BarValueView _healthBar;
        [SerializeField] private TextValueView _healthText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private BuffsHolder _buffsHolder;
        [SerializeField] private DamageEffect damageEffectPrefab;
        [SerializeField] private Transform _damageEffectPosition;
        private BoundedValue _health;
        
        public BuffsHolder BuffsHolder => _buffsHolder;

        protected void InitView(BoundedValue health, string name)
        {
            _health = health;
            _healthBar.Init(_health);
            _healthText.Init(_health);
            _nameText.text = name;
        }

        public void Step()
        {
            OnStep();
            _buffsHolder.Step();
        }

        protected abstract void OnStep();
        
        public void TakeDamage(int value)
        {
            _health.Subtract(value);
            var effectView = Instantiate(damageEffectPrefab, _damageEffectPosition.position, Quaternion.identity);
            effectView.Init(value);
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