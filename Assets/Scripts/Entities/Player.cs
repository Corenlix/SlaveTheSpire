using System;

namespace Entities
{
    public class Player : Entity
    {
        public event Action<Player> PlayerUpdated;

        public int MaxEnergy => _energy.MaxEnergy;
        public int Energy => _energy.Energy;
        
        private PlayerEnergy _energy;
        
        public void Init(int energy, int maxEnergy, int health, int maxHealth, string name, int shield)
        {
            _energy = new PlayerEnergy(energy, maxEnergy);
            base.Init(health, maxHealth, name, shield);
            PlayerUpdated?.Invoke(this);
        }

        public void SubtractEnergy(int amount)
        {
            _energy.Subtract(amount);
            PlayerUpdated?.Invoke(this);
        }

        public void RefreshEnergy()
        {
            _energy.Refresh();
            PlayerUpdated?.Invoke(this);
        }

        protected override void OnStep()
        {
        }
    }
}