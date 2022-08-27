using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        private BoundedValue _health;

        private void Start()
        {
            _health = new BoundedValue(_maxHealth);
        }

        public void TakeDamage(int value)
        {
            _health.Subtract(value);
        }
    }
}