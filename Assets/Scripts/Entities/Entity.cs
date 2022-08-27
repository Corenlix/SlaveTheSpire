using System;
using UIElements;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private BarValueView _healthBar;
        [SerializeField] private TextValueView _healthText;
        private BoundedValue _health;

        protected void InitHealth(int maxHealth)
        {
            _health = new BoundedValue(maxHealth);
            _healthBar.Init(_health);
            _healthText.Init(_health);
        }

        public void TakeDamage(int value)
        {
            _health.Subtract(value);
        }
    }
}