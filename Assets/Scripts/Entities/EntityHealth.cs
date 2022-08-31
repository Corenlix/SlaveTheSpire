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

        public void TakeHeal(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException();

            Health = Math.Clamp(Health + amount, 0, MaxHealth);
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException();
            
            HealthTakeDamage(damage);
            ShieldTakeDamage(damage);
        }

        private void HealthTakeDamage(int damage)
        {
            damage -= Shield;
            if (damage > 0)
                Health = Math.Clamp(Health - damage, 0, MaxHealth);
        }

        private void ShieldTakeDamage(int damage)
        {
            Shield -= damage;
            if (Shield < 0)
                Shield = 0;
        }

        public void TakeShield(int amount)
        {
            Shield += amount;
        }

        public void TakeShieldDamage(int amount)
        {
            Shield -= amount;
            if (Shield < 0)
                Shield = 0;
        }
    }
}