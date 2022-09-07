using System.Collections.Generic;
using Infrastructure.StaticData.Cards;

namespace Infrastructure
{
    public class PlayerData
    {
        public string Name;
        public List<CardId> Cards;
        public int MaxEnergy;
        public int MaxHealth;
        public int Health;
        public int Initiative;
    }
}