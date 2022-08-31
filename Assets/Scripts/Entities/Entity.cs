using System;
using Entities.Animations;
using Entities.Buffs;
using Infrastructure.StaticData.Buffs;
using UnityEngine;
namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public event Action<Entity> EntityUpdated;
        [SerializeField] private EntityAnimator _animator;
        [SerializeField] private BuffsHolder _buffsHolder;

        public string Name => _name;
        public int MaxHealth => _entityHealth.MaxHealth;
        public int Health => _entityHealth.Health;
        public int Shield => _entityHealth.Shield;
        
        private EntityHealth _entityHealth;
        private string _name;

        protected void Init(int health, int maxHealth, string name, int shield)
        {
            _entityHealth = new EntityHealth(health, maxHealth, shield);
            _name = name;
            EntityUpdated?.Invoke(this);
        }

        public void PlayAnimationWithAction(int triggerHashName, Action animationAction) =>
            _animator.PlayAnimationWithAction(triggerHashName, animationAction);

        public void PlayPhaseAnimation(PhaseAnimation phaseAnimation, Action firstPhaseAction, Action secondPhaseAction) =>
            _animator.PlayPhaseAnimationWithActions(phaseAnimation, firstPhaseAction, secondPhaseAction);

        public void Select() =>_animator.SetBool(AnimationNames.SelectBool, true);

        public void Deselect() =>_animator.SetBool(AnimationNames.SelectBool, false);

        public void TakeDamage(int amount)
        {
            _entityHealth.TakeDamage(amount);
            EntityUpdated?.Invoke(this);
            
            if(_entityHealth.Health == 0)
            {
                _animator.PlayAnimationWithAction(AnimationNames.DeathAnimation, OnDie);
            }
        }

        public void TakeHeal(int amount)
        {
            _entityHealth.TakeHeal(amount);
            EntityUpdated?.Invoke(this);
        }

        public void TakeShield(int amount)
        {
            _entityHealth.TakeShield(amount);
            EntityUpdated?.Invoke(this);
        }

        public void TakeShieldDamage(int amount)
        {
            _entityHealth.TakeShieldDamage(amount);
            EntityUpdated?.Invoke(this);
        }

        public void AddBuff(BuffId buffId, int steps) => _buffsHolder.Add(buffId, steps);
        
        public void Step()
        {
            OnStep();
            _buffsHolder.Step();
        }

        protected abstract void OnStep();
        
        private void OnDie()
        {
            Destroy(gameObject);
        }
    }
}