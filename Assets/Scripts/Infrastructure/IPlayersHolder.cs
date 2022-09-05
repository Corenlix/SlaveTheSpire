using System.Collections.Generic;
using Entities;

namespace Infrastructure
{
    public interface IPlayersHolder
    {
        List<Player> Players { get; }
        
        void Add(Player player);

        void Remove(Player player);
    }
}