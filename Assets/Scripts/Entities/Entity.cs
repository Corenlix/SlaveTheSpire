using UIElements;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private BarValueView _healthBar;
        [SerializeField] private TextValueView _healthText;
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
        }
    }
}