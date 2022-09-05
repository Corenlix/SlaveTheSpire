using System;

namespace Entities
{
    public class Player : Entity
    {
        public event Action<Player> PlayerInited;
        
        public PlayerEnergy Energy => _energy;
        public IDeckHolder DeckHolder { get; private set; }

        private PlayerEnergy _energy;

        public void Init(int energy, int maxEnergy, int health, int maxHealth, string name, int shield,  int initiative, int attackPower)
        {
            DeckHolder = new DeckHolder();
            _energy = new PlayerEnergy(energy, maxEnergy);
            base.Init(health, maxHealth, name, shield, initiative, attackPower);
        }
        
        protected override void OnStep()
        {
            _energy.Refresh();
        }
    }
}