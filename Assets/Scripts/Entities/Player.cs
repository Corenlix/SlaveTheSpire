using System;

namespace Entities
{
    public class Player : Entity
    {
        public event Action<Player> PlayerInited;
        
        public PlayerEnergy Energy => _energy;

        private PlayerEnergy _energy;
        
        public void Init(int energy, int maxEnergy, int health, int maxHealth, string name, int shield)
        {
            _energy = new PlayerEnergy(energy, maxEnergy);
            base.Init(health, maxHealth, name, shield);
        }
        
        protected override void OnStep()
        {
            _energy.Refresh();
        }
    }
}