using System.Collections.Generic;
using Entities;
using Infrastructure.StaticData;

namespace Infrastructure
{
    public class PlayerHolder : IPlayerHolder
    {
        public Player Player { get; private set; }
        
        public void SetPlayer(Player player)
        {
            Player = player;
        }
    }
}