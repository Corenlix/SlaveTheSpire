
using System;
using Infrastructure;

namespace Entities
{
    public class Player : Entity
    {
        public event Action<Player> PlayerInited;
        
        public PlayerEnergy Energy => _energy;
        public IDeckHolder DeckHolder { get; private set; }

        private PlayerEnergy _energy;

        public void Init(PlayerData playerData)
        {
            DeckHolder = new DeckHolder(playerData.Cards);
            _energy = new PlayerEnergy(playerData.MaxEnergy, playerData.MaxEnergy);
            base.Init(playerData.Health, playerData.MaxHealth, playerData.Name, 0, playerData.Initiative, 0);
        }
        
        protected override void OnStep()
        {
            _energy.Refresh();
        }

        public PlayerData GetData()
        {
            return new PlayerData()
            {
                Name = Name,
                Cards = DeckHolder.GetAllCards(),
                MaxEnergy = _energy.MaxEnergy,
                MaxHealth = EntityHealth.MaxHealth,
                Health = EntityHealth.Health,
                Initiative = Initiative,
            };
        }
    }
}