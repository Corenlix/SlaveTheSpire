using System;

namespace Entities
{
    public class EntityHealth
    {
        public int Health { get; private set; }
        public int MaxHealth { get; }
        public int Shield { get; private set; }

        public EntityHealth(int health, int maxHealth, int shield)
        {
            MaxHealth = maxHealth;
            Health = health;
            Shield = shield;

            if (health > maxHealth)
                throw new ArgumentOutOfRangeException();
           
            if (health <= 0)
                throw new ArgumentOutOfRangeException();
            
            if (shield < 0)
                throw new ArgumentOutOfRangeException();
        }

        public void TakeDamage(int damage)
        {
            HealthTakeDamage(damage);
            ShieldTakeDamage(damage);
        }

        private void HealthTakeDamage(int damage)
        {
            damage -= Shield;
            if (damage > 0)
                Health -= damage;
            
            if(Health < 0)
                Health = 0;
        }

        private void ShieldTakeDamage(int damage)
        {
            Shield -= damage;
            if (Shield < 0)
                Shield = 0;
        }
    }
}