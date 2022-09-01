using System;

namespace Entities
{
    public class EntityHealth
    {
        public event Action<EntityHealth> Changed;
        
        public int Health { get; private set; }
        public int MaxHealth { get; }
        public int Armor { get; private set; }

        public EntityHealth(int health, int maxHealth, int armor)
        {
            MaxHealth = maxHealth;
            Health = health;
            Armor = armor;

            if (health > maxHealth)
                throw new ArgumentOutOfRangeException();
           
            if (health <= 0)
                throw new ArgumentOutOfRangeException();
            
            if (armor < 0)
                throw new ArgumentOutOfRangeException();
        }

        public void ApplyHeal(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException();

            Health = Math.Clamp(Health + amount, 0, MaxHealth);
            Changed?.Invoke(this);
        }

        public void ApplyDamage(int damage)
        {
            HealthDamage(AbsorbDamageByArmor(damage));
        }

        public void ApplyDamageThroughArmor(int damage)
        {
            HealthDamage(damage);
        }

        public void AddArmor(int amount)
        {
            Armor += amount;
        }
        
        private void HealthDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException();
            
            Health = Math.Clamp(Health - damage, 0, MaxHealth);
            Changed?.Invoke(this);
        }

        private int AbsorbDamageByArmor(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException();
            
            var remainDamage = damage - Armor;
            if (remainDamage < 0)
                remainDamage = 0;

            Armor -= damage;
            if (Armor < 0)
                Armor = 0;
            
            Changed?.Invoke(this);

            return remainDamage;
        }
    }
}