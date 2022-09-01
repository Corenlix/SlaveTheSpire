using System;
using System.Collections.Generic;
using Entities.Animations;
using Entities.Buffs;
using Infrastructure.StaticData.Buffs;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public event Action<Entity> EntityInited;
        public event Action<Entity, int> EntityDamaged;
        public event Action<Entity, int> EntityHealed;
        public event Action<Entity> EntityGotArmor;
        public event Action<Entity> EntityStepStarted;
        [SerializeField] private EntityAnimator _animator;
        [SerializeField] private BuffsHolder _buffsHolder;

        public string Name => _name;
        public int MaxHealth => _entityHealth.MaxHealth;
        public int Health => _entityHealth.Health;
        public int Armor => _entityHealth.Armor;

        public int BonusDamage
        {
            get => _attackProcessor.BonusDamage;
            set => _attackProcessor.BonusDamage = value;
        }

        public float DamageMultiplier
        {
            get => _attackProcessor.DamageMultiplier;
            set => _attackProcessor.DamageMultiplier = value;
        }
        
        private readonly AttackProcessor _attackProcessor = new AttackProcessor();
        private EntityHealth _entityHealth;
        private string _name;

        protected void Init(int health, int maxHealth, string name, int shield)
        {
            _entityHealth = new EntityHealth(health, maxHealth, shield);
            _name = name;
            EntityInited?.Invoke(this);
        }

        public void PlayAnimationWithAction(int triggerHashName, Action animationAction) =>
            _animator.PlayAnimationWithAction(triggerHashName, animationAction);

        public void PlayPhaseAnimation(PhaseAnimation phaseAnimation, Action firstPhaseAction, Action secondPhaseAction) =>
            _animator.PlayPhaseAnimationWithActions(phaseAnimation, firstPhaseAction, secondPhaseAction);

        public void Select()
        {
            _animator.SetBool(AnimationNames.SelectBool, true);
        }

        public void Deselect()
        {
            _animator.SetBool(AnimationNames.SelectBool, false);
        }

        public void Attack(List<Entity> targets, int damage)
        {
            _attackProcessor.Attack(targets, damage);
        }

        public void ApplyDamage(int amount)
        {
            _entityHealth.ApplyDamage(amount);
            EntityDamaged?.Invoke(this, amount);
            
            if(_entityHealth.Health == 0)
            {
                _animator.PlayAnimationWithAction(AnimationNames.DeathAnimation, OnDie);
            }
        }

        public void ApplyDamageThroughArmor(int amount)
        {
            _entityHealth.ApplyDamageThroughArmor(amount);
            EntityDamaged?.Invoke(this, amount);
        }
        
        public void AddDamageProcessor(DamageProcessor processor) => _attackProcessor.AddDamageProcessor(processor);
        
        public void RemoveDamageProcessor(DamageProcessor processor) => _attackProcessor.RemoveDamageProcessor(processor);

        public void ApplyHeal(int amount)
        {
            _entityHealth.ApplyHeal(amount);
            EntityHealed?.Invoke(this, amount);
        }

        public void AddArmor(int amount)
        {
            _entityHealth.AddArmor(amount);
            EntityGotArmor?.Invoke(this);
        }

        public void AddBuff(BuffId buffId, int steps) => _buffsHolder.Add(buffId, steps);
        
        public void Step()
        {
            EntityStepStarted?.Invoke(this);
            _buffsHolder.Step();
            OnStep();
        }

        protected abstract void OnStep();
        
        private void OnDie()
        {
            Destroy(gameObject);
        }
    }
}