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

        public int ApplyHeal(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException();

            int healthToMax = MaxHealth - Health;
            int delta = healthToMax > amount ? amount : healthToMax;
            Health += delta;
            Changed?.Invoke(this);
            
            return delta;
        }

        public int ApplyDamage(int damage)
        {
            int damagedToArmor = ArmorDamage(damage);
            int damagedToHealth = HealthDamage(damage - damagedToArmor);

            return damagedToArmor + damagedToHealth;
        }

        public void ApplyDamageThroughArmor(int damage)
        {
            HealthDamage(damage);
        }

        public void AddArmor(int amount)
        {
            Armor += amount;
            Changed?.Invoke(this);
        }
        
        private int HealthDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException();

            int damageToDeal = damage < Health ? damage : Health;
            Health -= damageToDeal;
            Changed?.Invoke(this);
            
            return damageToDeal;
        }

        private int ArmorDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException();

            int damageToDeal = damage < Armor ? damage : Armor;
            Armor -= damageToDeal;
            Changed?.Invoke(this);

            return damageToDeal;
        }
    }
}