using System;
using Entities.Animations;
using Entities.Buffs;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public event Action<Entity> Destroyed;
        public event Action<Entity> EntityInited;
        public event Action<Entity> EntityStepStarted;
        public event Action<Entity> Died;

        public BuffsHolderFacade BuffsHolderFacade => _buffsHolderFacade;
        public EntityAnimatorFacade Animator => _entityAnimatorFacade;
        public AttackProcessor AttackProcessor => _attackProcessor;
        public EntityHealth EntityHealth => _entityHealth;
        public string Name => _name;
        public int Initiative => _initiative;

        [SerializeField] private EntityAnimator _animator;
        [SerializeField] private BuffsHolder _buffsHolder;
        private readonly AttackProcessor _attackProcessor = new();
        private EntityAnimatorFacade _entityAnimatorFacade;
        private BuffsHolderFacade _buffsHolderFacade;
        private EntityHealth _entityHealth;
        private string _name;
		private int _initiative;
        private int _attackPower;

        protected void Init(int health, int maxHealth, string name, int armor,  int initiative, int attackPower)
        {
            _entityHealth = new EntityHealth(health, maxHealth, armor);
            _entityAnimatorFacade = new EntityAnimatorFacade(_animator);
            _buffsHolderFacade = new BuffsHolderFacade(_buffsHolder);
            _name = name;
			_initiative = initiative;
            _attackPower = attackPower;
            EntityInited?.Invoke(this);
            EntityHealth.Died += Die;
        }

        private void Die(EntityHealth obj)
        {
            EntityHealth.Died -= Die;
            Animator.PlayDeathAnimation(OnDie);
        }

        public void Step()
        {
            EntityStepStarted?.Invoke(this);
            _buffsHolder.Step();
            OnStep();
        }

        protected abstract void OnStep();
        
        private void OnDie()
        {
            Died?.Invoke(this);
            Destroy(gameObject);
        }
        
        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}