using System;
using Entities.Animations;
using Entities.Buffs;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public event Action<Entity> EntityInited;
        public event Action<Entity> EntityStepStarted;

        public BuffsHolderFacade BuffsHolderFacade => _buffsHolderFacade;
        public EntityAnimatorFacade Animator => _entityAnimatorFacade;
        public AttackProcessor AttackProcessor => _attackProcessor;
        public EntityHealth EntityHealth => _entityHealth;
        public string Name => _name;

        [SerializeField] private EntityAnimator _animator;
        [SerializeField] private BuffsHolder _buffsHolder;
        private readonly AttackProcessor _attackProcessor = new();
        private EntityAnimatorFacade _entityAnimatorFacade;
        private BuffsHolderFacade _buffsHolderFacade;
        private EntityHealth _entityHealth;
        private string _name;

        protected void Init(int health, int maxHealth, string name, int shield)
        {
            _entityHealth = new EntityHealth(health, maxHealth, shield);
            _entityAnimatorFacade = new EntityAnimatorFacade(_animator);
            _buffsHolderFacade = new BuffsHolderFacade(_buffsHolder);
            _name = name;
            EntityInited?.Invoke(this);
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
            Destroy(gameObject);
        }
    }
}