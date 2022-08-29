using System;
using Entities.Buffs;
using Infrastructure.Factories;
using TMPro;
using UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IAnimatorStateListener
    {
        public event Action<AnimatorStateInfo> StateEntered;
        public event Action<AnimatorStateInfo> StateExited;

        [FormerlySerializedAs("Animator")] [SerializeField] private Animator _animator;
        [SerializeField] private BarValueView _healthBar;
        [SerializeField] private TextValueView _healthText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private BuffsHolder _buffsHolder;
        private BoundedValue _health;
        
        public BuffsHolder BuffsHolder => _buffsHolder;
        public Animator Animator => _animator;
        public BoundedValue Health => _health;
        
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