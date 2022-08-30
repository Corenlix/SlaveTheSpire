using Entities.Animations;
using Entities.Buffs;
using TMPro;
using UIElements;
using UnityEngine;
namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] private EntityAnimator _animator;
        [SerializeField] private BarValueView _healthBar;
        [SerializeField] private TextValueView _healthText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private BuffsHolder _buffsHolder;
        private BoundedValue _health;
        private int _shield;

        public BuffsHolder BuffsHolder => _buffsHolder;
        public EntityAnimator Animator => _animator;
        
        protected void Init(int health, int maxHealth, string name, int shield)
        {
            _health = new BoundedValue(health, maxHealth);
            _healthBar.Init(_health);
            _healthText.Init(_health);
            _nameText.text = name;
            _shield = shield;
        }

        public void TakeDamage(int damage)
        {
            if (_shield < damage)
            {
                damage -= _shield;
                _shield = 0;
            }
            else
            {
                _shield -= damage;
                damage = 0;
            }
            
            if(_health.CurrentValue <= damage)
            {
                _animator.PlayAnimation(AnimationNames.DeathAnimation, OnDie);
                _health.Subtract(_health.CurrentValue);
            }
            else
            {
                _health.Subtract(damage);
            }
            
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

        private void OnDie()
        {
            Destroy(gameObject);
        }
    }
}